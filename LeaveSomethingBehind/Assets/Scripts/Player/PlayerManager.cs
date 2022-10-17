using RoundTableStudio.Input;
using RoundTableStudio.Shared;
using System.Collections;
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

        [SerializeField]
        private float _currentHp;
        private const float _IMMUNE_TIME = 1f;
        private float _lastImmune;

        public void Start() {
            Input = GetComponent<InputHandler>();
            Mana = GetComponent<Mana>();
            Stamina = GetComponent<Stamina>();
            
            _playerMovement = GetComponent<PlayerMovement>();
            _attack = GetComponentInChildren<PlayerAttack>();

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
