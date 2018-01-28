using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Feminazi_Heavy : MonoBehaviour {
	public GameObject bullet;
    private int contador;
    public GameObject spawner;
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
                Wall();
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

    void Wall(){
		Vector2 pos = new Vector3 (transform.position.x, transform.position.y, 0f);
		Instantiate (bullet, pos, Quaternion.identity);
		StartCoroutine (Wait ());
	}
	IEnumerator Wait()
	{
		yield return new WaitForSeconds (500);
	}
}
