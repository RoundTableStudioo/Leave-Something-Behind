using UnityEngine;

namespace RoundTableStudio.Player {
    public class PlayerMovement : MonoBehaviour {
        [HideInInspector] 
        public Vector2 PushDirection;
        [HideInInspector] 
        public bool Damaged;
        
        private PlayerManager _manager;
        private Rigidbody2D _rb;
        private Vector2 _movement;
        private Vector2 _mousePosition;
        
        private void OnEnable() {
            _rb = GetComponent<Rigidbody2D>();
            _manager = GetComponent<PlayerManager>();
        }

        public void TickUpdate() {
            _movement.x = _manager.Input.Movement.x;
            _movement.y = _manager.Input.Movement.y;

            _mousePosition = _manager.MainCamera.ScreenToWorldPoint(_manager.Input.MousePosition);
        }

        public void FixedTickUpdate() {
            Vector2 playerMovement = _movement * (_manager.Stats.Speed * Time.fixedDeltaTime);
            
            if (!Damaged)
                _rb.MovePosition(_rb.position + playerMovement);
            else {
                _rb.MovePosition(_rb.position + PushDirection * Time.fixedDeltaTime);
                Damaged = false;
            }

            Vector2 lookDirection = _mousePosition - _rb.position;
            float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg + 90f;
        }
    }
}

