using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using KOS.Events;

namespace KOS.UI
{
    public class ButtonPrompt : MonoBehaviour
    {
        [SerializeField] private GameObject buttonPrompt;
        [SerializeField] private TMPro.TextMeshProUGUI interaction;

        private void Awake()
        {
            EventManager.Active.onShowPopup += ShowPrompt;
        }

        private void ShowPrompt(bool truth, string text)
        {
            interaction.text = text;
            buttonPrompt.SetActive(truth);
        }
    }
}
