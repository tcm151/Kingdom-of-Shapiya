using System.Collections.Generic;

using UnityEngine;

using KOS.Factories;

namespace KOS.Towers
{
    [CreateAssetMenu(menuName = "Factories/Tower Factory")]
    public class TowerFactory : Factory
    {
        [SerializeField]
        private Tower[] towerPrefabs;
        
        //> GET INSTANCE OF TOWER BY TOWER TYPE
        public Tower Get(TowerType index)
        {
            Tower instance = CreateInstance(towerPrefabs[(int)index]);
            instance.originFactory = this;
            return instance;
        }

        //> GET TOWER STATS BY TOWER TYPE
        public TowerData GetStats(TowerType index)
        {
            return towerPrefabs[(int)index].Data;
        }

        //> GET ALL TOWER STATS
        public TowerData[] GetStats()
        {
            List<TowerData> allStats = new List<TowerData>();
            foreach (var tower in towerPrefabs) allStats.Add(tower.Data);
            return allStats.ToArray();
        }

        //> RECLAIM AND DESTORY TOWER
        public void Reclaim(Tower tower)
        {
            Debug.Assert(tower.originFactory == this, "Wrong Factory Reclamation!");
            Destroy(tower.gameObject);
        }
    }
}
