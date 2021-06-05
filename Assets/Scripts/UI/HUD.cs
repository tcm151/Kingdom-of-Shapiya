using UnityEngine;

namespace KOS.UI
{
    public class HUD : UIScreen
    {
        [SerializeField] private TestingButtons testingButtons;

        public void Start()
        {
            #if UNITY_EDITOR
            testingButtons.gameObject.SetActive(true);
            #else
            testingButtons.gameObject.SetActive(false);
            #endif
        }

        override public void GoBack()
        {
            //@ pause the game here maybe...
        }
    }
}