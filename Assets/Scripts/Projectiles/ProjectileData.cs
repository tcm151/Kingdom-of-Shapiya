using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using KOS.Particles;
using KOS.ItemData;

namespace KOS.Projectiles
{
    [CreateAssetMenu(menuName = "Stats/Projectile Data")]
    public class ProjectileData : Data
    {
        // public ProjectileType type;

        [Header("Stats")]
        public float mass = 0.1f;
        public float lifetime = 5f;

        [Space(12)] public ParticleType particles;
        
    }
}
