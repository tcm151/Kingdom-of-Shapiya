using KOS.Events;
using TMPro;
using UnityEngine;

//! COLORS & colors 
//? COLORS & colors 
//@ COLORS & colors 
//$ COLORS & colors 
//# COLORS & colors  
//& COLORS & colors  
//% COLORS & colors  

//  colors & COLORS
//~ colors & COLORS
//- colors & COLORS
//+ colors & COLORS
//= colors & COLORS
//> colors & COLORS


namespace KOS.UI
{
    public class WaveTracker : MonoBehaviour
    {
        public enum Phase { GetReady, Incoming, Complete, Recovery, Reset, };

        public Color red;
        public Color orange;
        public Color green;

        [SerializeField] private TMP_Text waveNumber;
        [SerializeField] private TMP_Text waveStatus;

        private ProgressBar waveBar;
        private int wave, enemyAmount;
        private float recoveryPeriodProgress;
        private Phase phase;

        //> INITIALIZATION
        private void Awake()
        {
            waveBar = GetComponentInChildren<ProgressBar>();

            EventManager.Active.onWaveStarted += WaveStarted;
            EventManager.Active.onWaveEnded += WaveEnded;
            EventManager.Active.onEnemyKilled += EnemyKilled;
        }

        private void Update()
        {
            switch (phase)
            {
                case Phase.Complete:
                case Phase.Recovery:
                {
                    recoveryPeriodProgress += Time.deltaTime;
                    waveBar.SetValue(recoveryPeriodProgress);
                    if (recoveryPeriodProgress >= 4f && phase != Phase.Recovery) SetPhase(Phase.Recovery);
                    if (waveBar.value >= 0.50f * waveBar.max) waveBar.SetFillColor(orange);
                    break;
                }
                
                default: return;
            }
        }

        private void CheckGameOver()
        {
            //@ do this later
        }

        //> SET GAME STATE PHASE
        private void SetPhase(Phase newPhase)
        {
            this.phase = newPhase;

            (waveStatus.text, waveStatus.color) = phase switch
            {
                (Phase.GetReady) => ("Get Ready! Evil Shapes Inbound...", orange),
                (Phase.Incoming) => ("Evil Shapes Attacking!", red),
                (Phase.Complete) => ("Wave Complete!", green),
                (Phase.Recovery) => ("Prepare For The Next Wave...", orange),
                (Phase.Reset)    => ("WTF DO YOU WANT BRO...", red),
                _                => ("you shouldn't see this lol...", red),
            };
        }

        //> UPDATE HEALTH ON CHANGE
        private void EnemyKilled(string killer)
        {
            enemyAmount--;
            waveBar.SetValue(enemyAmount);

            if (waveBar.value < waveBar.min) EventManager.Active.CastleDestroyed(wave);

            if (waveBar.value <= 1.00f * waveBar.max && waveBar.value > 0.66f * waveBar.max) waveBar.SetFillColor(red);
            if (waveBar.value <= 0.66f * waveBar.max && waveBar.value > 0.33f * waveBar.max) waveBar.SetFillColor(orange);
            if (waveBar.value <= 0.33f * waveBar.max && waveBar.value > 0.00f * waveBar.max) waveBar.SetFillColor(green);
        }

        //> START OF WAVE PROCEDURE
        private void WaveStarted(int newWave, int newEnemyAmount)
        {
            SetPhase(Phase.Incoming);

            this.wave = newWave;
            waveNumber.text = $"WAVE {wave}";

            this.enemyAmount = newEnemyAmount;
            waveBar.SetMinMax(0, enemyAmount);
            waveBar.SetValue(enemyAmount);
            waveBar.SetFillColor(red);
        }

        //> END OF WAVE PROCEDURE
        private void WaveEnded(int wave, float recoveryPeriod)
        {
            SetPhase(Phase.Complete);
            recoveryPeriodProgress = 0f;
            waveBar.SetMinMax(0, recoveryPeriod);
            waveBar.SetFillColor(green);
            waveBar.SetValue(0);
        }
    }
}