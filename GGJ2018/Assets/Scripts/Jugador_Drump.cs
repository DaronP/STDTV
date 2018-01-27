using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jugador_Drump : MonoBehaviour {

    Vector3 jugadorTras;
    float factorVelocidad;
    public int jugador;

    // Use this for initialization
    void Start () {
        jugadorTras = new Vector3(0.0f, 0.6f, 0.0f);
        factorVelocidad = 21.3f;
	}
	
	// Update is called once per frame
	void Update () {
        movimiento();
	}

    void movimiento()
    {
        jugadorTras.y = 0;
        jugadorTras.x = 0;
        jugadorTras.z = 0;
        
        if (Input.GetAxis("Horizontal") > 0)
        {
            jugadorTras.x = factorVelocidad * Time.deltaTime;
            transform.Translate(jugadorTras);
        }
        if (Input.GetAxis("Horizontal") < 0)
        {
            jugadorTras.x = -factorVelocidad * Time.deltaTime;
            transform.Translate(jugadorTras);
        }

    }
    void salto() { }
    void block() { }
    void lightAttack() { }
    void heavyAttack() { }
    void dab() { }
    void vergazo() { }
}
