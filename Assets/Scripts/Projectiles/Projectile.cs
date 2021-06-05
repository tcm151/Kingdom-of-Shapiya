
using UnityEngine;

using KOS.ItemData;
using KOS.Particles;
using KOS.Damageables;

namespace KOS.Projectiles
{
    [SelectionBase]
    [RequireComponent(typeof(Rigidbody))]
    public class Projectile : MonoBehaviour, IDataItem<ProjectileData>
    {
        //- FACTORIES
        [SerializeField] protected ParticleFactory particleFactory = default;
        [SerializeField] internal ProjectileFactory originFactory = default;
        
        //- ITEM DATA
        public void SetData(ProjectileData data) => projectile = data;
        [SerializeField] private ProjectileData projectile;
        virtual public ProjectileData Data => projectile;

        //- COMPONENTS
        new protected Rigidbody rigidbody;

        //- LOCAL VARIABLES
        protected Vector3 previousPosition;
        protected string origin;
        protected float lifetime;

        //- STATE CHECKS
        virtual protected bool IsMoving => rigidbody.velocity.magnitude > 0;
        virtual protected bool IsExpired => lifetime >= Data.lifetime;

        //- DAMAGE
        public float damage {get; protected set;}
        
        //- POSITION HELPER
        public Vector3 position
        {
            get => transform.position;
            set => transform.position = value;
        }

        //> INITIALIZATION
        virtual protected void Awake()
        {
            // get components
            rigidbody = GetComponent<Rigidbody>();

            // initialize values
            previousPosition = rigidbody.position;
            rigidbody.mass = Data.mass;
            lifetime = 0;
        }

        //> FIRE WITH A GIVEN DIRECTION AND SPEED
        virtual public void Launch(Vector3 position, Vector3 direction, float speed, float damage, string origin)
        {
            this.damage = damage;
            this.origin = origin;
            rigidbody.position = previousPosition = position;
            rigidbody.rotation = Quaternion.LookRotation(direction, Vector3.up);
            rigidbody.AddForce(direction.normalized * (speed * rigidbody.mass), ForceMode.Impulse);
        }

        //> FIRE WITH A GIVEN VELOCITY
        virtual public void Launch(Vector3 position, Vector3 velocity, float damage, string origin)
        {
            this.damage = damage;
            this.origin = origin;
            rigidbody.position = previousPosition = position;
            rigidbody.rotation = Quaternion.LookRotation(velocity.normalized, Vector3.up);
            rigidbody.AddForce(velocity * rigidbody.mass, ForceMode.Impulse);
        }

        //> UPDATE EVERY FRAME
        virtual protected void Update()
        {
            if (this.IsExpired) originFactory.Reclaim(this);
            if (this.IsMoving) transform.rotation = Quaternion.LookRotation(rigidbody.velocity, Vector3.up);

            CheckImpact();
        }
        
        // yo I am a pussy

        //> CHECK IMPACT FOR BALLISTIC PROJECTILES
        virtual protected void CheckImpact()
        {
            if (Physics.Linecast(previousPosition, rigidbody.position, out RaycastHit hit))
            {
                IDamageable damageable = hit.collider.GetComponentInParent<IDamageable>();
                if (damageable != null)
                {
                    damageable.TakeDamage(this.damage, this.origin);
                    ImpactAt(hit.point);   
                }
                
                if (hit.collider.gameObject != this.gameObject) ImpactAt(hit.point);
            }
            else previousPosition = rigidbody.position;
        }

        //> DO THINGS ON IMPACT
        virtual protected void ImpactAt(Vector3 impactPoint)
        {
            ParticleGroup impact = particleFactory.Get(Data.particles);
            impact.position = impactPoint;
            impact.forward = transform.forward;
            impact.Play();
            
            originFactory.Reclaim(this);
        }
    }
}