using UnityEngine;

using KOS.Events;
using KOS.GameplayTracking;

namespace KOS
{
    public class UserStudyUI : MonoBehaviour
    {
        public GameObject consentForm;
        public GameObject informationForm;
        public GameObject thankYouScreen;
        public GameObject cannedFeedbackForm;
        public GameObject longFormFeedbackForm;
        public GameObject debriefForm;

        public GameObject mainMenuButton;
        public GameObject keepPlayingButton;

        public GameplayTracker gameplayTracker;

        public bool toggledFromPause = false;

        private void Awake()
        {
            EventManager.Active.onToggleConsentForm += ToggleConsentForm;
            EventManager.Active.onToggleThankYouScreen += ToggleThankYouScreen;
            toggledFromPause = false;
        }

        public void GetInformation()
        {
            informationForm.GetComponent<InformationFormUI>().GetInformation(ref gameplayTracker.stats.Info);
            informationForm.GetComponent<InformationFormUI>().GetInformation(ref gameplayTracker.feedback.Info);
        }

        public void GetCannedFeedback()
        {
            cannedFeedbackForm.GetComponent<CannedFeedbackUI>().GetCannedFeedback(ref gameplayTracker.feedback);
        }

        public void GetLongFormFeedback()
        {
            longFormFeedbackForm.GetComponent<LongFormFeedbackUI>().GetLongFormFeedback(ref gameplayTracker.feedback);
        }

        public void ToggleConsentForm() => consentForm.SetActive(!consentForm.activeSelf);
        public void ToggleDebriefForm() => debriefForm.SetActive(!debriefForm.activeSelf);
        public void ToggleThankYouScreen() => thankYouScreen.SetActive(!thankYouScreen.activeSelf);
        public void ToggleInformationForm() => informationForm.SetActive(!informationForm.activeSelf);
        public void ToggleCannedFeedbackForm() => cannedFeedbackForm.SetActive(!cannedFeedbackForm.activeSelf);
        public void ToggleLongFormFeedbackForm() => longFormFeedbackForm.SetActive(!longFormFeedbackForm.activeSelf);


    }
}
