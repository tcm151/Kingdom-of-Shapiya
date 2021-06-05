using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using KOS.Audio;
using KOS.Events;


namespace KOS.UI
{
    public class TransitionScreenController : MonoBehaviour
    {
        public GameObject youWinScreen;
        public GameObject gameOverScreen;
        public GameObject pauseScreen;
        public GameObject GUI;

        public AudioSource gameMusic;

        public bool canPause = true;
        public bool isPaused = false;
        private HUD HUD;

        public void Start()
        {
            EventManager.Active.onPause += PauseGame;
            EventManager.Active.onUnPause += UnPauseGame;
            EventManager.Active.onGameWon += ShowYouWinScreen;
            EventManager.Active.onGameOver += ShowGameOverScreen;
            EventManager.Active.onToggleCursorLock += ToggleCanPause;

            gameMusic = GetComponent<AudioSource>();
            HUD = GUI.GetComponent<HUD>();
        }

        private void Update()
        {
            if (Cursor.lockState != CursorLockMode.Locked) return;

            if ((Input.GetKeyDown(KeyCode.Escape)) && canPause && !isPaused) ShowPauseMenu();

            else if ((Input.GetKeyDown(KeyCode.Escape)) && isPaused) ClosePauseMenu();
        }

        public void Restart() => StartCoroutine(RestartCoroutine());

        private IEnumerator RestartCoroutine()
        {
            EventManager.Active.Restart();
            yield return new WaitForSecondsRealtime(1f);
            
            UnPauseGame();
            Bank.Connect.Reset();
            SceneManager.LoadScene(1);
        }

        public void MainMenu()
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(0);
        }

        private void ShowGameOverScreen()
        {
            PauseGame();
            gameMusic.Stop();
            gameOverScreen.SetActive(true);
        }

        private void ShowYouWinScreen()
        {
            PauseGame();
            gameMusic.Stop();
            AudioManager.Active.PlayOneShot("Victory");
            youWinScreen.SetActive(true);
        }

        public void CloseYouWinScreen()
        {
            UnPauseGame();
            youWinScreen.SetActive(false);
        }

        private void ShowPauseMenu()
        {
            EventManager.Active.OpenedPauseMenu();

            PauseGame();
            HUD.Hide();
            pauseScreen.SetActive(true);
            AudioManager.Active.PlayOneShot("Unpause");
        }

        private void PauseGame()
        {
            isPaused = true;
            Time.timeScale = 0;
            EventManager.Active.ToggleCursorLock();
        }

        public void ClosePauseMenu()
        {
            HUD.Show();
            pauseScreen.SetActive(false);
            youWinScreen.SetActive(false);

            UnPauseGame();
            AudioManager.Active.PlayOneShot("Pause");
        }

        private void UnPauseGame()
        {
            isPaused = false;
            Time.timeScale = 1;
            EventManager.Active.ToggleCursorLock();
        }

        public void KeepPlaying()
        {
            UnPauseGame();
            GUI.SetActive(true);
        }

        public void OpenUserStudy() => Time.timeScale = 0f;

        private void ToggleCanPause() => canPause = !canPause;
    }
}