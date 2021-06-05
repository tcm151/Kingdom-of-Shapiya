
using TMPro;
using UnityEngine;
using UnityEngine.UI;

using KOS.Events;
using KOS.Towers;

namespace KOS.UI
{
    public class TowerSlotBroadcaster : MonoBehaviour
    {
        [SerializeField] private TowerFactory towerFactory;
        [SerializeField] private TowerType tower;
        private TowerData stats;

        [SerializeField] private Image towerImage;
        public TextMeshProUGUI towerName, towerCost;

        private void Start()
        {
            GetComponent<Button>().onClick.AddListener(Click);

            stats = towerFactory.GetStats(tower);
            towerImage.sprite = stats.image;
            towerName.text = stats.name;
            towerCost.text = stats.buildCost.ToString();
        }

        private void Click()
        {
            Debug.Log("Building "+ stats.name);

            EventManager.Active.ToggleBuildMenu();
            EventManager.Active.PlacingTower(tower);        
        }
    }
}
