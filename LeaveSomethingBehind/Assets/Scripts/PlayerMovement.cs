using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RoundTableStudio {
    public class PlayerMovement : MonoBehaviour
    {
        private PlayerManager _manager;
        private Rigidbody2D _rb;
        private Vector2 _movement;
        
        private void Start() {
            _rb = GetComponent<Rigidbody2D>();
            _manager = GetComponent<PlayerManager>();
        }

        public void TickUpdate()
        {
            _movement.x = Input.GetAxisRaw("Horizontal");
            _movement.y = Input.GetAxisRaw("Vertical");
        }

        public void FixedTickUpdate() {
            float movementSpeed = _manager.Stats.Speed;

            _rb.MovePosition(_rb.position + _movement * (movementSpeed * Time.fixedDeltaTime));
        }

    }
}

