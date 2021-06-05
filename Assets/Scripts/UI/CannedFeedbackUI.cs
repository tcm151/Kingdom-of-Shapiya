using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using KOS.GameplayTracking;

namespace KOS.GameplayTracking
{
    public class CannedFeedbackUI : MonoBehaviour
    {
        public Slider difficultySlider;
        public Slider enemyVarietySlider;
        public Slider weaponVarietySlider;
        public Slider weaponUseabilitySlider;
        public Slider towerVarietySlider;
        public Slider funnynessSlider;
        public Slider enjoymentSlider;

        public ToggleGroup favoriteWeapon;
        public ToggleGroup favoriteTower;

        public void GetCannedFeedback(ref GameplayTracker.Feedback feedback)
        {
            feedback.Difficulty = (int)difficultySlider.value;
            feedback.EnemyVariety = (int)enemyVarietySlider.value;
            feedback.WeaponVariety = (int)weaponVarietySlider.value;
            feedback.WeaponUseability = (int)weaponUseabilitySlider.value;
            feedback.TowerVariety = (int)towerVarietySlider.value;
            feedback.Funnyness = (int)funnynessSlider.value;
            feedback.Enjoyment = (int)enjoymentSlider.value;


            Toggle[] weaponToggles = favoriteWeapon.GetComponentsInChildren<Toggle>();
            foreach (var toggle in weaponToggles)
            {
                if (toggle.isOn) feedback.FavouriteWeapon = toggle.gameObject.name;
            }
            
            Toggle[] towerToggles = favoriteTower.GetComponentsInChildren<Toggle>();
            foreach (var toggle in towerToggles)
            {
                if (toggle.isOn) feedback.FavouriteTower = toggle.gameObject.name;
            }
        }
    }
}
