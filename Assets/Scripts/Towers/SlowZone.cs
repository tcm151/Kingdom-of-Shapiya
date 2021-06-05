
using UnityEngine;

using KOS.Enemies;

namespace KOS.Towers
{
    public class SlowZone : MonoBehaviour
    {
        private SlowTower slowTower;

        public float scale
        {
            set => transform.localScale = Vector3.one * value;
            get
            {
                float scale = 0f;
                scale += transform.localScale.x;
                scale += transform.localScale.y;
                scale += transform.localScale.z;
                return scale / 3;
            }
        }

        private void Awake()
        {
            slowTower = gameObject.GetComponentInParent<SlowTower>();
        }

        private void OnTriggerEnter(Collider collider)
        {
            if (collider.gameObject.tag == "Enemy")
            {
                Enemy enemy = collider.GetComponentInParent<Enemy>();
                slowTower.SlowEnemy(enemy);
            }
        }

        private void OnTriggerExit(Collider collider)
        {
            if (collider.gameObject.tag == "Enemy")
            {
                Enemy enemy = collider.GetComponentInParent<Enemy>();
                slowTower.UnSlowEnemy(enemy);
            }
        }
    }
}
