using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using KOS.Enemies;

namespace KOS.Waves
{
    [System.Serializable]
    public class SpawnRule
    {
        public EnemyType enemyType;
        public int sequence;
        public int firstWave;
        public int baseAmount;
        public int frequency;
        public int increasePerWave;
        public float cooldown;

        //> GET AMOUNT AND TYPE OF ENEMIES FOR CURRENT WAVE
        public SpawnEntry GetWave(int currentWave)
        {
            if (currentWave < firstWave) return new SpawnEntry(sequence, enemyType); // return zero if zero wave

            if ((currentWave - firstWave) % frequency == 0)
            {
                int waveAmount = baseAmount;
                waveAmount += (currentWave - firstWave) / frequency * increasePerWave;
                // Debug.Log("Enemy: " + enemyType + " Amount: " + waveAmount);
                return new SpawnEntry(sequence, waveAmount, enemyType, cooldown);
            }

            return new SpawnEntry(sequence, enemyType); // if none spawning on this wave return zero
        }
    }
}
