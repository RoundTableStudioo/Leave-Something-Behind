using RoundTableStudio.Sound;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RoundTableStudio.Core
{
    public enum GameStates {
        Menu,
        Started,
        Paused,
        Ended
    }
    
    public class GameManager : MonoBehaviour
    {
        #region Singleton

        public static GameManager Instance;

        private void Awake() {
            if (Instance != null) return;

            Instance = this;
            
            DontDestroyOnLoad(gameObject);
        }

        #endregion

        public GameStates GameState;
        public int GameTime = 15;

        public void StopMainTheme() {
            SoundManager.Instance.Stop("MainTheme");
        }

        public bool IsGamePaused() {
            return GameState == GameStates.Paused;
        }

        public bool IsGameEnded() {
            return GameState == GameStates.Ended;
        }

        public void SetGameState(GameStates state) {
            GameState = state;
        }

        public void StartGame() {
            GameState = GameStates.Paused;
            SoundManager.Instance.Play("MainTheme");
        }

        public void EndGame() {
            GameState = GameStates.Ended;
            SoundManager.Instance.Stop("MainTheme");
        }

        public void WinGame() {
            GameState = GameStates.Ended;
            SoundManager.Instance.Stop("MainTheme");
            SoundManager.Instance.Play("Final");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
