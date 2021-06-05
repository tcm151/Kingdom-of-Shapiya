using UnityEngine;
using KOS.Audio;
using KOS.Waves;
using KOS.Events;
using KOS.PowerUps;
using KOS.ItemData;
using KOS.Particles;
using KOS.Factories;
using KOS.Damageables;



namespace KOS.Enemies
{
    [SelectionBase]
    [RequireComponent(typeof(Rigidbody))]
    abstract public class Enemy : MonoBehaviour, IDataItem<EnemyData>, IDamageable
    {
        //- FACTORIES
        [SerializeField] protected ParticleFactory particleFactory = default;
        internal EnemyFactory originFactory;
        protected Spawner spawner;

        //- OBJECT STATS
        public EnemyData Data => enemy;
        [SerializeField] protected EnemyData enemy = default;
        public void SetData(EnemyData data) => enemy = data;

        //- MOVE FORCE
        public float MoveForce {get; set;}

        //- POWERUPS
        [SerializeField] protected PowerUp[] powerUps;
        protected bool HasPowerUps => powerUps.Length > 0;

        //- COMPONENTS
        new public Rigidbody rigidbody {get; protected set;}

        //- LOCAL VARIABLES
        [SerializeField] protected Vector3 target;
        private Vector3 targetDirection, lookDirection;
        private Quaternion lookRotation;

        //- STATE CHECKS
        // protected bool isSlowed; // yet to be implemented
        protected bool IsDead => health <= 0;
        protected bool IsMoving => rigidbody.velocity.magnitude > 0;
        protected int Reward => Random.Range(enemy.hexes.x, enemy.hexes.y);

        //- HEALTH
        public float health {get; protected set;}

        //- SHORTHAND HELPERS
        public Vector3 position => rigidbody.position;
        public Vector3 velocity => rigidbody.velocity;

        //> INITIALIZATION
        virtual protected void Awake()
        {
            // get components
            rigidbody = GetComponent<Rigidbody>();
            // set initial enemy
            health = enemy.health;
            rigidbody.mass = enemy.mass;
            MoveForce = enemy.moveForce;
            // add a little variation
            MoveForce += Random.Range(enemy.moveForceVariance.x, enemy.moveForceVariance.y);
        }

        virtual public void SetTarget(Vector3 newTarget) => target = newTarget;
        virtual public void SpawnAt(Vector3 spawnPosition) => transform.position = spawnPosition;

        //> APPLY MOVEMENT ON FIXED TIME STEP
        virtual protected void FixedUpdate() => Move();

        //> MOVE ENEMY TOWARDS ITS TARGET
        virtual protected void Move()
        {
            // calculate the direction which to move
            targetDirection = (target - transform.position).normalized;
            targetDirection.y = 0;
            // add a force in that direction, adjust for the current velocity to go a constant speed
            rigidbody.AddForce((targetDirection * MoveForce) - (rigidbody.velocity), ForceMode.Acceleration);

            if (!this.IsMoving) return;

            // force look towards movement direction
            lookDirection = rigidbody.velocity.normalized;
            lookDirection.y = 0;
            lookRotation = Quaternion.LookRotation(lookDirection, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.fixedDeltaTime);
        }

        //> TAKE DAMAGE FROM AN EXTERNAL OBJECT
        virtual public void TakeDamage(float damage, string origin)
        {
            health -= damage;

            if (this.IsDead) Die(origin);
        }

        public System.Action<Enemy> onDeath;

        virtual protected void Die(string origin)
        {
            // log kill
            // Debug.Log($"{enemy.name} was killed by {origin}", this);

            // event triggers
            onDeath?.Invoke(this);
            EventManager.Active.EnemyKilled(origin);
            AudioManager.Active.PlayAtPoint("enemyDeath", rigidbody.position);

            //- deposit reward 
            if (origin != "castle") Bank.Connect.Deposit(this.Reward);

            // play death particles
            ParticleGroup deathParticles = particleFactory.Get(enemy.particles);
            deathParticles.position = rigidbody.position;
            deathParticles.SetScale(enemy.particleScale);
            deathParticles.Play();

            // cleanup
            // EnemyList.Connect.Remove(this); //! TODO port this towards events
            originFactory.Reclaim(this);

            // potentially spawn power up on death
            if (HasPowerUps) SpawnPowerUp();
        }

        //> POWER-UP SPAWNING
        virtual protected void SpawnPowerUp()
        {
            //> REFACTOR to use rigidbody enemy powerup drop chance
            // 7% chance for power-up to spawn 
            if (Random.Range(1, 16) != 15) return;

            // randomly select power-up to spawn
            int index = Random.Range(0, powerUps.Length);
            Instantiate(powerUps[index], rigidbody.position, rigidbody.rotation);
        }

        //> DRAW HELPFUL THINGS WHEN SELECTED
        virtual protected void OnDrawGizmosSelected()
        {
            if (target == Vector3.zero) return;

            // draw ray towards target
            Vector3 targetGizmo = target;
            targetGizmo.y = position.y;
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(targetGizmo, 0.1f);
            Gizmos.DrawLine(rigidbody.position, targetGizmo);
        }
    }
}