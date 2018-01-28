using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(BoxCollider2D))]


public class Jugador : MonoBehaviour
{
    public int playerNumber;
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

    /*void Awake()
    {
        animator = GetComponent<Animator>();
        SetupAnimator();
    }*/

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
        var inputDevice = (InputManager.Devices.Count > playerNumber) ? InputManager.Devices[playerNumber] : null;
        if (inputDevice != null)
        {
            movimiento(inputDevice);
            salto(inputDevice);
            block(inputDevice);
            lightAttack(inputDevice);
            dab(inputDevice);
        }
        // detectar si el jugador esta tocando el piso
        DetectPlayerTouchingGround();
    }

    public void Animate(float horizontal)
    {
        
    }

    void movimiento(InputDevice dev)
    {
        if (movement.isBlocking == true)
        {
            return;
        }

        jugadorTras.y = 0;
        jugadorTras.x = 0;
        jugadorTras.z = 0;

        if (dev.LeftStickX.Value > 0)
        {
            jugadorTras.x = movement.factorVelocidad * Time.deltaTime;
            transform.Translate(jugadorTras);
            direccion = 1;
        }
        if (dev.LeftStickX.Value < 0)
        {
            jugadorTras.x = -movement.factorVelocidad * Time.deltaTime;
            transform.Translate(jugadorTras);
            direccion = -1;
        }

    }
    void salto(InputDevice dev)
    {
        if (dev.LeftStickY.Value > 0)
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
    void block(InputDevice dev)
    {
        movement.isBlocking = false;
        if (isTouchingGround == false)
        {
            return;
        }

        if (dev.LeftStickY.Value < 0)
        {
            movement.isBlocking = true;

        }
    }
    void lightAttack(InputDevice dev)
    {

    }
    void dab(InputDevice dev)
    {

    }
    void vergazo(int hit)
    {
        if (movement.isBlocking == true)
        {
            return;
        }
        vida.golpeRecivido = vida.golpeRecivido + hit;
    }

    void SetupAnimator()
    {
        Animator wantedAnim = GetComponentsInChildren<Animator>()[1];
        Avatar wantedAvater = wantedAnim.avatar;

        animator.avatar = wantedAvater;
        Destroy(wantedAnim);
    }
}