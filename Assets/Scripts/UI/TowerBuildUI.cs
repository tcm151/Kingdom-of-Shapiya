
using UnityEngine;

using KOS.Events;
using KOS.Towers;


namespace KOS.UI
{
    public class TowerBuildUI : MonoBehaviour
    {
        [SerializeField] private GameObject towerBuildMenu;
        [SerializeField] private TowerOptionsMenu towerOptionsMenu;
        private bool towerBuildMenuActive = false;

        private void Awake()
        {
            EventManager.Active.onToggleBuildMenu += ToggleBuildMenu;
            EventManager.Active.onOpenTowerOptions += OpenTowerOptionsMenu;
        }

        private void ToggleBuildMenu()
        {
            towerBuildMenuActive = !towerBuildMenuActive;
            towerBuildMenu.SetActive(towerBuildMenuActive);
        }

        private void OpenTowerOptionsMenu(Tower tower)
        {
            towerOptionsMenu.SetTower((BallisticTower)tower);
            towerOptionsMenu.gameObject.SetActive(true);
        }
    }
}
