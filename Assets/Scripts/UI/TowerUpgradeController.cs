
using TMPro;
using UnityEngine;
using UnityEngine.UI;

using KOS.Events;
using KOS.Towers;

namespace KOS.UI
{
    public class TowerUpgradeController : MonoBehaviour
    {
        private TowerData stats;

        [SerializeField] private Image towerImage;
        [SerializeField] private TextMeshProUGUI towerName;

        [SerializeField] private GameObject statEntry1;
        [SerializeField] private TextMeshProUGUI damageUpgradeCost;
        [SerializeField] private TextMeshProUGUI damageLevel;
        [SerializeField] private TextMeshProUGUI damageValue;
        [SerializeField] private GameObject statEntry2;
        [SerializeField] private TextMeshProUGUI bulletSpeedUpgradeCost;
        [SerializeField] private TextMeshProUGUI bulletSpeedLevel;
        [SerializeField] private TextMeshProUGUI bulletSpeedValue;
        [SerializeField] private GameObject statEntry3;
        [SerializeField] private TextMeshProUGUI towerRangeUpgradeCost;
        [SerializeField] private TextMeshProUGUI towerRangeLevel;
        [SerializeField] private TextMeshProUGUI towerRangeValue;
        [SerializeField] private GameObject statEntry4;
        [SerializeField] private TextMeshProUGUI rotationSpeedUpgradeCost;
        [SerializeField] private TextMeshProUGUI rotationSpeedLevel;
        [SerializeField] private TextMeshProUGUI rotationSpeedValue;
        [SerializeField] private GameObject statEntry5;
        [SerializeField] private TextMeshProUGUI fireRateUpgradeCost;
        [SerializeField] private TextMeshProUGUI fireRateLevel;
        [SerializeField] private TextMeshProUGUI fireRateValue;


        static private Color green = new Color32(0,255,0,255), red = new Color32(255,0,0,255);

        private void Update() => Refresh();

        private void OnEnable()
        {
            EventManager.Active.onSelectTowerToUpgrade += ChangeTowerSlot;
        }

        public void ChangeTowerSlot(TowerData newStats)
        {
            Debug.Log("GOT BUTTON!");
            stats = newStats;
            towerName.text = stats.name;
            towerImage.sprite = stats.image;

            Refresh();
        }

        private void Refresh()
        {
            damageUpgradeCost.color = (stats.projectileDamage.CanUpgrade()) ? green : red;
            bulletSpeedUpgradeCost.color = (stats.projectileSpeed.CanUpgrade()) ? green : red;
            towerRangeUpgradeCost.color = (stats.range.CanUpgrade()) ? green : red;
            rotationSpeedUpgradeCost.color = (stats.rotationSpeed.CanUpgrade()) ? green : red;
            fireRateUpgradeCost.color = (stats.fireRate.CanUpgrade()) ? green : red;

            if (stats.projectileDamage.upgradeable == false) statEntry1.SetActive(false);
            else statEntry1.SetActive(true);
            if (stats.projectileSpeed.upgradeable == false) statEntry2.SetActive(false);
            else statEntry2.SetActive(true);
            if (stats.range.upgradeable == false) statEntry3.SetActive(false);
            else statEntry3.SetActive(true);
            if (stats.rotationSpeed.upgradeable == false) statEntry4.SetActive(false);
            else statEntry4.SetActive(true);
            if (stats.fireRate.upgradeable == false) statEntry5.SetActive(false);
            else statEntry5.SetActive(true);

            damageUpgradeCost.text = Mathf.RoundToInt(stats.projectileDamage.upgradeCost).ToString();
            damageLevel.text = stats.projectileDamage.level.ToString();
            damageValue.text = stats.projectileDamage.value.ToString("G4");
            bulletSpeedUpgradeCost.text = Mathf.RoundToInt(stats.projectileSpeed.upgradeCost).ToString();
            bulletSpeedLevel.text = stats.projectileSpeed.level.ToString();
            bulletSpeedValue.text = stats.projectileSpeed.value.ToString("G4");
            towerRangeUpgradeCost.text = Mathf.RoundToInt(stats.range.upgradeCost).ToString();
            towerRangeLevel.text = stats.range.level.ToString();
            towerRangeValue.text = stats.range.value.ToString("G4");
            rotationSpeedUpgradeCost.text = Mathf.RoundToInt(stats.rotationSpeed.upgradeCost).ToString();
            rotationSpeedLevel.text = stats.rotationSpeed.level.ToString();
            rotationSpeedValue.text = stats.rotationSpeed.value.ToString("G4");
            fireRateUpgradeCost.text = Mathf.RoundToInt(stats.fireRate.upgradeCost).ToString();
            fireRateLevel.text = stats.fireRate.level.ToString();
            fireRateValue.text = stats.fireRate.value.ToString("G4");
        }

        public void UpgradeDamage()
        {
            EventManager.Active.TowerUpgraded(stats.towerType);
            stats.projectileDamage.Upgrade();
        }


        public void UpgradeBulletSpeed()
        {
            EventManager.Active.TowerUpgraded(stats.towerType);
            stats.projectileSpeed.Upgrade();
        }
        
        public void UpgradeTowerRange()
        {
            EventManager.Active.TowerUpgraded(stats.towerType);
            stats.range.Upgrade();
        }

        public void UpgradeRotationSpeed()
        {
            EventManager.Active.TowerUpgraded(stats.towerType);
            stats.rotationSpeed.Upgrade();
        }

        public void UpgradeFirerate()
        {
            EventManager.Active.TowerUpgraded(stats.towerType);
            stats.fireRate.Upgrade();
        }
    }
}
