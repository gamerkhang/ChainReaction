using UnityEngine;
using System.Collections;

public class ChainHeadScript : MonoBehaviour {
	public bool isHead = false;
	public ChainScript chain;
	// Use this for initialization
	void Start () {
		chain = transform.GetComponentInParent<ChainScript> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnTriggerEnter2D(Collider2D coll) {
		if (isHead == true && coll.gameObject.tag == "Enemy") {
			chain.addChainLength ();
			coll.transform.GetComponent<MobScript>().mobDie ();
		}
	}

}
