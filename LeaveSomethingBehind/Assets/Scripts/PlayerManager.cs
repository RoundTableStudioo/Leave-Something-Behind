using System;
using UnityEngine;

namespace RoundTableStudio
{
    public class PlayerManager : MonoBehaviour {
        public PlayerStats Stats;
        
        private PlayerMovement _playerMovement;

        public void Start() {
            _playerMovement = GetComponent<PlayerMovement>();
        }

        public void Update() {
            _playerMovement.TickUpdate();
        }

        public void FixedUpdate() {
            _playerMovement.FixedTickUpdate();
        }
    }
}
