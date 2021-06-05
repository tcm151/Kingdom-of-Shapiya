using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using KOS.Enemies;

namespace KOS.Towers
{
    public class AreaOfEffectTower : Tower
    {
        //- LOCAL VARIABLES
        protected Collider[] colliders;
        protected Enemy enemy;

        override protected void Fire()
        {
            colliders = Physics.OverlapSphere(transform.position, tower.range.value);
            foreach (var collider in colliders)
            {
                enemy = collider.GetComponent<Enemy>();
                // apply effect to enemies
            }
        }
    }
}
