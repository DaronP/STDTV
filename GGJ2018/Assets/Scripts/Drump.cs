using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drump : MonoBehaviour {

	public GameObject bullet;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown("Heavy"))
		{
			Shooting ();
			Debug.Log ("Heavy");
		}

	}

	void Shooting(){
		Vector2 pos = new Vector2 (transform.position.x, transform.position.y);
		Instantiate (bullet, pos, Quaternion.identity);
	}
}
