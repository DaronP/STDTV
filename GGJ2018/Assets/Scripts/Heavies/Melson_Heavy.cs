using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melson_Heavy : MonoBehaviour {
	public GameObject bullet;
    private int contador;
    private bool puedeHacerHeavy;
    private int cooldown;

    // Use this for initialization
    void Start () {

	}

    // Update is called once per frame
    void Update()
    {
        if (puedeHacerHeavy)
        {
            if (Input.GetKeyDown("joystick button 1"))
            {
                Shooting();
                contador = contador + 1;
                if (contador == 3)
                {
                    puedeHacerHeavy = false;
                    cooldown = 600;
                }
                Debug.Log(contador);
            }
        }
        else
        {
            cooldown -= 1;
            if (cooldown < 1)
            {
                contador = 0;
                puedeHacerHeavy = true;
            }
        }
    }

    void Shooting(){
		Vector2 pos = new Vector2 (transform.position.x, transform.position.y);
		Instantiate (bullet, pos, Quaternion.identity);
		StartCoroutine (Wait ());
	}
	IEnumerator Wait()
	{
		yield return new WaitForSeconds (50);
	}
}
