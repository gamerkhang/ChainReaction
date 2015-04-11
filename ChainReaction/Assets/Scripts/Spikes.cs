using UnityEngine;
using System.Collections;

public class Spikes : MonoBehaviour {
	public GUIStyle menuStyle;
	public GUIStyle image1;
//	private bool isDead = false;

	IEnumerator MyMethod() {
		yield return new WaitForSeconds(.5f);
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
/*
	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == "Player") {
			isDead = true;
		}
	}


	void OnGUI()
	{
		if (isDead) {
			if (Input.GetKeyDown (KeyCode.R)) {
				Application.LoadLevel (Application.loadedLevel);
			}
			StartCoroutine(MyMethod());
			GUI.Window (0, new Rect (Screen.width * .25f, Screen.height * .1f, Screen.width * .5f, Screen.height * .75f), InitialWindow, "", menuStyle);
		}
	}

	void InitialWindow(int windowID) {
		if (GUI.Button (new Rect (Screen.width * .38f, Screen.height * .60f, Screen.width * .1f, Screen.height * .1f), "Quit", image1)) {
			Application.LoadLevel("MainMenu");
		}
		
	}
*/	
}
