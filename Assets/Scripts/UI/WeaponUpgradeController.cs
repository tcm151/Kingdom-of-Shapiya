
using TMPro;
using UnityEngine;
using UnityEngine.UI;

using KOS.Events;
using KOS.Weapons;

namespace KOS.UI
{
    public class WeaponUpgradeController : MonoBehaviour
    {
        private WeaponData stats;
        private Weapon weapon;

        [Header("Info")]
        [SerializeField] private Image weaponImage;
        [SerializeField] private TextMeshProUGUI weaponName;

        [Header("Purchasing")]
        [SerializeField] private GameObject purchaseEntry;
        [SerializeField] private Button purchaseButton;
        [SerializeField] private TextMeshProUGUI unlockCost;
        [SerializeField] private GameObject unlockMask;


        [Header("Stat Entries")]
        [SerializeField] private GameObject statEntry1;
        [SerializeField] private TextMeshProUGUI damageUpgradeCost;
        [SerializeField] private TextMeshProUGUI damageLevel;
        [SerializeField] private TextMeshProUGUI damageValue;
        [SerializeField] private GameObject statEntry2;
        [SerializeField] private TextMeshProUGUI bulletSpeedUpgradeCost;
        [SerializeField] private TextMeshProUGUI bulletSpeedLevel;
        [SerializeField] private TextMeshProUGUI bulletSpeedValue;
        [SerializeField] private GameObject statEntry3;
        [SerializeField] private TextMeshProUGUI bulletAmountUpgradeCost;
        [SerializeField] private TextMeshProUGUI bulletAmountLevel;
        [SerializeField] private TextMeshProUGUI bulletAmountValue;
        [SerializeField] private GameObject statEntry4;
        [SerializeField] private TextMeshProUGUI fireRateUpgradeCost;
        [SerializeField] private TextMeshProUGUI fireRateLevel;
        [SerializeField] private TextMeshProUGUI fireRateValue;


        static private Color green = new Color32(0,255,0,255), red = new Color32(255,0,0,255);

        private void Update() => Refresh();

        public void ChangeWeaponSlot(WeaponData newWeapon)
        {
            stats = newWeapon;
            weaponName.text = stats.name;
            weaponImage.sprite = stats.image;
            weapon = GameObject.Find("Weapons").transform.Find(stats.name).gameObject.GetComponent<Weapon>();

            Refresh();
        }

        private void Refresh()
        {
            if (!weapon.Unlocked)
            {
                unlockMask.SetActive(true);
                purchaseEntry.SetActive(true);

                unlockCost.color = (weapon.IsUnlockable()) ? green : red;
                unlockCost.text = stats.unlockCost.ToString();

                damageUpgradeCost.color = red;
                bulletSpeedUpgradeCost.color = red;
                bulletAmountUpgradeCost.color = red;
                fireRateUpgradeCost.color = red;

            }
            else
            {
                unlockMask.SetActive(false);
                purchaseEntry.SetActive(false);

                damageUpgradeCost.color = (stats.projectileDamage.CanUpgrade()) ? green : red;
                bulletSpeedUpgradeCost.color = (stats.projectileSpeed.CanUpgrade()) ? green : red;
                bulletAmountUpgradeCost.color = (stats.projectileAmount.CanUpgrade()) ? green : red;
                fireRateUpgradeCost.color = (stats.fireRate.CanUpgrade()) ? green : red;
            }


            
            if (stats.projectileDamage.upgradeable == false) statEntry1.SetActive(false);
            else statEntry1.SetActive(true);
            if (stats.projectileSpeed.upgradeable == false) statEntry2.SetActive(false);
            else statEntry2.SetActive(true);
            if (stats.projectileAmount.upgradeable == false) statEntry3.SetActive(false);
            else statEntry3.SetActive(true);
            if (stats.fireRate.upgradeable == false) statEntry4.SetActive(false);
            else statEntry4.SetActive(true);

            damageUpgradeCost.text = Mathf.RoundToInt(stats.projectileDamage.upgradeCost).ToString();
            damageLevel.text = stats.projectileDamage.level.ToString();
            damageValue.text = stats.projectileDamage.value.ToString("G4");
            bulletSpeedUpgradeCost.text = Mathf.RoundToInt(stats.projectileSpeed.upgradeCost).ToString();
            bulletSpeedLevel.text = stats.projectileSpeed.level.ToString();
            bulletSpeedValue.text = stats.projectileSpeed.value.ToString("G4");
            bulletAmountUpgradeCost.text = Mathf.RoundToInt(stats.projectileAmount.upgradeCost).ToString();
            bulletAmountLevel.text = stats.projectileAmount.level.ToString();
            bulletAmountValue.text = stats.projectileAmount.value.ToString("G4");
            fireRateUpgradeCost.text = Mathf.RoundToInt(stats.fireRate.upgradeCost).ToString();
            fireRateLevel.text = stats.fireRate.level.ToString();
            fireRateValue.text = stats.fireRate.value.ToString("G4");
        }

        public void UnlockWeapon()
        {
            if (weapon.Unlock())
            {
                EventManager.Active.WeaponPurchased(stats.weaponType);
                Debug.Log(stats.name + " unlocked!");
                Refresh();
            }
        }

        public void UpgradeDamage()
        {
            EventManager.Active.WeaponUpgraded(stats.weaponType);
            stats.projectileDamage.Upgrade();
        }


        public void UpgradeBulletSpeed()
        {
            EventManager.Active.WeaponUpgraded(stats.weaponType);
            stats.projectileSpeed.Upgrade();
        }
        
        public void UpgradeBulletAmount()
        {
            EventManager.Active.WeaponUpgraded(stats.weaponType);
            stats.projectileAmount.Upgrade();
        }

        public void UpgradeFirerate()
        {
            EventManager.Active.WeaponUpgraded(stats.weaponType);
            stats.fireRate.Upgrade();
        }
    }
}
