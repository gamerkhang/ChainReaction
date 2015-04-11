using UnityEngine;
using System.Collections;

public class button_switch : MonoBehaviour {
	public GameObject pressed;
	public GameObject door;
	Animator anim;
	public static bool lightHit = false;

	// Use this for initialization
	void Start () {
		anim = door.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter2D(Collision2D other) {
		if (other.gameObject.tag == "Player" || other.gameObject.tag == "Light") {
			GameObject button = (GameObject)Instantiate(pressed, transform.position, transform.rotation);
			Destroy(gameObject);
			anim.SetBool ("button_pressed", true);
			if(other.gameObject.tag == "Light")
				Destroy (other.gameObject);
	
		}
	}


	void OnTriggerStay2D(Collider2D other) {
		if (other.gameObject.tag == "Player" || other.gameObject.tag == "Light") {
			GameObject button = (GameObject)Instantiate(pressed, transform.position, transform.rotation);
			Destroy(gameObject);
			anim.SetBool ("button_pressed", true);
//			if(other.gameObject.tag == "Light")
//				Destroy (other.gameObject);
		}
	}
}
