using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KOS.Projectiles
{
    [CreateAssetMenu(menuName = "Stats/Explosive Projectile Data")]
    public class ExplosiveProjectileData : ProjectileData
    {
        [Header("Explosives")]
        public float blastRadius;
        public float blastForce;
        public float upwardsInfluence;
    }
}
