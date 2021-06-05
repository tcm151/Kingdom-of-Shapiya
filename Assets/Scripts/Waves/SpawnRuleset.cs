using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KOS.Waves
{
    [CreateAssetMenu(menuName = "Waves/Spawn Ruleset")]
    public class SpawnRuleset : ScriptableObject
    {
        public float recoveryPeriod = 45f;
        public float spawnSpeed = 60f;
        public float spawnSpeedMultiplier = 1.15f;

        public SpawnRule[] spawnRules;

        public int QueueWave(ref SpawnQueue spawnQueue, int currentWave)
        {
            foreach (var rule in spawnRules)
            {
                var spawnEntry = rule.GetWave(currentWave);
                spawnQueue.AddMany(spawnEntry);
            }
            spawnQueue.Shuffle();
            return spawnQueue.Count;
        }
    }
}
