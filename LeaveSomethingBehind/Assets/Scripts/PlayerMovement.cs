using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RoundTableStudio {
    public class PlayerMovement : MonoBehaviour
    {
        public float MovementSpeed = 5f;
        //public Animator animator;

        private Rigidbody2D _rb;
        private Vector2 _movement;


        private void Start() {
            _rb = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            _movement.x = Input.GetAxisRaw("Horizontal");
            _movement.y = Input.GetAxisRaw("Vertical");

            // animator.SetFloat("Horizontal", movimiento.x);
            // animator.SetFloat("Vertical", movimiento.y);
            // animator.SetFloat("Velocidad", movimiento.sqrMagnitude);
        }

        private void FixedUpdate()
        {
            _rb.MovePosition(_rb.position + _movement * (MovementSpeed * Time.fixedDeltaTime));
        }

    }
}

