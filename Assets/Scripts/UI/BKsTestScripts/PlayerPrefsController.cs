using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace KOS.UI
{
    public class PlayerPrefsController : MonoBehaviour
    {
        private int volume;
        private int difficulty; // 1 = easy, 2 = medium, 3 = hard

        [SerializeField] private GameObject volumeSlider = null;
        [SerializeField] private GameObject difficultySlider = null;

        private void Start()
        {
            difficulty = 2;
        }

        public void updateVolume()
        {
            volume = (int)volumeSlider.GetComponent<Slider>().value;
            PlayerPrefs.SetInt("volume", volume);
            PlayerPrefs.Save();
        }

        public void updateDifficulty()
        {
            difficulty = (int)difficultySlider.GetComponent<Slider>().value;
            PlayerPrefs.SetInt("difficulty", difficulty);
            PlayerPrefs.Save();
        }
    }
}
