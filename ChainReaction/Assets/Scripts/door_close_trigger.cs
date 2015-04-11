using UnityEngine;
using System.Collections;

public class door_close_trigger : MonoBehaviour {
	Animator anim;
	public GameObject door;
	// Use this for initialization
	void Start () {
		anim = door.GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == "Player") {
			anim.SetBool ("trigger_close", true);
		}

	}
}
