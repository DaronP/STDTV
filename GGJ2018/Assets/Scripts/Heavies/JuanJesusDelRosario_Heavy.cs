using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JuanJesusDelRosario_Heavy : MonoBehaviour {
	public GameObject bullet;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown("joystick button 1"))
		{
			Shooting ();
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
