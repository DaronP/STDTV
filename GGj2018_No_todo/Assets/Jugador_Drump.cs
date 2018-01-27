using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Jugador_Drump : MonoBehaviour {

    private Jugador jugador { get { return FindObjectOfType<Jugador>(); } set { jugador = value; } }



    private void Update()
    {
        heavyAttack();
    } 


    void heavyAttack()
    {
        if (jugador.movement.isBlocking == true)
        {
            return;
        }
        if (Input.GetButton("Heavy"))
        {
            //Aqui hacen lo que uds quieran
        }
    }

}
