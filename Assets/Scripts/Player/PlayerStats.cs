using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using KOS.Audio;
using KOS.Events;
using KOS.Enemies;
using KOS.PowerUps;

namespace KOS.Player
{
    public class PlayerStats : MonoBehaviour
    {
        private const float MaxCastleHealth = 100;

        [SerializeField]
        public float speedMultiplier = 1f;                   // to be used with movement speed
        public float damageMultiplier = 1f;                  // to be used with weapon damage
        public float powerUpDuration = 15f;                  // how long the speed/damage power ups will last
        public float powerUpModifier = 1.5f;                 // amount power ups will affect stats
        public int addCurrency = 100;                        // value of one Hex coin
        public int healthRecoverAmount = 25;                 // amount the castle will be healed by
        public float currentCastleHealth = MaxCastleHealth;  // Starting and persisting health of castle

        private void Awake()
        {
            EventManager.Active.onCastleHit += CastleTakeDamage;
        }

        // handles picking up power ups and currency
        private void OnTriggerEnter(Collider other)
        {
            switch (other.gameObject.tag)
            {
                case "HealthPowerUp":
                    AudioManager.Active.PlayOneShot("powerUp");
                    EventManager.Active.PowerUpCollected(PowerUpType.Health);
                    Destroy(other.gameObject);
                    RestoreCastleHealth();
                    Debug.Log("Castle Health: " + GetCastleHealth());
                    break;
                case "SpeedPowerUp":
                    AudioManager.Active.PlayOneShot("powerUp");
                    EventManager.Active.PowerUpCollected(PowerUpType.Speed);
                    Destroy(other.gameObject);
                    StartCoroutine(SpeedPowerUp(powerUpDuration));
                    break;
                case "DamagePowerUp":
                    AudioManager.Active.PlayOneShot("powerUp");
                    EventManager.Active.PowerUpCollected(PowerUpType.Damage);
                    Destroy(other.gameObject);
                    StartCoroutine(DamagePowerUp(powerUpDuration));
                    break;
                case "Currency":
                    AudioManager.Active.PlayOneShot("powerUp");
                    EventManager.Active.PowerUpCollected(PowerUpType.Hexes);
                    Destroy(other.gameObject);
                    Bank.Connect.Deposit(addCurrency);
                    Debug.Log("Hexes: " + GetCurrencyAmount());
                    break;
                default:
                    break;
            }
        }

        private void CastleTakeDamage(int damage, int wave)
        {
            currentCastleHealth -= damage;
            EventManager.Active.CastleHealthChanged(currentCastleHealth);
            Debug.Log("CASTLE HEATLTH: " + currentCastleHealth);
        }

        public float GetSpeedMultiplier() => speedMultiplier;
        public float GetDamageMultiplier() => damageMultiplier;
        public float GetCastleHealth() => currentCastleHealth;
        public int GetCurrencyAmount() => Bank.Connect.balance;

        private void RestoreCastleHealth()
        {
            // will not restore castle health beyond maximum
            if (currentCastleHealth + healthRecoverAmount >= MaxCastleHealth)
            {
                currentCastleHealth = MaxCastleHealth;
            }
            else
            {
                currentCastleHealth += healthRecoverAmount;
            }
            EventManager.Active.CastleHealthChanged(currentCastleHealth);
        }

        private IEnumerator SpeedPowerUp (float duration)
        {
            Debug.Log("Speed boost started");
            
            speedMultiplier *= powerUpModifier;
            yield return new WaitForSeconds(duration);
            speedMultiplier /= powerUpModifier;
        
            Debug.Log("Speed boost ended");
        }

        private IEnumerator DamagePowerUp (float duration)
        {
            Debug.Log("Damage boost started");
            
            damageMultiplier *= powerUpModifier;
            yield return new WaitForSeconds(duration);
            damageMultiplier /= powerUpModifier;
        
            Debug.Log("Damage boost ended");
        }
    }
}

