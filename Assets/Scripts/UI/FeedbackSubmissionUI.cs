using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using TMPro;

namespace KOS.UI
{
    public class FeedbackSubmissionUI : MonoBehaviour
    {
        [SerializeField] private GameObject submissionScreen, thankYouScreen;

        [SerializeField] public TMP_InputField textBox;
        [SerializeField] public Button submitButton;

        static private string bugReportFormURL = "https://docs.google.com/forms/d/e/1FAIpQLScphF8_8iUedWEE9c6LtS9imfwsT9zkV1zSjTiDRxjr07P-KQ/formResponse";
        static private string reportID = "entry.1200120593";

        private void OnEnable()
        {
            submissionScreen.SetActive(true);
            thankYouScreen.SetActive(false);

            submitButton.onClick.AddListener
            (
                delegate
                {
                    StartCoroutine(SendFormData(textBox.text));
                    submissionScreen.SetActive(false);
                    thankYouScreen.SetActive(true);
                }
            );
        }

        static private IEnumerator SendFormData<T>(T dataItem)
        {
            string itemData = (dataItem is string) ? dataItem.ToString() : JsonUtility.ToJson(dataItem);

            WWWForm form = new WWWForm();
            form.AddField(reportID, itemData);
            using (UnityWebRequest www = UnityWebRequest.Post(bugReportFormURL, form))
            {
                yield return www.SendWebRequest();
                Debug.Log("Bug Report Sent!");
            }
        }

        static public void OpenLink(string link)
        {
            bool googleSearch = link.Contains("google.com/search");
            string linkNoSpaces = link.Replace(" ", googleSearch ? "+" : "%20");
            
            Application.OpenURL(linkNoSpaces);
        }
    }
}
