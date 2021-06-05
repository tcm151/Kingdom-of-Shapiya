
using UnityEngine;

using KOS.Audio;
using KOS.Particles;
using KOS.Damageables;
using KOS.Explodeables;

namespace KOS.Enemies
{
    public class ExplodingEnemy : FloatingEnemy, IExplodeable
    {
        //- LOCAL VARIABLES
        new protected Rigidbody rigidbody;
        protected IDamageable damageable;
        protected float damageReceived;

        //> EXPLODE! (BOOM)
        public void Explode(Vector3 point)
        {
            Collider[] colliders = Physics.OverlapSphere(point, enemy.blastRadius);
            foreach (var collider in colliders)
            {
                damageable = collider.GetComponentInParent<IDamageable>();
                if (damageable != null)
                {
                    damageReceived = Mathf.Clamp(enemy.blastDamage / (Vector3.Distance(rigidbody.position, damageable.position)), 0f, enemy.blastDamage);
                    damageable.TakeDamage(damageReceived, enemy.name);
                    damageable = null;
                }

                rigidbody = collider.GetComponent<Rigidbody>();
                if (rigidbody != null)
                {
                    rigidbody.AddExplosionForce(enemy.blastForce, rigidbody.position, enemy.blastRadius, enemy.upwardsInfluence);
                    rigidbody = null;                
                }
            }

            ImpactAt(point);
            AudioManager.Active.PlayAtPoint("explosion", point);
        }

        //> DO THINGS ON IMPACT
        private void ImpactAt(Vector3 impactPoint)
        {
            ParticleGroup impact = particleFactory.Get(ParticleType.Explosion);
            impact.position = impactPoint;
            impact.Play();
        }
    }
}
