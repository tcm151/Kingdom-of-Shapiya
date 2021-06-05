using KOS.Events;
using UnityEngine;

namespace KOS.UI
{
    public class CastleHealthBar : UIScreen
    {
        [SerializeField] private ProgressBar healthBar;

        public Color red;
        public Color orange;
        public Color green;

        public float minHealth, maxHealth;

        private int wave;
        
        //> INITIALIZATION
        override public void GoBack()
        {
            //@ do nothing...
        }

        override protected void Awake()
        {
            base.Awake();
            
            healthBar = GetComponentInChildren<ProgressBar>();
            healthBar.SetMinMax(minHealth, maxHealth);

            EventManager.Active.onWaveStarted += WaveStarted;
            EventManager.Active.onCastleHealthChanged += UpdateHealth;
        }

        //> KEEP TRACK OF CURRENT WAVE
        private void WaveStarted(int newWave, int e) => wave = newWave;

        //> UPDATE HEALTH ON CHANGE
        private void UpdateHealth(float newHealth)
        {
            healthBar.SetValue(newHealth);
            
            if (healthBar.value < healthBar.min) EventManager.Active.CastleDestroyed(wave);
            
            if (healthBar.value >= 0.66f * healthBar.max) healthBar.SetFillColor(red);
            if (healthBar.value <= 0.66f * healthBar.max) healthBar.SetFillColor(orange);
            if (healthBar.value <= 0.33f * healthBar.max) healthBar.SetFillColor(green);
        }
    }
}