using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

using KOS.GameplayTracking;

namespace KOS
{
    public class InformationFormUI : MonoBehaviour
    {
        public TMP_InputField ageInput;
        public Slider FPSSlider;
        public Slider TDSlider;
        public ToggleGroup inputMethod;
        public ToggleGroup operatingSystem;

        public void GetInformation(ref Info info)
        {
            info.age = ageInput.text;
            info.FPSEXP = (int)FPSSlider.value;
            info.TDEXP = (int)TDSlider.value;
            
            Toggle[] inputMethodToggles = inputMethod.GetComponentsInChildren<Toggle>();
            foreach (var toggle in inputMethodToggles)
            {
                if (toggle.isOn) info.inputMethod = toggle.gameObject.name;
            }

            Toggle[] operatingSystemToggles = operatingSystem.GetComponentsInChildren<Toggle>();
            foreach (var toggle in operatingSystemToggles)
            {
                if (toggle.isOn) info.OS = toggle.gameObject.name;
            }
        }
    }
}
