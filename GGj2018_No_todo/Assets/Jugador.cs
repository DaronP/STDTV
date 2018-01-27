using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(BoxCollider2D))]


public class Jugador : MonoBehaviour
{

    Animator animator;

    [System.Serializable]
    public class AnimationSettings
    {
        public string horizontalVelocityFloat = "Horizontal";
        public string jumpBool = "jump";
        public string groundedBool = "isGrounded";
        public string lightAttack = "lightAttack";
        public string heavyAttack = "heavyAttack";
        public string block = "block";
        public string dab = "dab";
    }
    [SerializeField]
    public AnimationSettings animations;

    [System.Serializable]
    public class MovimientoSettings
    {
        public bool isBlocking = false;
        public float factorVelocidad = 21.3f;
    }
    [SerializeField]
    public MovimientoSettings movement;

    [System.Serializable]
    public class JumpSettings
    {
        public Transform groundPoint;
        public LayerMask groundMask;
        public float groundDetectionDistance = 0.1f;
        public float jumpForce = 5.0f;
    }
    [SerializeField]
    public JumpSettings jump;

    [System.Serializable]
    public class Health
    {
        public int golpeRecivido = 0;
        public int golpeDab = 0;
        public int golpeDado = 0;
    }
    [SerializeField]
    public Health vida;


    private Rigidbody2D rb;
    private Animator anim;
    private bool isTouchingGround;

    Vector3 jugadorTras;
    private int direccion;

    // Use this for initialization
    void Start()
    {
        jugadorTras = new Vector3(0.0f, 0.0f, 0.0f);

        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        movimiento();
        salto();
        block();

        // detectar si el jugador esta tocando el piso
        DetectPlayerTouchingGround();
    }

    void movimiento()
    {
        if (movement.isBlocking == true)
        {
            return;
        }

        jugadorTras.y = 0;
        jugadorTras.x = 0;
        jugadorTras.z = 0;

        if (Input.GetAxis("Horizontal") > 0)
        {
            jugadorTras.x = movement.factorVelocidad * Time.deltaTime;
            transform.Translate(jugadorTras);
            direccion = 1;
        }
        if (Input.GetAxis("Horizontal") < 0)
        {
            jugadorTras.x = -movement.factorVelocidad * Time.deltaTime;
            transform.Translate(jugadorTras);
            direccion = -1;
        }

    }
    void salto()
    {
        if (Input.GetAxis("Vertical") > 0)
        {
            if (isTouchingGround)
            {
                rb.AddForce(Vector2.up * jump.jumpForce, ForceMode2D.Impulse);
            }
        }
    }

    private void DetectPlayerTouchingGround()
    {
        isTouchingGround = Physics2D.OverlapCircle(jump.groundPoint.position, jump.groundDetectionDistance,
            jump.groundMask);

        if (isTouchingGround)
        {
            //animator.SetBool("Jumping", false);
        }
        //else animator.SetBool("Jumping", true);
    }
    void block()
    {
        movement.isBlocking = false;

        if (Input.GetAxis("Vertical") < 0)
        {
            movement.isBlocking = true;

        }
    }
    void lightAttack() { }
    void dab() { }
    void vergazo(int hit)
    {
        if (movement.isBlocking == true)
        {
            return;
        }
        vida.golpeRecivido = vida.golpeRecivido + hit;
    }
}