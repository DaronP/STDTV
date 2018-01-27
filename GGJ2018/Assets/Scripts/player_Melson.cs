using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_Melson : MonoBehaviour {

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

	// Use this for initialization
	void Start () 
	{
		rb = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () 
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


	void movimiento();
	void salto();
	public void block()
	{
		if (isTouchingGround)
		{
			rb.AddForce(0f);
			Debug.Log("BLock");
		}
		else if (!isTouchingGround && !dblJumped)
		{
			rb.velocity = new Vector2(rb.velocity.x, 0f);
			rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
			dblJumped = true;
			Debug.Log("Double jumped.");
		}
	}
	void lightAttack();
	void heavyAttack();
	void dab();
	void vergazo();

}
