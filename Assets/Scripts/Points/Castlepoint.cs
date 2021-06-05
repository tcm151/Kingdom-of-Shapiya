using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using KOS.Events;
using KOS.Enemies;

namespace KOS.Points
{
    public class Castlepoint : Checkpoint
    {
        //> OVERRIDE CHECKPOINT TRIGGER ENTER
        override protected void OnTriggerEnter(Collider collision)
        {
            if (collision.gameObject.tag == "Enemy")
            {
                Enemy enemy = collision.gameObject.GetComponentInParent<Enemy>();
                EventManager.Active.CastleHit(enemy.Data.damage, 0);
                enemy.TakeDamage(10000, "castle");
                // enemy.Factory.Reclaim(enemy);
            }
        }
    }
}
