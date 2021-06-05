using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

namespace KOS.UI
{
    public class TypeWriterTextScript : MonoBehaviour 
    {
        private TMP_Text txt;
        private string story;
        public float timeBetweenChars = 0.250f;
        public float timeBeforeStartChars = 0.0f;

        private void Awake () 
        {
            txt = GetComponent<TMPro.TextMeshProUGUI>();
            story = txt.text;
            txt.text = "";

            // TODO: add optional delay when to start
            StartCoroutine ("PlayText");
        }

        private IEnumerator PlayText()
        {
            yield return new WaitForSeconds(timeBeforeStartChars);

            foreach (char c in story) 
            {
                txt.text += c;
                yield return new WaitForSeconds (timeBetweenChars);
            }
        }

    }
}
