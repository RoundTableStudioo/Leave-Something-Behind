using System;
using RoundTableStudio.Input;
using RoundTableStudio.Stats;
using UnityEngine;

namespace RoundTableStudio.Player
{
    public class PlayerManager : MonoBehaviour {
        public PlayerStats Stats;
        public Camera MainCamera;
        [HideInInspector] 
        public InputHandler Input;
        [HideInInspector]
        public Mana Mana;
        [HideInInspector] 
        public Stamina Stamina;
        
        private PlayerMovement _playerMovement;
        private PlayerAttack _attack;

        public void Start() {
            _playerMovement = GetComponent<PlayerMovement>();
            Input = GetComponent<InputHandler>();
            _attack = GetComponentInChildren<PlayerAttack>();
            Mana = GetComponent<Mana>();
            Stamina = GetComponent<Stamina>();
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
