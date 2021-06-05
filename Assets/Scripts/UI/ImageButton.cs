
using TMPro;
using UnityEngine;
using UnityEngine.UI;

using KOS.Events;
using KOS.Towers;
using KOS.ItemData;

namespace KOS.UI
{
    public class ImageButton : MonoBehaviour
    {
        [SerializeField] private Data ItemData;
        [SerializeField] private Image itemImage;
        [SerializeField] private TextMeshProUGUI itemName;

        private Button button;
        public Button Button => button;

        virtual public void Set(Data ItemData, Sprite image, string name)
        {
            this.ItemData = ItemData;
            itemName.text = name;
            itemImage.sprite = image;

            button = gameObject.GetComponent<Button>();
            button.onClick.AddListener(ButtonPressed);
        }

        virtual protected void ButtonPressed()
        {
            EventManager.Active.SelectTowerToUpgrade((TowerData)ItemData);
        }
    }
}
