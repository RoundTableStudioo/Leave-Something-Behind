using UnityEngine;

namespace RoundTableStudio.Shared {
    public class GameStates : MonoBehaviour {
        public static GameStates Instance;
        private void Awake() {
            if (Instance != null) return;

            Instance = this;
        }

        private bool _gamePaused;
        private bool _gameStarted;
        private bool _gameEnded;

        public void SetPauseState(bool state) {
            _gamePaused = state;
        }

        public void SetStartedState(bool state) {
            _gameStarted = state;
        }

        public void SetEndedState(bool state) {
            _gameEnded = state;
        }

        public bool GetPauseState() {
            return _gamePaused;
        }

        public bool GetStartedState() {
            return _gameStarted;
        }

        public bool GetEndedState() {
            return _gameEnded;
        }
    }
}
