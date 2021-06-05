
using UnityEngine;

using KOS.Events;
using KOS.ItemData;

namespace KOS.PowerUps
{
    public class PowerUp : MonoBehaviour //, IDataItem
    {
        protected PowerUpType powerUp;

        //- OBJECT STATS
        // public PowerUpData Data => enemy;
        // [SerializeField] protected PowerUpData enemy = default;
        // public void SetData(Data data) => enemy = (PowerUpData)data;

        
        protected void OnTriggerEnter(Collider collider)
        {
            if (collider.gameObject.tag == "Player")
            {
                EventManager.Active.PowerUpCollected(powerUp);
            }
        }
        
    }
}