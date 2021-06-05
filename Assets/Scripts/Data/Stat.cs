
using System;
using UnityEngine;

namespace KOS.ItemData
{
    [Serializable]
    public class Stat
    {
        public string name;

        public int level;
        public int maxLevel;
        public float defaultValue;
        public float value;
        public float upgradeAmount;
        public float defaultUpgradeCost;
        public float upgradeCost;
        public float costMultiplier;
        
        public bool upgradeable = true;

        public bool CanUpgrade() => (level < maxLevel) && Bank.Connect.HasBalance((int)upgradeCost);

        public bool Upgrade()
        {
            if (!CanUpgrade()) return false;

            Bank.Connect.Charge((int)upgradeCost);
            value += upgradeAmount;
            level++;
            upgradeCost *= costMultiplier;
            return true;
        }

        public void SetDefault()
        {
            this.level = 0;
            this.value = defaultValue;
            this.upgradeCost = defaultUpgradeCost; 
        }
    }
}
