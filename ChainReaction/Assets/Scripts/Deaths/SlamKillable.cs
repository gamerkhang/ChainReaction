using UnityEngine;
using System.Collections;

public class SlamKillable : Killable {

	public GameObject deathEffect;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public override void kill ()
	{
		Instantiate (deathEffect, transform.position, Quaternion.identity);
		base.kill ();
	}
}
