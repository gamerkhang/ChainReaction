using UnityEngine;
using System.Collections;

public class anykeytoContinue : MonoBehaviour {
	public int level;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	if (Input.anyKeyDown) {
			Application.LoadLevel(level);
		}
	}
}
