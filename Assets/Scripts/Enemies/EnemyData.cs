
using UnityEngine;

using KOS.Particles;
using KOS.ItemData;

namespace KOS.Enemies
{
    [CreateAssetMenu(menuName = "Stats/Enemy Stats")]
    public class EnemyData : Data
    {
        // public EnemyType enemyType;

        [Header("Stats")]
        public float health = 2f;
        [Range(1, 100)]
        public int damage = 1;
        [Range(5, 100)]
        public float mass = 10f;
        [Range(1, 15)]
        public float moveForce = 4f;
        [Range(0.25f, 25)]
        public float floatHeight = 1f;
        [Range(20, 50)]
        public float floatForce = 33f;
        public bool isAerial = false;

        [Header("Variation")]
        public Vector2 floatHeightVariance;
        public Vector2 moveForceVariance;

        [Header("Death")]
        public ParticleType particles;
        public float particleScale;

        [Header("Explosion")]
        public float blastRadius;
        public float blastForce;
        public float blastDamage;
        public float upwardsInfluence;

        [Header("Rewards")]
        public float powerupDropChance = 0.01f;
        public Vector2Int hexes;
    }
}
