using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using KOS.Audio;
using KOS.ItemData;
using KOS.Particles;
using KOS.Damageables;
using KOS.Explodeables;

namespace KOS.Projectiles
{
    [SelectionBase]
    [RequireComponent(typeof(Rigidbody))]
    public class ExplosiveProjectile : Projectile, IDataItem<ExplosiveProjectileData>, IExplodeable
    {
        public void SetData(ExplosiveProjectileData data) => explosion = data;
        override public ProjectileData Data => explosion;
        [SerializeField] private ExplosiveProjectileData explosion;

        //- LOCAL VARIABLES
        protected IDamageable damageable;
        protected Collider[] colliders;
        protected float damageReceived;

        //> CHECK IMPACT FOR EXPLOSIVE BALLISTIC PROJECTILES
        override protected void CheckImpact()
        {
            if (Physics.Linecast(previousPosition, rigidbody.position, out RaycastHit hit))
            {
                if (hit.collider.gameObject == this.gameObject) return;
                else Explode(hit.point);
            }
            else previousPosition = rigidbody.position;
        }

        //> EXPLODE! (BOOM)
        public void Explode(Vector3 point)
        {
            colliders = Physics.OverlapSphere(point, explosion.blastRadius);
            foreach (var collider in colliders)
            {
                damageable = collider.GetComponentInParent<IDamageable>();
                if (damageable != null)
                {
                    damageReceived = this.damage / (Vector3.Distance(point, damageable.position));
                    damageReceived = Mathf.Clamp(damageReceived, 0f, this.damage);
                    damageable.TakeDamage(damageReceived, origin);
                    damageable = null;
                }
                
                rigidbody = collider.GetComponentInParent<Rigidbody>();
                if (rigidbody != null)
                {
                    rigidbody.AddExplosionForce(explosion.blastForce, point, explosion.blastRadius, explosion.upwardsInfluence);
                    rigidbody = null;
                }
            }

            ImpactAt(point);
            AudioManager.Active.PlayAtPoint("explosion", point);
        }

        //> DRAW BLAST RADIUS
        private void OnDrawGizmosSelected()
        {
            if (!rigidbody || !explosion) return;
            
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(rigidbody.position, explosion.blastRadius);
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(rigidbody.position, explosion.blastRadius * 2.5f);
        }

    }
}
