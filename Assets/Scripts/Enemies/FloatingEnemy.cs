
using UnityEngine;

namespace KOS.Enemies
{
    [SelectionBase]
    [RequireComponent(typeof(Rigidbody))]
    public class FloatingEnemy : Enemy
    {
        [HideInInspector] public AnimationCurve floatForceCurve;
        protected FloatPoint[] floatPoints;
        protected float floatHeight;
        protected float floatForce;
        protected float maxFloatForce;

        [SerializeField] protected LayerMask floatLayers;

        //> INITIALIZATION
        override protected void Awake()
        {
            base.Awake();

            // get components
            floatPoints = GetComponentsInChildren<FloatPoint>();
            // set initial enemy data
            floatHeight = enemy.floatHeight;
            maxFloatForce = enemy.floatForce;
            // add a little variation
            floatHeight += Random.Range(enemy.floatHeightVariance.x, enemy.floatHeightVariance.y);
            
            SetFloatCurve();
        }

        //> SET THE FORCE CURVES WITH CURRENT VALUES
        virtual protected void SetFloatCurve()
        {
            Keyframe[] keys = new Keyframe[2];
            keys[0].time = 0;
            keys[0].value = maxFloatForce;
            keys[1].time = floatHeight;
            keys[1].value = 9.81f;
            floatForceCurve.keys = keys;
        }

        //> APPLY MOVEMENT ON FIXED TIME STEP
        override protected void FixedUpdate()
        {
            Move();
            Float();
        }

        //> FLOAT THE ENEMY AT THE DESIRED HEIGHT
        protected void Float()
        {
            foreach (var point in floatPoints) // cast a ray from every float point
            {
                if (Physics.Raycast(point.position, Vector3.down, out RaycastHit hit, floatHeight, floatLayers))
                {
                    floatForce = floatForceCurve.Evaluate(hit.distance) / floatPoints.Length;
                    rigidbody.AddForce(Vector3.up * floatForce, ForceMode.Acceleration);

                    Debug.DrawRay(point.position, Vector3.down * hit.distance, Color.red);
                }
                else
                {
                    // rigidbody should fall slower than gravity
                    rigidbody.AddForce(Vector3.up * 3.5f / floatPoints.Length, ForceMode.Acceleration);

                    Debug.DrawRay(point.position, Vector3.down * 20, Color.green);
                }
            }
        }
    }
}
