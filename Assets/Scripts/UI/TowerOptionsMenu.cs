
using UnityEngine;
using UnityEngine.UI;

using KOS.Events;
using KOS.Towers;

namespace KOS.UI
{
    public class TowerOptionsMenu : MonoBehaviour
    {
        [SerializeField] private Button destroyAndRefundButton;
        [SerializeField] private Image first, last, strongest, weakest, random;

        [SerializeField] private BallisticTower currentTower;
        public void SetTower(BallisticTower newTower) => currentTower = newTower;

        private Color selected = new Color32(68, 250, 95, 255);

        private void Update()
        {
            if (currentTower is BallisticTower)
            {
                first.color = ((int)currentTower.targetMode == 0) ? selected : Color.white;
                last.color = ((int)currentTower.targetMode == 1) ? selected : Color.white;
                strongest.color = ((int)currentTower.targetMode == 2) ? selected : Color.white;
                weakest.color = ((int)currentTower.targetMode == 3) ? selected : Color.white;
                random.color = ((int)currentTower.targetMode == 4) ? selected : Color.white;
            }
        }

        public void SetTargetMode(int newTargetMode)
        {
            currentTower.targetMode = (TargetMode)newTargetMode;
        }

        public void RefundTower()
        {
            EventManager.Active.TowerDestroyed(currentTower.Data.towerType);
            Bank.Connect.Deposit(currentTower.Data.buildCost);
            // currentTower.hexCell.occupied = false;
            currentTower.originFactory.Reclaim(currentTower);
            this.gameObject.SetActive(false);
        }

        public void ToggleTowerRange()
        {
            currentTower.ToggleRange();
        }

    }
}
