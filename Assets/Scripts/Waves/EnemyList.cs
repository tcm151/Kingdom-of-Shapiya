
using System.Collections.Generic;

using UnityEngine;

using KOS.Enemies;

namespace KOS.Waves
{
    public class EnemyList
    {
        //- INTERNAL LIST
        private List<Enemy> enemies;

        //> CONSTRUCTOR
        public EnemyList()
        {
            enemies = new List<Enemy>();
            enemies.Clear();
        }

        //> EMPTY CHECK
        public bool IsEmpty => enemies.Count == 0;

        //> CLEAR LIST OF ALL ENEMIES
        public void Reset() => enemies.Clear();

        //> ADD ENEMY
        public void Add(Enemy newEnemy) => enemies.Add(newEnemy);

        //> REMOVE ENEMY AND TRIM
        public void Remove(Enemy deadEnemy)
        {
            enemies.Remove(deadEnemy);
            enemies.TrimExcess();
        }
    }
}
