using UnityEngine;
using System.Collections;

public class FrostKillable : Killable {

	DanFrostFight fight;
	// Use this for initialization
	void Start () {
		fight = gameObject.GetComponent<DanFrostFight> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public override void kill ()
	{
		fight.onDeath ();
	}
}
