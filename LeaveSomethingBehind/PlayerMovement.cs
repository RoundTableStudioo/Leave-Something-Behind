using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    
    
    public float VelocidadMov = 5f;

    public Rigidbody2D rb;
    public Animator animator;

    Vector2 movimiento;


    // Update is called once per frame
    void Update()
    {
        movimiento.x = Input.GetAxisRaw("Horizontal");
        movimiento.y = Input.GetAxisRaw("Vertical");

        animator.SetFloat("Horizontal", movimiento.x);
        animator.SetFloat("Vertical", movimiento.y);
        animator.SetFloat("Velocidad", movimiento.sqrMagnitude);
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movimiento * VelocidadMov * Time.fixedDeltaTime);
    }

}
