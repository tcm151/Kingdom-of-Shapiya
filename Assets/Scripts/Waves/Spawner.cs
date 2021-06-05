
using System;

using UnityEngine;

using KOS.Audio;
using KOS.Events;
using KOS.Points;
using KOS.Enemies;
using KOS.Factories;

namespace KOS.Waves
{
    public class Spawner : MonoBehaviour
    {    
        [Header("Factory")]
        [SerializeField]
        private EnemyFactory enemyFactory = default;
        private SpawnQueue spawnQueue;
        private EnemyList enemyList;
        
        [Header("Ruleset")]
        public SpawnRuleset ruleset = default;
        
        [SerializeField] private Spawnpoint[] spawnpoints;
        [SerializeField] private Checkpoint[] checkpoints;

        private float recoveryProgress, spawnPeriod, spawnSpeed, spawnSpeedMultiplier, spawnCooldown, enemyCooldown;
        private bool waveInProgress, active;
        private int numEnemies;
        
        private string[] voiceLines;
        private bool gameOver;
        private int wave;

        public int Wave => wave;

        //> INITIALIZATION
        private void Awake()
        {
            gameOver = false;

            // points of interest
            checkpoints = GetComponentsInChildren<Checkpoint>();
            spawnpoints = GetComponentsInChildren<Spawnpoint>();

            // lists
            enemyList = new EnemyList();
            spawnQueue = new SpawnQueue();

            // register for events
            EventManager.Active.onRestart += Reset;
            EventManager.Active.onGameOver += GameOver;
            // EventManager.Active.onEnemyKilled += EnemyKilled;
            EventManager.Active.onToggleSpawning += ToggleSpawning;
            EventManager.Active.onTryForceNextWave += TryForceNextWave;
            
            // set voice lines
            voiceLines = new []
            {
                "voiceConcaveAngles",
                "voiceGeometryHomework",
                "voiceHolyHex",
                "voiceLookingObtuse",
                "voiceLunchBreak",
                "voiceSquarePlusDirt",
                "voiceUltimateFrisbee"
            };
        }

        //> RESET ON ENABLE
        private void OnEnable() => Reset();

        //> RESET THE PARAMETERS AND LISTS
        private void Reset()
        {
            // Debug.Log("RESETTING SPAWNER!");
            gameOver = false;

            spawnQueue.Reset();
            enemyList.Reset();

            wave = 0;
            active = true;
            waveInProgress = false;
            recoveryProgress = 0;

            spawnSpeed = ruleset.spawnSpeed;
            spawnSpeedMultiplier = ruleset.spawnSpeedMultiplier;
        }
        
        //> PREVENT SPAWNING WHEN GAME OVER
        private void GameOver() => gameOver = true;

        //> REMOVE ENEMY FROM LIST WHEN KILLED
        private void EnemyKilled(Enemy enemy) => enemyList.Remove(enemy);

        //> TOGGLE SPAWNING OF ENEMIES
        private void ToggleSpawning()
        {
            active = !active;
            Debug.Log("Spawning is " + active);
        }

        //> ATTEMPT TO FORCE NEXT WAVE
        private void TryForceNextWave()
        {
            if (waveInProgress) return;

            recoveryProgress += 1000;
            EventManager.Active.ForcedNextWave();
            Debug.Log("FORCED NEXT WAVE!");
        }

        //> MANAGE SPAWNING OF WAVES
        private void Update()
        {
            if (!active) return; //> cancel if not spawning

            if (waveInProgress) SpawnEnemies();
            else
            if ((recoveryProgress += Time.deltaTime) > ruleset.recoveryPeriod) // wait until recovery period over
            {
                spawnPeriod = 0;
                recoveryProgress = 0;
                spawnCooldown = 60f / spawnSpeed;
                
                wave++;
                waveInProgress = true;
                numEnemies = ruleset.QueueWave(ref spawnQueue, wave);
                
                Debug.Log("STARTING WAVE #" + wave + "!");
                Debug.Log("NUMBER OF ENEMIES: " + numEnemies);

                EventManager.Active.WaveStarted(wave, numEnemies);
                
                // play start of round voiceline
                AudioManager.Active.PlayOneShot(voiceLines[UnityEngine.Random.Range(0, voiceLines.Length)]);
            }
        }

        private void SpawnEnemies()
        {
            // if no more enemies to spawn, and none left alive; finish the wave
            if (spawnQueue.IsEmpty && enemyList.IsEmpty && !gameOver)
            {
                spawnQueue.Reset();
                waveInProgress = false;
                spawnSpeed *= spawnSpeedMultiplier;

                EventManager.Active.WaveEnded(wave, ruleset.recoveryPeriod);
                
                Debug.Log("WAVE " + wave + " COMPLETED!");

                return;
            }

            // spawn at desired spawn interval
            spawnPeriod += Time.deltaTime;
            if ((spawnPeriod >= spawnCooldown && spawnPeriod >= enemyCooldown) && !spawnQueue.IsEmpty)
            {
                var (enemyType, enemyCooldown) = spawnQueue.Next();
                this.enemyCooldown = enemyCooldown;
                Enemy enemy = enemyFactory.Get(enemyType);
                enemy.onDeath += EnemyKilled;
                enemyList.Add(enemy);

                Spawnpoint spawnpoint = spawnpoints[UnityEngine.Random.Range(0, spawnpoints.Length)];
                enemy.SetTarget(spawnpoint.NextTarget.Position);
                enemy.SpawnAt(spawnpoint.Position);
                spawnPeriod = 0;
            }
        }
    }
}
