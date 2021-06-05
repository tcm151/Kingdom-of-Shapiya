
using UnityEngine;

using KOS.ItemData;
using KOS.Projectiles;

namespace KOS.Weapons
{
    [CreateAssetMenu(menuName = "Stats/Weapon Stats")]
    public class WeaponData : Data
    {
        public WeaponType weaponType;
        public int unlockCost = 150;
        public bool unlocked = false;

        [Header("Stats")]
        public ProjectileType projectileType;
        public Stat projectileDamage;
        public Stat projectileAmount;
        public Stat projectileSpeed;
        public Stat fireRate;
        public bool automatic = false;

        [Header("Recoil & Spread")]
        public Vector2 horizontalRecoil;
        public Vector2 verticalRecoil;
        public Vector2 horizontalSpread;
        public Vector2 verticalSpread;
        public string soundName;

        private void OnEnable()
        {
            projectileDamage.SetDefault();
            projectileAmount.SetDefault();
            projectileSpeed.SetDefault();
            fireRate.SetDefault();
        }
    }
}
