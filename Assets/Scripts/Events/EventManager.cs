using System;

using UnityEngine;

using KOS.Towers;
using KOS.Weapons;
using KOS.Enemies;
using KOS.PowerUps;

namespace KOS.Events
{
    
    public class EventManager : MonoBehaviour //? convert to a ScriptableObject
    {
        //> SINGLE INSTANCE
        static private EventManager instance;
        static public EventManager Active
        {
            get
            {
                if (!instance)
                {
                    Debug.LogError("No EventManager in scene!");
                    Debug.Break();
                }
                return instance;
            }
        }

        //> CONSTRUCTOR
        private void Awake() => instance = this;


        //> TRACKING ===================================================================================================
        public event Action onSaveStats;
        public void SaveStats() => onSaveStats?.Invoke();

        public event Action onSaveFeedback;
        public void SaveFeedback() => onSaveFeedback?.Invoke();

        public event Action onResetStats;
        public void ResetStats() => onResetStats?.Invoke();
        //> ============================================================================================================



        //> BANK =======================================================================================================
        public event Action<int> onBalanceChanged;
        public void BalanceChanged(int balance) => onBalanceChanged?.Invoke(balance);

        public event Action<int> onDeposited;
        public void Deposited(int increase) => onDeposited?.Invoke(increase);
        
        public event Action<int> onCharged;
        public void Charged(int decrease) => onCharged?.Invoke(decrease);
        //> ============================================================================================================



        //> WAVES ======================================================================================================
        public event Action<int, int> onWaveStarted;
        public void WaveStarted(int wave, int numEnemies) => onWaveStarted?.Invoke(wave, numEnemies);

        public event Action<int, float> onWaveEnded;
        public void WaveEnded(int wave, float recoveryPeriod) => onWaveEnded?.Invoke(wave, recoveryPeriod);

        public event Action onToggleSpawning;
        public void ToggleSpawning() => onToggleSpawning?.Invoke();

        public event Action onTryForceNextWave;
        public void TryForceNextWave() => onTryForceNextWave?.Invoke();
        
        public event Action onForcedNextWave;
        public void ForcedNextWave() => onForcedNextWave?.Invoke();

        public event Action<int> onCastleDestroyed;
        public void CastleDestroyed(int wave) => onCastleDestroyed?.Invoke(wave);
        //> ============================================================================================================



        //> BUILDING ===================================================================================================
        public event Action onToggleBuildMenu;
        public void ToggleBuildMenu() => onToggleBuildMenu?.Invoke();

        public event Action<TowerType> onPlacingTower;
        public void PlacingTower(TowerType tower) => onPlacingTower?.Invoke(tower);

        public event Action onStoppedPlacingTower;
        public void StoppedPlacingTower() => onStoppedPlacingTower?.Invoke();

        public event Action<TowerType> onTowerBuilt;
        public void TowerBuilt(TowerType tower) => onTowerBuilt?.Invoke(tower);

        public event Action<TowerType> onTowerDestroyed;
        public void TowerDestroyed(TowerType tower) => onTowerDestroyed?.Invoke(tower);
        //> ============================================================================================================



        //> POWERUPS ===================================================================================================
        public event Action<PowerUpType> onPowerUpCollected;
        public void PowerUpCollected(PowerUpType powerUp) => onPowerUpCollected?.Invoke(powerUp);
        //> ============================================================================================================




        //> TOWERS =====================================================================================================
        public event Action<TowerData> onSelectTowerToUpgrade;
        public void SelectTowerToUpgrade(TowerData tower) => onSelectTowerToUpgrade?.Invoke(tower);
        
        public event Action<TowerType> onTowerUpgraded;
        public void TowerUpgraded(TowerType tower) => onTowerUpgraded?.Invoke(tower);
        //> ============================================================================================================



        public event Action onRestart;
        public void Restart() => onRestart?.Invoke();



        //> MENUS ======================================================================================================
        public event Action<bool, string> onShowPopup;
        public void ShowPopup(bool truth, string text) => onShowPopup?.Invoke(truth, text);

        public event Action onGameOver;
        public void GameOver() => onGameOver?.Invoke();

        public event Action onGameWon;
        public void GameWon() => onGameWon?.Invoke();

        public event Action onToggleConsentForm;
        public void ToggleConsentForm() => onToggleConsentForm?.Invoke();

        public event Action onToggleThankYouScreen;
        public void ToggleThankYouScreen() => onToggleThankYouScreen?.Invoke();

        public event Action onPause;
        public void Pause() => onPause?.Invoke();

        public event Action onUnPause;
        public void UnPause() => onUnPause?.Invoke();

        public event Action onOpenedPauseMenu;
        public void OpenedPauseMenu() => onOpenedPauseMenu?.Invoke();

        public event Action onOpenedSettingsMenu;
        public void OpenedSettingsMenu() => onOpenedSettingsMenu?.Invoke();
        
        public event Action onOpenedControlsScreen;
        public void OpenedControlsScreen() => onOpenedControlsScreen?.Invoke();

        public event Action onOpenedGameplayScreen;
        public void OpenedGameplayScreen() => onOpenedGameplayScreen?.Invoke();
        
        public event Action onQuitToMainMenu;
        public void QuitToMainMenu() => onQuitToMainMenu?.Invoke();

        public event Action onOpenedBugReportScreen;
        public void OpenedBugReportScreen() => onOpenedBugReportScreen?.Invoke();

        public event Action onSentBugReport;
        public void SentBugReport() => onSentBugReport?.Invoke();

        public event Action<Tower> onOpenTowerOptions;
        public void OpenTowerOptions(Tower tower) => onOpenTowerOptions?.Invoke(tower);

        public event Action onChangedFOV;
        public void ChangedFOV() => onChangedFOV?.Invoke();

        public event Action onChangedVolume;
        public void ChangedVolume() => onChangedVolume?.Invoke();

        public event Action onChangedDifficulty;
        public void ChangedDifficulty() => onChangedDifficulty?.Invoke();

        public event Action onMouseSensitivityChanged;
        public void MouseSensitivityChanged() => onMouseSensitivityChanged?.Invoke();
        //> ============================================================================================================



        //> WEAPONS ====================================================================================================
        public event Action<WeaponData> onWeaponFired;
        public void WeaponFired(WeaponData weaponData) => onWeaponFired?.Invoke(weaponData);

        public event Action<WeaponType> onWeaponSwitchedTo;
        public void WeaponSwitchedTo(WeaponType weapon) => onWeaponSwitchedTo?.Invoke(weapon);

        public event Action<WeaponData> onSelectWeaponToUpgrade;
        public void SelectWeaponToUpgrade(WeaponData stats) => onSelectWeaponToUpgrade?.Invoke(stats);

        public event Action<WeaponType> onWeaponUpgraded;
        public void WeaponUpgraded(WeaponType weapon) =>  onWeaponUpgraded?.Invoke(weapon);

        public event Action<WeaponType> onWeaponPurchased;
        public void WeaponPurchased(WeaponType weapon) => onWeaponPurchased?.Invoke(weapon);
        //> ============================================================================================================



        //> PLAYER =====================================================================================================
        public event Action onJump;
        public void Jumped() => onJump?.Invoke();

        public event Action<bool> onSprinting;
        public void Sprinting(bool truth) => onSprinting?.Invoke(truth);
        //> ============================================================================================================



        //> CAMERA =====================================================================================================
        public event Action onToggleCursorLock;
        public void ToggleCursorLock() => onToggleCursorLock?.Invoke();

        public event Action<float> onSetMouseSensitivity;
        public void SetMouseSensitivity(float sensitivity) => onSetMouseSensitivity?.Invoke(sensitivity);

        public event Action<float> onSetFieldOfView;
        public void SetFieldOfView(float fov) => onSetFieldOfView?.Invoke(fov);
        //> ============================================================================================================



        //> ENEMIES ====================================================================================================
        public event Action<string> onEnemyKilled;
        public void EnemyKilled(string enemy) => onEnemyKilled?.Invoke(enemy);

        public event Action<int, int> onCastleHit;
        public void CastleHit(int damage, int wave) => onCastleHit?.Invoke(damage, wave);

        public event Action<float> onCastleHealthChanged;
        public void CastleHealthChanged(float health) => onCastleHealthChanged?.Invoke(health);
        //> ============================================================================================================

        
    }
}
