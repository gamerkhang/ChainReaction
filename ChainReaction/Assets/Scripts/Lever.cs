using UnityEngine;
using System.Collections;

public class Lever : MonoBehaviour {
	public GameObject rightLever;
	public GameObject leftLever;
	public MirrorFlip mirror;

	private bool isLeft = true;
	private GameObject next;
	private GameObject current;	

	// Use this for initialization
	void Start () {
		leftLever.GetComponent<Renderer>().enabled = true;
		rightLever.GetComponent<Renderer>().enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(isLeft){
			current = leftLever;
			next = rightLever;
		}
		else{
			current = rightLever;
			next = leftLever;
		}
	}

	void OnTriggerStay2D(Collider2D other) {
		if(isLeft){
			current = leftLever;
			next = rightLever;
		}
		else{
			current = rightLever;
			next = leftLever;
		}
		if (other.gameObject.tag == "Player" && Input.GetKeyDown (KeyCode.E)) {
//			audio.Play();
			next.GetComponent<Renderer>().enabled = true;
			current.GetComponent<Renderer>().enabled = false;
			isLeft = !isLeft;
			Debug.Log (isLeft);
			mirror.Flip();
		}
	}
}
