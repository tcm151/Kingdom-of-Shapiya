using System;
using System.Collections.Generic;
using UnityEngine;
using KOS.Audio;
using KOS.Damageables;
using KOS.Enemies;
using KOS.Particles;
using KOS.Projectiles;
using Random = UnityEngine.Random;


namespace KOS.Towers
{
    [SelectionBase]
    public class BallisticTower : Tower
    {
        //- FACTORIES
        [SerializeField] protected ProjectileFactory projectileFactory = default;

        //- TARGETING
        public TargetMode targetMode;
        public LayerMask lineOfSightMask;
        protected Enemy strongest, weakest;
        protected List<Enemy> potentialTargets;

        [Header("")][SerializeField] protected Enemy targetEnemy;

        //- COMPONENTS
        protected ProjectileOrigin[] origins;
        protected Transform headPivot;

        //- LOCAL VARIABLES
        protected float targetDistance;
        protected bool canFire;
        // public HexCell hexCell;

        //> INITIALIZATION
        override protected void Awake()
        {
            // old
            active = false;
            fireInterval = 60f;
            rangeIndicator = GetComponentInChildren<RangeIndicator>(true);

            // new
            targetMode = tower.targetMode;
            headPivot = transform.GetChild(2);
            potentialTargets = new List<Enemy>();
            origins = GetComponentsInChildren<ProjectileOrigin>();
        }

        //> DOES ENEMY HAVE VALID TARGET
        private bool ValidTarget => targetEnemy != null;

        //> CHECK IF TARGET IS WITHIN RANGE
        private bool TargetWithinRange
        {
            get
            {
                targetDistance = Vector3.Distance(transform.position, targetEnemy.position);
                return (targetDistance <= tower.range.value);
            }
        }

        //> CAN TOWER SEE TARGET
        private bool HasLineOfSight(IDamageable damageable)
        {
            if (Physics.Linecast(headPivot.position, damageable.position, out RaycastHit hit, lineOfSightMask))
            {
                // REFACTOR remove tower tag here with setup layer mask
                if (hit.collider.tag == "Enemy" || hit.collider.tag == "Tower") return true;
            }

            return false;
        }

        //> CHECK IF TOWER HAS TARGET AND CAN FIRE
        override protected void Update()
        {
            if (!active) return; // do nothing if unactive

            if (!ValidTarget || !TargetWithinRange || !HasLineOfSight(targetEnemy))
            {
                AcquireTargets();
                return;
            }

            if (Math.Abs(rangeIndicator.scale - tower.range.value) > 0.0001f) rangeIndicator.scale = tower.range.value;

            // only fire if facing collider within 2-ish degrees
            canFire = RotateTowardsTarget();

            // monitor fire rate
            if ((fireInterval += tower.fireRate.value * Time.deltaTime) >= 60f && canFire)
            {
                fireInterval = 0;
                Fire();
            }
        }

        //> ACQUIRE A NEW TARGET
        virtual protected bool AcquireTargets()
        {
            // clear trackers
            potentialTargets.Clear();
            strongest = weakest = null;

            // get all colliders within range
            Collider[] colliders = Physics.OverlapSphere(transform.position, tower.range.value);

            // return if no colliders
            if (colliders.Length <= 0) return false;

            foreach (var collider in colliders)
            {
                Enemy enemy = collider.GetComponentInParent<Enemy>();

                if (!enemy) continue;
                if (tower.isAntiAir != enemy.Data.isAerial || !HasLineOfSight(enemy)) continue;

                potentialTargets.Add(enemy);
                if (!strongest) strongest = enemy;
                if (!weakest) weakest = enemy;
                if (enemy.health > strongest.health) strongest = enemy;
                if (enemy.health < weakest.health) weakest = enemy;
            }

            return SelectTarget();
        }

        //> SELECT TARGET BASED ON TARGETING MODE
        virtual protected bool SelectTarget()
        {
            if (potentialTargets.Count > 0)
            {
                targetEnemy = (targetMode) switch
                {
                    (TargetMode.Strongest) => strongest,
                    (TargetMode.Weakest)   => weakest,
                    (TargetMode.First)     => potentialTargets[0],
                    (TargetMode.Last)      => potentialTargets[potentialTargets.Count - 1],
                    (TargetMode.Random)    => potentialTargets[Random.Range(0, potentialTargets.Count)],
                            _              => null,
                };
                
                //! Must test above works before removing this...
                // switch (targetMode)
                // {
                //     case TargetMode.Random:
                //         targetEnemy = potentialTargets[Random.Range(0, potentialTargets.Count)];
                //         return true;
                //
                //     case TargetMode.First:
                //         targetEnemy = potentialTargets[0];
                //         return true;
                //
                //     case TargetMode.Last:
                //         targetEnemy = potentialTargets[potentialTargets.Count - 1];
                //         return true;
                //
                //     case TargetMode.Strongest:
                //         targetEnemy = strongest;
                //         return true;
                //
                //     case TargetMode.Weakest:
                //         targetEnemy = weakest;
                //         return true;
                //
                //     default:
                //         Debug.LogWarning("No targeting mode set!");
                //         targetEnemy = null;
                //         return false;
                // }
            }

            targetEnemy = null;
            return false;
        }

        //> ROTATE TOWARDS THE TARGET AT ROTATION SPEED
        private bool RotateTowardsTarget()
        {
            Quaternion targetDirection = Quaternion.LookRotation(targetEnemy.position - origins[current].position);
            headPivot.rotation = Quaternion.RotateTowards(headPivot.rotation, targetDirection, tower.rotationSpeed.value * Time.deltaTime);
            float dot = Quaternion.Dot(headPivot.rotation, targetDirection);

            // can fire if *almost* looking directly at enemy
            return (dot <= -0.999f || dot >= 0.999f);
        }

        //> CALCULATE TRAJECTORY NEEDED TO HIT MOVING TARGET
        private Vector3 CalculateTrajectory()
        {
            // magic helper numbers
            const float ErrorMultiplier = 1.1f, HalfGravity = 4.905f;

            // calculate expected travel time
            Vector3 targetPosition = targetEnemy.position;
            Vector3 targetVelocity = targetEnemy.velocity;
            float currentDistance = Vector3.Distance(targetPosition, origins[current].position);
            float travelTime = (currentDistance / tower.projectileSpeed.value) * ErrorMultiplier;

            // determine horizontal plane trajectory
            Vector3 interceptPosition = targetPosition + (targetVelocity * travelTime);
            Vector3 trajectory = (interceptPosition - origins[current].position).normalized * tower.projectileSpeed.value;
            trajectory.y = 0;

            // separately calculate required vertical trajectory
            float interceptDistance = Vector3.Distance(interceptPosition, origins[current].position);
            float interceptTime = interceptDistance / tower.projectileSpeed.value;
            float verticalVelocity = (interceptPosition.y - origins[current].position.y - (-HalfGravity * interceptTime * interceptTime)) / interceptTime;
            trajectory.y = verticalVelocity;

            return trajectory;
        }

        //> FIRE A PROJECTILE
        override protected void Fire()
        {
            if (!ValidTarget || !TargetWithinRange) return;

            Vector3 trajectory = CalculateTrajectory();

            Projectile projectile = projectileFactory.Get(tower.projectileType);
            projectile.position = origins[current].position;
            projectile.Launch(origins[current].position, trajectory, tower.projectileDamage.value, tower.name);

            AudioManager.Active.PlayAtPoint("towerFire", origins[current].position);

            ParticleGroup muzzleFlash = particleFactory.Get(ParticleType.TowerMuzzleFlash);
            muzzleFlash.position = origins[current].position;
            muzzleFlash.forward = origins[current].forward;
            muzzleFlash.Play();
        }

        //> DRAW TOWER RANGE AND TARGET
        override protected void OnDrawGizmosSelected()
        {
            base.OnDrawGizmosSelected();

            if (!ValidTarget) return;

            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, targetEnemy.position);
        }
    }
}