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
        [HideInInspector]
        public PlayerAttack Attack;
        
        private PlayerMovement _playerMovement;
        private PlayerAnimations _animations;
        
        private float _currentHp;
        private const float _IMMUNE_TIME = 1f;
        private float _lastImmune;

        public void OnEnable() {
            Input = GetComponent<InputHandler>();
            Mana = GetComponent<Mana>();
            Stamina = GetComponent<Stamina>();
            MainCamera = Camera.main;

            _playerMovement = GetComponent<PlayerMovement>();
            Attack = GetComponentInChildren<PlayerAttack>();
            _animations = GetComponentInChildren<PlayerAnimations>();

            _currentHp = Stats.MaxHp;
        }

        public void Update() {
            //if (GameStates.Instance.GetPauseState()) return;
            
            _playerMovement.TickUpdate();
            Input.TickUpdate();
            _animations.TickUpdate();
            Attack.TickUpdate();
        }

        public void FixedUpdate() {
            //if (GameStates.Instance.GetPauseState()) return;
            
            _playerMovement.FixedTickUpdate();
        }

        public void LateUpdate() {
            //if (GameStates.Instance.GetPauseState()) return;
            
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
