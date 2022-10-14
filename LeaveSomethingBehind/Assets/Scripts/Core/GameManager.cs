using UnityEngine;
using RoundTableStudio.Player;

namespace RoundTableStudio.Core
{
    public class GameManager : MonoBehaviour
    {
        #region Singleton

        public static GameManager Instance;

        private void Awake() {
            if (Instance != null) return;

            Instance = this;
        }

        #endregion

        public PlayerManager Player;
    }
}
