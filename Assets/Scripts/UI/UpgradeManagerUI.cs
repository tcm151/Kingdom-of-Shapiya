
using UnityEngine;

using KOS.Events;
using KOS.Towers;

namespace KOS.UI
{
    public class UpgradeManagerUI : MonoBehaviour
    {
        [SerializeField] private GameObject TowerUpgradeScreen;

        private void OnEnable()
        {
            EventManager.Active.onSelectTowerToUpgrade += OpenTowerUpgradeScreen;
        }

        private void OpenTowerUpgradeScreen(TowerData tower)
        {
            TowerUpgradeScreen.SetActive(true);
            TowerUpgradeScreen.GetComponent<TowerUpgradeController>().ChangeTowerSlot(tower);
        }
    }
}
