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
    public Animator animator;
    public LayerMask otros;
    public Collider2D hurtbox;

    private bool animationPlaying;
    private float curAnimTime = 0f;

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

    public BoxCollider2D lightAttackerRight;
    public BoxCollider2D lightAttackerLeft;
    private bool isAttacking;
    private float lightAttackActiveTimer = 1f;
    private float lightAttackRecover = 1f;

    private Rigidbody2D rb;
    private bool isTouchingGround;

    Vector3 jugadorTras;
    private int direccion;

    void Awake()
    {
        animator = GetComponent<Animator>();
        //SetupAnimator();
    }

    // Use this for initialization
    void Start()
    {
        jugadorTras = new Vector3(0.0f, 0.0f, 0.0f);

        rb = GetComponent<Rigidbody2D>();
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

        // testing animacion
        if(animationPlaying)
        {
            curAnimTime += Time.deltaTime;
            
            if(curAnimTime > animator.GetCurrentAnimatorClipInfo(0)[0].clip.length)
            {

            }
        }
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

        if (dev.DPadRight.IsPressed)
        {
            jugadorTras.x = movement.factorVelocidad * Time.deltaTime;
            transform.Translate(jugadorTras);
            direccion = 1;
            transform.localScale = new Vector3(-1f, 1f, 1f); // escala normal
        }
        if (dev.DPadLeft.IsPressed)
        {
            jugadorTras.x = -movement.factorVelocidad * Time.deltaTime;
            transform.Translate(jugadorTras);
            direccion = -1;
            transform.localScale = Vector3.one; // escala al reves
        }

    }
    void salto(InputDevice dev)
    {
        if (dev.DPadUp.WasPressed)
        {
            Debug.Log("Player " + playerNumber + " JUMP!");
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
            animator.SetBool(animations.jumpBool, false);
        }
        else animator.SetBool(animations.jumpBool, true);
    }
    void block(InputDevice dev)
    {
        movement.isBlocking = false;
        hurtbox.enabled = true;
        if (isTouchingGround == false)
        {
            return;
        }

        if(dev.DPadDown.IsPressed)
        {
            Debug.Log("Player " + playerNumber + " BLOCK!");
            hurtbox.enabled = false;
            animator.SetBool(animations.block, true);
            movement.isBlocking = true;
        }
    }
    
    void lightAttack(InputDevice dev)
    {
        if (dev.Action1.WasPressed)
        {
            if (isAttacking==false && movement.isBlocking==false)
            {
                Collider2D hitCol = null;
                if (direccion>0)
                {
                    
                    if (hitCol = Physics2D.OverlapBox(lightAttackerRight.transform.position, lightAttackerRight.size, 0,otros))
                    {
                        Debug.Log("Pego right");
                    }

                }
                else
                {
                    if (hitCol = Physics2D.OverlapBox(lightAttackerLeft.transform.position, lightAttackerLeft.size,0,otros))
                    {
                        Debug.Log("Pego left");
                    }
                }
                if (hitCol != null)
                {
                    Jugador j = hitCol.gameObject.GetComponent<Jugador>();
                    if (j != null)
                    {
                        j.vergazo(1);
                    }
                    else
                    {
                        Debug.Log("DIDNT FIND JUGADOR");
                    }
                }
                else
                {
                    Debug.Log("NULL COLLIDER");
                }
                lightAttackerLeft.enabled = false;
                lightAttackerRight.enabled = false;
            }
        }

    }

    void dab(InputDevice dev)
    {
        if (dev.Action4)
        {
            Debug.Log("Player " + playerNumber + " DAB!");
        }
    }
    void vergazo(int hit)
    {
        if (movement.isBlocking == true)
        {
            return;
        }
        vida.golpeRecivido = vida.golpeRecivido + hit;
        Debug.Log(gameObject.name + " RECEIVED " + hit + " DAMAGE!");
    }

    void SetupAnimator()
    {
        Animator wantedAnim = GetComponentsInChildren<Animator>()[1];
        Avatar wantedAvater = wantedAnim.avatar;

        animator.avatar = wantedAvater;
        Destroy(wantedAnim);
    }
}