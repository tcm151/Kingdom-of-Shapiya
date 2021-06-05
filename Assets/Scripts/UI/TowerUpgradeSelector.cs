
using UnityEngine;

using KOS.Towers;

namespace KOS.UI
{
    public class TowerUpgradeSelector : MonoBehaviour
    {
        [SerializeField] private TowerFactory factory = default;
        [SerializeField] private ImageButton imageButton;
        [SerializeField] private GameObject TowerUpgradeScreen;
        [SerializeField] private GameObject TowerSelectScreen;

        private void Awake()
        {
            TowerData[] towers = factory.GetStats();
            foreach (var data in towers)
            {
                ImageButton button = Instantiate(imageButton) as ImageButton;
                button.transform.SetParent(this.transform, false);
                button.Set(data, data.image, data.name);
                button.Button.onClick.AddListener(SetInactive);
            }
        }

        public void SetInactive()
        {
            TowerSelectScreen.SetActive(false);
        }
    }
}
