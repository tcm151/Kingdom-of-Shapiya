
using TMPro;
using UnityEngine;
using UnityEngine.UI;

using KOS.Weapons;

namespace KOS.UI
{
    public class UpgradeWeaponSlotScript : MonoBehaviour
    {
        [SerializeField] private WeaponData weapon;
        [SerializeField] private Image weaponImage;
        [SerializeField] private TextMeshProUGUI weaponName;
        [SerializeField] private WeaponUpgradeController upgrader;

        private void OnEnable()
        {
            weaponName.text = weapon.name; 
            weaponImage.sprite = weapon.image; 
        }

        public void SetActiveWeapon()
        {
            upgrader.ChangeWeaponSlot(weapon);
        }
    }
}
