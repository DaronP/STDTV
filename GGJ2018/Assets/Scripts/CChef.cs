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

    private Vector3 jugadorTras;
    private float factorVelocidad;
    private Rigidbody2D rb;
    private Animator anim;
    private bool isTouchingGround;
    private bool dblJumped;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        jugadorTras = new Vector3(0.0f, 0.6f, 0.0f);
        factorVelocidad = 21.3f;
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
        jugadorTras.y = 0;
        jugadorTras.x = 0;
        jugadorTras.z = 0;

        if (Input.GetAxis("Horizontal") > 0f)
        {
            jugadorTras.x = factorVelocidad * Time.deltaTime;
            transform.Translate(jugadorTras);
        }
        if (Input.GetAxis("Horizontal") < 0f)
        {
            jugadorTras.x = -factorVelocidad * Time.deltaTime;
            transform.Translate(jugadorTras);
        }

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
        }
        //else animator.SetBool("Jumping", true);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isTouchingGround = true;
    }
}
