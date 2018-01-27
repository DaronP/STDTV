using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallMovement : MonoBehaviour {

	private Rigidbody2D rb;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		rb.AddForce (Vector2.up * 2);

	}

	void OnTriggerEnter2D(Collider2D col){
		Destroy (col);
	}
}
