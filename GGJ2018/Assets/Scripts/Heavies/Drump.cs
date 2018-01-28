using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drump : MonoBehaviour {
	

	public GameObject bullet;
	public GameObject spawner;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown("joystick button 1"))
		{
			Wall ();

		}
	}

	void Wall(){
		
		Vector2 pos = new Vector3 (transform.position.x, transform.position.y, 0f);
		Instantiate (bullet, pos, Quaternion.identity);
		StartCoroutine (Wait ());
	}
	IEnumerator Wait()
	{
		yield return new WaitForSeconds (50);
	}
}
