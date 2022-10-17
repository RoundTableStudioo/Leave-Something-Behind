using System;
using RoundTableStudio.Input;
using RoundTableStudio.Stats;
using UnityEngine;

namespace RoundTableStudio.Player
{
    public class PlayerManager : MonoBehaviour {
        [HideInInspector] 
        public InputHandler Input;
        public PlayerStats Stats;

        [HideInInspector]
        public ManaBar Mana;
        public Camera MainCamera;
        
        private PlayerMovement _playerMovement;
        private PlayerAttack _attack;

        public void Start() {
            _playerMovement = GetComponent<PlayerMovement>();
            Input = GetComponent<InputHandler>();
            _attack = GetComponentInChildren<PlayerAttack>();
            Mana = GetComponent<ManaBar>();
        }

        public void Update() {
            _playerMovement.TickUpdate();
            Input.TickUpdate();
            _attack.TickUpdate();
        }

        public void FixedUpdate() {
            _playerMovement.FixedTickUpdate();
        }

        public void LateUpdate() {
            Input.LateTickUpdate();
        }
    }
}
