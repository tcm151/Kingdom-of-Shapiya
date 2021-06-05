using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace KOS.UI
{
    public class MainMenuUIController : MonoBehaviour
    {
        [SerializeField] private GameObject mainMenu = null;
        [SerializeField] private GameObject settingsMenu = null;
        private int menuTracker = 1; // 1 = main menu, 2 = settings

        public void showMainMenu()
        {
            mainMenu.SetActive(true);
            settingsMenu.SetActive(false);
            menuTracker = 1;
        }

        public void showSettingMenu()
        {
            settingsMenu.SetActive(true);
            mainMenu.SetActive(false);
            menuTracker = 2;

        }

        public void play()
        {
            SceneManager.LoadScene(3);
        }
        
        public void quit()
        {
            Application.Quit();
        }

        private void Update()
        {
            if (menuTracker == 2)
            {
                if (Input.GetKey("escape"))
                {
                    showMainMenu();
                }
            }
        }
    }
}
