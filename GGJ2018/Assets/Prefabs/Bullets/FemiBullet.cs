using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class FemiBullet : MonoBehaviour {

    private Jugador jugador { get { return FindObjectOfType<Jugador>(); } set { jugador = value; } }

    private Rigidbody2D rb;
	public float speed;



	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();

	}

	// Update is called once per frame
	void FixedUpdate () {
		if (jugador.direccion == 1) {
			rb.AddForce (Vector2.right * speed);
		}
		if (jugador.direccion == -1) {
			rb.AddForce (Vector2.left * speed);
		}			
	}

	void OnCollisionEnter2D(Collision2D col){
		if (col.gameObject.tag == "Player") {
			Destroy (this.gameObject);
			jugador.vergazo (10);
		}
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        Destroy(this.gameObject);
    }
}
