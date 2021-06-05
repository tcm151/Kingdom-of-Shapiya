using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace KOS.UI
{
    public class MenuController : MonoBehaviour
    {
        private int volume = 0;
        private int difficulty = 10;

        private void Start()
        {
            PlayerPrefs.SetInt("volume", volume);
            PlayerPrefs.SetInt("difficulty", difficulty);
            PlayerPrefs.Save();

            SceneManager.LoadScene("BKs2ndScene");

        }

        private void Update()
        {

        }
    }
}
