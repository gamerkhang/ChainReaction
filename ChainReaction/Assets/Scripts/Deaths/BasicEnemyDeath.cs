using UnityEngine;
using System.Collections;

public class BasicEnemyDeath : Killable {

	public GameObject deathEffect;

	// Use this for initialization
	void Start () {
		
	}

	public override void kill() {
		Instantiate (deathEffect, transform.position, Quaternion.identity);
		base.kill ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
