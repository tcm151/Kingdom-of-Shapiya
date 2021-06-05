
using System.Collections.Generic;
using UnityEngine;

using KOS.Enemies;

namespace KOS.Factories
{
    [CreateAssetMenu(menuName = "Factories/Enemy Factory")]
    public class EnemyFactory : Factory
    {
        [SerializeField]
        private Enemy[] enemyPrefabs; // list of ALL enemy prefabs

        //> GET INSTANCE OF ENEMY BY ENEMY TYPE
        public Enemy Get(EnemyType index)
        {
            Enemy instance = CreateInstance(enemyPrefabs[(int)index]);
            instance.originFactory = this;
            return instance;
        }

        //> GET THE ENEMY STATS BY ENEMY TYPE
        public EnemyData GetStats(EnemyType index) => enemyPrefabs[(int)index].Data;
        
        //> GET ALL TOWER STATS
        public EnemyData[] GetStats()
        {
            var allStats = new List<EnemyData>();
            foreach (var enemy in enemyPrefabs) allStats.Add(enemy.Data);
            return allStats.ToArray();
        }
        
        //> RECLAIM AND DESTROY ENEMY
        public void Reclaim(Enemy enemy)
        {
            Debug.Assert(enemy.originFactory == this, "Wrong Factory Reclamation!");
            Destroy(enemy.gameObject);
        }
    }
}
