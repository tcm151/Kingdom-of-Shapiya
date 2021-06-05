
using UnityEngine;

using KOS.ItemData;
using KOS.Projectiles;

namespace KOS.Towers
{
    [CreateAssetMenu(menuName = "Stats/Tower Stats")]
    public class TowerData : Data
    {
        // [Header("Basic Info")]
        public TowerType towerType;
        public int buildCost;

        [Header("Stats")]
        public ProjectileType projectileType;
        public TargetMode targetMode;
        public Stat projectileDamage;
        public Stat projectileSpeed;
        public Stat rotationSpeed;
        public Stat fireRate;
        public Stat range;


        [Header("Tower Type")]
        public bool isAntiAir;

        virtual protected void OnEnable()
        {
            projectileDamage.SetDefault();
            projectileSpeed.SetDefault();
            rotationSpeed.SetDefault();
            range.SetDefault();
            fireRate.SetDefault();
        }
    }
}
