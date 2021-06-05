
using KOS.Enemies;

namespace KOS.Waves
{
    public struct SpawnEntry
    {
        public int sequence;
        public EnemyType enemy;
        public int amount;
        public float cooldown;

        public SpawnEntry(int sequence, EnemyType enemy)
        {
            this.sequence = sequence;
            this.amount = 0;
            this.enemy = enemy;
            this.cooldown = 0;
        }
        
        public SpawnEntry(int sequence, int amount, EnemyType enemy, float cooldown)
        {
            this.sequence = sequence;
            this.amount = amount;
            this.enemy = enemy;
            this.cooldown = cooldown;
        }
    }
}
