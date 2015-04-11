using UnityEngine;
using System.Collections;

public class randomPlatform : MonoBehaviour {
	public float waitTime = 0f;
	public float delayTime = 0f;
	bool trigger = true;
	public GameObject platform;
	public GameObject hitBox;

	IEnumerator DelayMethod() {
		print ("delay " + delayTime);
		yield return new WaitForSeconds (delayTime);
	}

	IEnumerator MyMethod() {
		yield return new WaitForSeconds (delayTime);
		yield return new WaitForSeconds(waitTime);
		if (!trigger) {
			platform.GetComponent<Renderer>().enabled = false;
			hitBox.GetComponent<Collider2D>().enabled = false;
		} else {
			platform.GetComponent<Renderer>().enabled = true;
			hitBox.GetComponent<Collider2D>().enabled = true;
		}
		trigger = !trigger;
		StartCoroutine(MyMethod());
	}




	// Use this for initialization
	void Start () {
		platform.GetComponent<Renderer>().enabled = false;
		hitBox.GetComponent<Collider2D>().enabled = false;
		StartCoroutine(DelayMethod());
		StartCoroutine(MyMethod());
	}
	
	// Update is called once per frame
	void Update () {



	}
}
