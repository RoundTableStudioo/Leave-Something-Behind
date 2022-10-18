using RoundTableStudio.Input;
using RoundTableStudio.Shared;
using System.Collections;
using UnityEngine;

namespace RoundTableStudio.Player
{
    public class PlayerManager : MonoBehaviour {
        public PlayerStats Stats;
        [HideInInspector]
        public Camera MainCamera;
        [HideInInspector] 
        public InputHandler Input;
        [HideInInspector]
        public Mana Mana;
        [HideInInspector] 
        public Stamina Stamina;
        
        private PlayerMovement _playerMovement;
        private PlayerAttack _attack;
        private PlayerAnimations _animations;
        
        private float _currentHp;
        private const float _IMMUNE_TIME = 1f;
        private float _lastImmune;

        public void Start() {
            Input = GetComponent<InputHandler>();
            Mana = GetComponent<Mana>();
            Stamina = GetComponent<Stamina>();
            MainCamera = Camera.main;

            _playerMovement = GetComponent<PlayerMovement>();
            _attack = GetComponentInChildren<PlayerAttack>();
            _animations = GetComponentInChildren<PlayerAnimations>();

            _currentHp = Stats.MaxHp;
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

        private void TakeDamage(Damage damage) {
            if (Time.time - _lastImmune < _IMMUNE_TIME) return;

            _lastImmune = Time.time;
            _currentHp -= damage.Amount;
            
            _playerMovement.PushDirection = (transform.position - damage.PushOrigin).normalized * damage.PushForce;
            StartCoroutine(_animations.ChangePlayerColor(Color.red));
            _playerMovement.Damaged = true;

            if (_currentHp <= 0) {
                Die();
            }
        }

        private void Die() {
            Debug.LogWarning("TO DO - Finish the game");
        }
    }
}
