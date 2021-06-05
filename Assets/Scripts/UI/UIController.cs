
using UnityEngine;

namespace KOS.UI
{
    public class UIController : MonoBehaviour
    {
        [SerializeField] private GameObject mainMenu = null;
        [SerializeField] private GameObject settingsMenu = null;

        public void turnOnMainOffSetting()
        {
            mainMenu.SetActive(true);
            settingsMenu.SetActive(false);
        }

        public void turnOnSettingsOffMain()
        {
            settingsMenu.SetActive(true);
            mainMenu.SetActive(false);
        }

    }
}
