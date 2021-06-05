using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using KOS.GameplayTracking;

namespace KOS
{
    public class LongFormFeedbackUI : MonoBehaviour
    {
        public TMP_InputField feedbackText;

        public void GetLongFormFeedback(ref GameplayTracker.Feedback feedback)
        {
            feedback.LongFormFeedback = feedbackText.text;
        }
    }
}
