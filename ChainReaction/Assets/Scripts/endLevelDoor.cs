using UnityEngine;
using System.Collections;

public class endLevelDoor : MonoBehaviour {
	private bool open;
	bool playSound = false;

	// Use this for initialization
	void Start () {
		open = GetComponent<Animator> ().GetBool("button_pressed");
	}
	
	// Update is called once per frame
	void Update () {
		open = GetComponent<Animator> ().GetBool("button_pressed");

		if (open) {
			if(!playSound){
				GetComponent<AudioSource>().Play();
				playSound = true;
			}
		}
	}

	void OnTriggerStay2D(Collider2D other) {
		if (open) {
			if (other.gameObject.tag == "Player" && Input.GetKeyDown (KeyCode.W)) {
				Application.LoadLevel("CompleteScreen");
			}
		}
	}
}
