using System.Collections;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using KOS.Audio;

namespace KOS.UI
{
    public class OpeningAnimation : MonoBehaviour
    {
        public Animator transition;
        public Animator logoTransition;
        public GameObject yesButton;
        public GameObject noButton;
        public Button skipButton;
        public GameObject videoCanvasPlayer = null;

        public int timeBeforeVideoActive = 3;
        public int timeBeforeBookImageActive = 10;
        public int timeForFirstStoryPartToBeRead = 30;
        public int timeForStoryToDisappear = 2;
        public int timeForSecondStoryToBeRead = 30;
        public int timeForTransitionBeforeGameLoad = 2;

        public GameObject onceUponText;
        public GameObject storyText1;
        public GameObject storyText2;
        public GameObject mainmenuBackground;

        public AudioManager AudioManager;

        public GameObject bookCanvas;

        private void Start()
        {
            //Button startButtonbutton = startButton.GetComponent<Button>();
            //startButtonbutton.onClick.AddListener(PlayButtonClick);

            Button yesButtonbutton = yesButton.GetComponent<Button>();
            yesButtonbutton.onClick.AddListener(YesButtonClick);

            Button noButtonbutton = noButton.GetComponent<Button>();
            noButtonbutton.onClick.AddListener(NoButtonClick);

            AudioManager.Active.PlayOneShot("MainMenu");

            PlayLogoAnimation();
        }

        public void PlayButtonClick()
        {
            StartCoroutine(StartAnimation());
        }

        public void PlayLogoAnimation()
        {
            logoTransition.SetTrigger("FadeIn");
            //logoTransition.SetTrigger("StartMenuChange");
            //logoTransition.SetTrigger("LogoZoom");
        }

        private IEnumerator StartAnimation()
        {
            transition.SetTrigger("StartOpening");
            AudioManager.Active.Stop(0);
            AudioManager.Active.PlayOneShot("introMusic");
            
            yield return new WaitForSecondsRealtime(timeBeforeVideoActive);

            videoCanvasPlayer.SetActive(true);
            mainmenuBackground.SetActive(false);
            
            //startButton.SetActive(false);

            yield return new WaitForSecondsRealtime(timeBeforeBookImageActive);

            bookCanvas.SetActive(true);
            videoCanvasPlayer.SetActive(false);

            yield return new WaitForSecondsRealtime(timeForFirstStoryPartToBeRead);

            onceUponText.SetActive(false);
            storyText1.SetActive(false);

            yield return new WaitForSecondsRealtime(timeForStoryToDisappear);

            storyText2.SetActive(true);

            yield return new WaitForSecondsRealtime(timeForSecondStoryToBeRead);

            yesButton.SetActive(true);
            noButton.SetActive(true);






        }

        public void YesButtonClick()
        {
            StartCoroutine(LoadGame());
        }

        public void NoButtonClick()
        {
            StartCoroutine(LoadGame());
        }

        public void SkipButtonClick()
        {
            StartCoroutine(LoadGame());
            AudioManager.Active.Stop(0);
        }

        private IEnumerator LoadGame()
        {
            transition.SetTrigger("LoadGame");
            yield return new WaitForSecondsRealtime(timeForTransitionBeforeGameLoad);
            SceneManager.LoadScene(1);
        }

        public void QuitGame()
        {
            Application.Quit();
        }


    }
}
