using UnityEngine;
using System.Collections;

public class Killable : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

	public virtual void kill() {
		Destroy (this.gameObject);

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
