

using KOS.Events;
using UnityEngine;

namespace KOS.Waves
{
    public class Wave : MonoBehaviour
    {
        // public enum Phase {Prepare, Defend};

        abstract private class Phase
        {
            abstract public void Begin();
            
            abstract protected void End();
        }

        private class Spawning : Phase
        {
            override public void Begin()
            {
                
            }

            override protected void End()
            {
                
            }
        }
        
        private class Replenishing : Phase
        {
            override public void Begin()
            {
                
            }

            override protected void End()
            {
                
            }
        }
        
        private void Awake()
        {
            EventManager.Active.onWaveStarted += WaveStarted;
            EventManager.Active.onWaveEnded += WaveEnded;
        }

        private void WaveEnded(int wave, float recoveryPeriod)
        {
            
        }

        public void WaveStarted(int newWave, int enemyAmount)
        {
            
        }
    }
}