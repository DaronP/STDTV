using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitGame : MonoBehaviour {

	// Use this for initialization
	public void Quit()
	{
		UnityEditor.EditorApplication.isPlaying = false;
		//Application.Quit ();
	}
}
