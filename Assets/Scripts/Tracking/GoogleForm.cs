using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Networking;

namespace KOS
{
    public class GoogleForm : MonoBehaviour
    {
        private string formURL;

        //> CONVERT AN ID:DATA DICTIONARY TO A WWW FORM
        static public WWWForm CreateForm(Dictionary<string, string> dataContainer)
        {
            string entryID, data;
            WWWForm response = new WWWForm();

            foreach (var entry in dataContainer)
            {
                entryID = entry.Key;
                data = entry.Value;
                response.AddField(entryID, data);
            }

            return response;
        }
        
        //> ATTEMPT TO SUBMIT RESPONSE TO FORM
        public void SubmitResponse(string formURL, WWWForm response) => StartCoroutine(SubmitResponseCoroutine(formURL, response));

        //> COROUTINE FOR ASYNCHRONOUS BEHAVIOUR
        static private IEnumerator SubmitResponseCoroutine(string formURL, WWWForm response)
        {
            using (UnityWebRequest www = UnityWebRequest.Post(formURL, response))
            {
                yield return www.SendWebRequest();
                Debug.Log("Response Sent!");
            }
        }

        //> OPEN A LINK
        static public void OpenLink(string URL)
        {
            bool isGoogleSearch = URL.Contains("google.com/search");
            string properURL = URL.Replace(" ", (isGoogleSearch) ? "+" : "%20");
            Application.OpenURL(properURL);
        }

    }
}

