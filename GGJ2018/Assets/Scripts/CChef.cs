using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CChef : MonoBehaviour, Jugador
{
    public Transform groundPoint;
    public LayerMask groundMask;
    public float groundDetectionDistance;
    public float jumpForce;
    public string horAxis;
    public string jumpAxis;

    private Rigidbody2D rb;
    private Animator anim;
    private bool isTouchingGround;
    private bool dblJumped;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    #region UnityCallbacks
    private void Update()
    {
        // detectar input horizontal
        movimiento();
        // detectar input vertical
        float verInput = Input.GetAxis(jumpAxis);
        if(verInput == 1f)
        {
            salto();
        }
        else if(verInput == -1f)
        {
            block();
        }
        // detectar si el jugador esta tocando el piso
        DetectPlayerTouchingGround();
    }
    #endregion

    public void movimiento()
    {
        
    }

    public void block()
    {
        Debug.Log("Block");
    }

    public void dab()
    {
        throw new NotImplementedException();
    }

    public void heavyAttack()
    {
        throw new NotImplementedException();
    }

    public void lightAttack()
    {
        throw new NotImplementedException();
    }

    public void salto()
    {
        if (isTouchingGround)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            Debug.Log("First Jump");
        }
        else if (!isTouchingGround && !dblJumped)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0f);
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            dblJumped = true;
            Debug.Log("Double jumped.");
        }
    }

    public void vergazo()
    {
        throw new NotImplementedException();
    }

    private void DetectPlayerTouchingGround()
    {
        isTouchingGround = Physics2D.OverlapCircle(groundPoint.position, groundDetectionDistance,
            groundMask);
        Debug.Log(isTouchingGround);
        if (isTouchingGround)
        {
            //animator.SetBool("Jumping", false);
            dblJumped = false;
        }
        //else animator.SetBool("Jumping", true);
    }
}
