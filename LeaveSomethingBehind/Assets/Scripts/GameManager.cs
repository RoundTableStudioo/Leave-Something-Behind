using System;
using UnityEngine;

namespace RoundTableStudio
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
