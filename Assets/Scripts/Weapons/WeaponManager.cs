
using UnityEngine;

using KOS.Events;
using KOS.Towers;

namespace KOS.Weapons
{
    public class WeaponManager : MonoBehaviour
    {
        private Weapon[] weapons;
        private int current, last;

        [SerializeField]
        private GameObject hammer;

        private bool building;

        //> INITIALIZATION
        private void Awake()
        {
            // get all even if inactive
            weapons = GetComponentsInChildren<Weapon>(true);
            last = weapons.Length - 1;
            current = 0;
            building = false;

            // toggle off and on for some reason I can't remember
            foreach (var weapon in weapons)
            {
                weapon.gameObject.SetActive(true);
                weapon.gameObject.SetActive(false);
            }

            // set pistol as only active
            weapons[0].gameObject.SetActive(true);

            EventManager.Active.onPlacingTower += TakeOutHammer;
            EventManager.Active.onStoppedPlacingTower += PutAwayHammer;
        }

        //> TAKE OUT HAMMER
        private void TakeOutHammer(TowerType loldontneedthisboss)
        {
            building = true;
            weapons[current].gameObject.SetActive(false);
            hammer.SetActive(true);
        }

        //> PUT AWAY HAMMER
        private void PutAwayHammer()
        {
            building = false;
            hammer.SetActive(false);
            weapons[current].gameObject.SetActive(true);
        }

        //> CHECK FOR SCROLL WHEEL INPUT
        private void Update()
        {
            if (building) return;

            if (Input.GetAxis("Mouse ScrollWheel") < 0) NextWeapon();
            else if (Input.GetAxis("Mouse ScrollWheel") > 0) PreviousWeapon();
        }

        //> SWAP TO NEXT WEAPON
        private void NextWeapon()
        {
            weapons[current].gameObject.SetActive(false);

            // try to swap to weapon only if it is unlocked        
            for (int i = 0; i < weapons.Length; i++)
            {
                if (current == last) current = 0; else current++;

                if (weapons[current].Unlocked)
                {
                    weapons[current].gameObject.SetActive(true);
                    EventManager.Active.WeaponSwitchedTo(weapons[current].Data.weaponType);
                    return;
                }
            }   
        }

        //> SWAP TO PREVIOUS WEAPON
        private void PreviousWeapon()
        {
            weapons[current].gameObject.SetActive(false);

            // try to swap to weapon only if it is unlocked   
            for (int i = 0; i < weapons.Length; i++)
            {        
                if (current == 0) current = last; else current--;

                if (weapons[current].Unlocked)
                {
                    weapons[current].gameObject.SetActive(true);
                    EventManager.Active.WeaponSwitchedTo(weapons[current].Data.weaponType);
                    return;
                }
            }
        }
    }
}
