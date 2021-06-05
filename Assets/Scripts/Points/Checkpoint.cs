using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using KOS.Enemies;

namespace KOS.Points
{
    [RequireComponent(typeof(CapsuleCollider))]
    public class Checkpoint : MonoBehaviour
    {
        [SerializeField]
        protected float radius = 5f;

        protected Vector3 position;
        public Vector3 Position
        {
            get // return random position within radius
            {
                Vector3 position = transform.position;
                Vector2 randomWithinRadius = Random.insideUnitCircle * radius / 3.25f;
                position.x += randomWithinRadius.x;
                position.z += randomWithinRadius.y;

                return position;
            }
        }

        [SerializeField]
        protected Checkpoint nextTarget;
        public Checkpoint NextTarget {get => nextTarget;}

        //> UPDATE ON INSPECTOR CHANGES
        virtual protected void OnValidate()
        {
            transform.localScale = new Vector3(radius, 25f, radius);
        }

        //> SEND ENEMY TO NEXT CHECKPOINT
        virtual protected void OnTriggerEnter(Collider collison)
        {
            if (!nextTarget)
            {
                Debug.LogError("Next checkpoint not set!", this.gameObject);
                Debug.Break();
                return;
            }

            if (collison.gameObject.tag == "Enemy" && nextTarget)
            {
                collison.gameObject.GetComponentInParent<Enemy>().SetTarget(nextTarget.Position);
            }
        }
    }
}
