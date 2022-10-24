using System;
using RoundTableStudio.Input;
using RoundTableStudio.Shared;
using System.Collections;
using RoundTableStudio.Core;
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
        public Life Life;
        [HideInInspector]
        public PlayerAttack Attack;
        
        private PlayerMovement _playerMovement;
        private PlayerAnimations _animations;
        
        private const float _IMMUNE_TIME = 1f;
        private float _lastImmune;
        
        private void OnEnable() {
            Mana = GetComponent<Mana>();
            Stamina = GetComponent<Stamina>();
            Life = GetComponent<Life>();
            MainCamera = Camera.main;

            _playerMovement = GetComponent<PlayerMovement>();
            Attack = GetComponentInChildren<PlayerAttack>();
            _animations = GetComponentInChildren<PlayerAnimations>();
        }

        private void Start() {
            Input = InputHandler.Instance;
        }

        public void Update() {
            if (GameManager.Instance.IsGamePaused()) return;
            
            _playerMovement.TickUpdate();
            Input.TickUpdate();
            _animations.TickUpdate();
            Attack.TickUpdate();
            Life.TickUpdate();
        }

        public void FixedUpdate() {
            if (GameManager.Instance.IsGamePaused()) return;
            
            _playerMovement.FixedTickUpdate();
        }

        public void LateUpdate() {
            if (GameManager.Instance.IsGamePaused()) return;
            
            Input.LateTickUpdate();
        }

        private void TakeDamage(Damage damage) {
            if (Time.time - _lastImmune < _IMMUNE_TIME) return;

            _lastImmune = Time.time;

            if (damage.isPhysical) damage.Amount *= 1 - Stats.PhysicalDefense;
            if (damage.isMagical) damage.Amount *= 1 - Stats.MagicDefense;
            
            if (Life.LoseHp(damage.Amount)) {
                Die();
            }
            
            _playerMovement.PushDirection = (transform.position - damage.PushOrigin).normalized * damage.PushForce;
            StartCoroutine(_animations.ChangePlayerColor(Color.red));
            _playerMovement.Damaged = true;
        }

        private void Die() {
            GameManager.Instance.EndGame();
        }

        private void OnCollisionEnter2D(Collision2D collision) {
            if (!collision.collider.CompareTag("Corruption")) return;

            Damage corruptionDamage = new Damage {
                Amount = 1f,
                PushOrigin = collision.collider.ClosestPoint(transform.position),
                PushForce = 25f,
                isMagical = false,
                isPhysical = false
            };

            _animations.ChangePlayerColor(Color.magenta);
            TakeDamage(corruptionDamage);
        }
    }
}
