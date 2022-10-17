using RoundTableStudio.Input;
using RoundTableStudio.Shared;
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
            Input = GetComponent<InputHandler>();
            Mana = GetComponent<Mana>();
            Stamina = GetComponent<Stamina>();
            
            _playerMovement = GetComponent<PlayerMovement>();
            _attack = GetComponentInChildren<PlayerAttack>();
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
