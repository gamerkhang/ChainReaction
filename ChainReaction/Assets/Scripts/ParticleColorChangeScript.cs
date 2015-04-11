using UnityEngine;
using System.Collections;

public class ParticleColorChangeScript : ColorChangeScript {

	// Use this for initialization
	public void Start () {
		base.Start ();
		Debug.Log(gameObject.GetComponent<ParticleSystem>().startColor);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public override Color MyColor{
		get
		{
			return gameObject.GetComponent<ParticleSystem>().startColor;
		}
		set
		{
			gameObject.GetComponent<ParticleSystem>().startColor = value;
		}
	}

}
