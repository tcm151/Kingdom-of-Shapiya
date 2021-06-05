using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace KOS.UI
{
    public class SettingsController : MonoBehaviour
    {
        public Slider volumeSlider;
        public Slider difficultySlider;
        public Slider fovSlider;
        public Slider mouseSlider;
        //public AudioListener audioListener;

        private void Start()
        {
            AudioListener.volume = PlayerPrefs.GetFloat("volume");
        }

        private void OnEnable()
        {
            difficultySlider.value = PlayerPrefs.GetInt("difficulty");
            volumeSlider.value = PlayerPrefs.GetFloat("volume");
        }

        public void ChangeDifficulty()
        {
            PlayerPrefs.SetInt("difficulty", (int)difficultySlider.value);
            PlayerPrefs.Save();
        }

        public void ChangeVolume()
        {
            PlayerPrefs.SetFloat("volume", volumeSlider.value);
            AudioListener.volume = volumeSlider.value;
            PlayerPrefs.Save();
        }

        public void ChangeFOV()
        {
            PlayerPrefs.SetFloat("fov", fovSlider.value);
            PlayerPrefs.Save();
        }

        public void ChangeMouseSensitivity()
        {
            PlayerPrefs.SetFloat("mouseSensitivity", mouseSlider.value);
            PlayerPrefs.Save();
        }
    }
}
