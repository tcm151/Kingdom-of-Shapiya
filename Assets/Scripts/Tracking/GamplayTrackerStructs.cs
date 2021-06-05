using System;
using System.Collections;
using System.Collections.Generic;

namespace KOS.GameplayTracking
{
    [Serializable]
    public struct Info
    {
        public string age;
        public int FPSEXP;
        public int TDEXP;
        public string inputMethod;
        public string OS;
    }

    [Serializable]
    public struct Player
    {
        public int timesJumped;
        public float timeSpentSprinting;
    }

    [Serializable]
    public struct Hexes
    {
        public int amountEarned;
        public int amountSpent;
    }

    [Serializable]
    public struct Waves
    {
        public int damageTaken;
        public int wavesCompleted;
        public int timesForcedNextWave;
        public HashSet<int> wavesTakenDamageHashSet;
        public int[] wavesTakenDamage;
        public int waveFailed;
    }

    [Serializable]
    public struct Weapons
    {
        public Weapon Pistol;
        public Weapon Shotgun;
        public Weapon Revolver;
        public Weapon Rifle;
        public Weapon RocketLauncher;
    }

    [Serializable]
    public struct Weapon
    {
        public bool purchased;
        public float timeUsed;
        public int timesFired;
        public int kills;
        public int timesUpgraded;
    };

    [Serializable]
    public struct Towers
    {
        public int TotalBuilt;
        public int TotalRefunded;
        public TowerS BasicI;
        public TowerS BasicII;
        public TowerS HeavyI;
        public TowerS HeavyII;
        public TowerS MortarI;
        public TowerS AntiAirI;
        public TowerS SlowI;
    }

    [Serializable]
    public struct TowerS
    {
        public int amountBuilt;
        public int amountRefunded;
        public int kills;
        public int timesUpgraded;
    };

    public struct Enemies
    {
        public int totalKilled;
    }

    [Serializable]
    public struct Powerups
    {
        public int healthPowerupsCollected;
        public int speedPowerupsCollected;
        public int damagePowerupsCollected;
        public int currencyPowerupsCollected;
    }

    [Serializable]
    public struct UI
    {
        public bool openedPauseMenu;
        public bool openedSettingsMenu;
        public bool openedControlsScreen;
        public bool openedGameplayScreen;
        public bool openedBugReportScreen;
        public bool openedTowerOptionsMenu;
        public int timesOpenedTowerOptionsMenu;
        public bool quitToMainMenu;
        public int bugReportsSent;
    }

    [Serializable]
    public struct Settings
    {
        public bool changedFOV;
        public bool changedVolume;
        public bool changedDifficulty;
        public bool changedMouseSensitivity;
    }
}