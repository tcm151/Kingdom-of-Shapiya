
using UnityEngine;

using KOS.Enemies;

namespace KOS.Towers
{
    public class SlowTower : Tower
    {
        [SerializeField]
        private GameObject slowZoneArea;
        private SlowZone slowZone;

        override protected void Awake()
        {
            slowZone = GetComponentInChildren<SlowZone>();
            slowZoneArea.transform.localScale = Vector3.one * tower.range.value;
            rangeIndicator = GetComponentInChildren<RangeIndicator>(true);
        }
        
        override protected void Update()
        {
            if (slowZone.scale != tower.range.value)
            {
                slowZone.scale = tower.range.value;
                UpdateSlow();
            }

            if (rangeIndicator.scale != tower.range.value) rangeIndicator.scale = tower.range.value;
        }

        override protected void Fire()
        {
            // do do
        }

        public void SlowEnemy(Enemy enemy)
        {
            enemy.MoveForce = enemy.Data.moveForce * (1f - tower.projectileDamage.value);
            // enemy.numOfTowersSlowing++;
        }

        //> BUG

        public void UnSlowEnemy(Enemy enemy)
        {
            // enemy.numOfTowersSlowing--;
            // if (enemy.numOfTowersSlowing <= 0) enemy.MoveForce = enemy.Stats.moveForce;
        }

        //> UPDATE COLLIDER RADIUS FOR TOWERS
        private void UpdateRange()
        {
            slowZoneArea.transform.localScale = Vector3.one * tower.range.value;
            UpdateSlow();
        }

        //> RE-SLOW ENEMIES WITHIN SLOW ZONES. USEFUL FOR TOWER UPGRADES
        private void UpdateSlow()
        {
            Collider[] colliders = Physics.OverlapSphere(slowZoneArea.transform.position, tower.range.value);
            foreach (var collider in colliders)
            {
                if (collider.gameObject.tag == "Enemy")
                {
                    Enemy enemy = collider.GetComponentInParent<Enemy>();
                    enemy.MoveForce = (1.0f - Data.projectileDamage.value) * enemy.Data.moveForce;
                }
            }
        }
    }
}
