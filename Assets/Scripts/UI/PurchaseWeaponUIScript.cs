
using TMPro;
using UnityEngine;
using UnityEngine.UI;

using KOS.Weapons;

namespace KOS.UI
{
    public class PurchaseWeaponUIScript : MonoBehaviour
    {
        public Color unlockableColor = new Color32(49,226,49,255);
        public Color lockedColor = new Color32(226,50,50,255);

        [SerializeField]
        private Weapon weapon;
        
        [SerializeField]
        private Image weaponImage;

        [SerializeField]
        private TextMeshProUGUI weaponName, unlockCost;

        private void Start()
        {
            weaponImage.sprite = weapon.Data.image;
            weaponName.text = weapon.Data.name;
            unlockCost.text = weapon.UnlockCost.ToString();
        }

        private void OnEnable()
        {
            unlockCost.text = weapon.UnlockCost.ToString();
        }

        private void Update()
        {
            unlockCost.color = (weapon.IsUnlockable()) ? unlockableColor : lockedColor;
        }

        public void PurchaseWeapon()
        {
            if (weapon.Unlock())
            {
                Debug.Log("Weapon " + weaponName + " unlocked!");
                this.gameObject.SetActive(false);
                // play new sound
            }
        }
    }
}
