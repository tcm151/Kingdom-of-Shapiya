
using System.Collections.Generic;
using System;

using UnityEngine;

using KOS.Enemies;

namespace KOS.Waves
{
    [Serializable]
    public class SpawnQueue
    {
        //- INTERNAL QUEUE AND LISTS
        private Queue<(EnemyType, float)> spawnQueue;
        private List<(EnemyType, float)> sequence1;
        private List<(EnemyType, float)> sequence2;
        private List<(EnemyType, float)> sequence3;
        private List<(EnemyType, float)> sequence4;
        
        //> CONSTRUCTOR
        public SpawnQueue()
        {
            spawnQueue = new Queue<(EnemyType, float)>();
            sequence1 = new List<(EnemyType, float)>();
            sequence2 = new List<(EnemyType, float)>();
            sequence3 = new List<(EnemyType, float)>();
            sequence4 = new List<(EnemyType, float)>();
        }

        //> RESET ALL INTERNAL LISTS
        public void Reset()
        {
            spawnQueue.Clear();
            sequence1.Clear();
            sequence2.Clear();
            sequence3.Clear();
            sequence4.Clear();
        }

        //> STATE CHECKS
        public int Count => spawnQueue.Count;
        public bool IsEmpty => spawnQueue.Count == 0;

        //> GET NEXT & PEEK NEXT
        public (EnemyType, float) Next() => spawnQueue.Dequeue();
        public (EnemyType, float) Peek() => spawnQueue.Peek();

        //> SHUFFLE THE SEQUENCES INTO THE QUEUE
        public void Shuffle()
        {
            List.Shuffle(sequence1);
            List.Shuffle(sequence2);
            List.Shuffle(sequence3);
            List.Shuffle(sequence4);

            foreach (var item in sequence1) spawnQueue.Enqueue(item);
            foreach (var item in sequence2) spawnQueue.Enqueue(item);
            foreach (var item in sequence3) spawnQueue.Enqueue(item);
            foreach (var item in sequence4) spawnQueue.Enqueue(item);
        }

        //> ADD ENEMY TO SEQUENCE LIST
        private void Add(int sequence, EnemyType nextEnemy, float cooldown)
        {
            switch (sequence)
            {
                case 1:
                    sequence1.Add((nextEnemy, cooldown));
                    return;
                
                case 2:
                    sequence2.Add((nextEnemy, cooldown));
                    return;
                
                case 3:
                    sequence3.Add((nextEnemy, cooldown));
                    return;
                
                case 4:
                    sequence4.Add((nextEnemy, cooldown));
                    return;
            }
        }
        
        public void AddMany(SpawnEntry entry)
        {
            for (int i = 0; i < entry.amount; i++) Add(entry.sequence, entry.enemy, entry.cooldown);
        }

    }
}