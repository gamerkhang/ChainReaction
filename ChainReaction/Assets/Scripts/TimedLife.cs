using UnityEngine;
using System.Collections;

public class TimedLife : MonoBehaviour {

	public float ttl;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		ttl -= Time.deltaTime;
		if (ttl <= 0) {
			Destroy (gameObject);
			Debug.Log("removed");
		}
	}
}
