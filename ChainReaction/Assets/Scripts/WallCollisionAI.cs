using UnityEngine;
using System.Collections;

public class WallCollisionAI : MonoBehaviour {

	BasicEnemyAI ai;

	// Use this for initialization
	void Start () {
		ai = gameObject.GetComponentInParent<BasicEnemyAI> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		// If the alien hits the trigger...
		if (col.gameObject.layer != LayerMask.NameToLayer("Tutorial")) {
			ai.Flip();
				}
	}
}
