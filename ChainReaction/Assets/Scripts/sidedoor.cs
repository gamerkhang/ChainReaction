using UnityEngine;
using System.Collections;

public class sidedoor : MonoBehaviour {
	Animator anim;
	bool trigger;
	bool playSound = false;
	// Use this for initialization

	IEnumerator MyMethod() {
		yield return new WaitForSeconds(.5f);
		trigger = true;
	}

	void Start () {
		anim = GetComponent<Animator> ();
		trigger = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (anim.GetBool ("button_pressed") == true) {
			if(!playSound){
				GetComponent<AudioSource>().Play();
				playSound = true;
			}
			StartCoroutine(MyMethod());
			if(trigger == true)
				Destroy (GetComponent<BoxCollider2D>());	
		}

		if (anim.GetBool ("trigger_close") == true) {
			GetComponent<BoxCollider2D>().enabled = true;
			if(!playSound){
				GetComponent<AudioSource>().Play();
				playSound = true;
			}
		}

	}
}
