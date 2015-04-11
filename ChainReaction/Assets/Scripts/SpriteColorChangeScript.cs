using UnityEngine;
using System.Collections;

public class SpriteColorChangeScript : ColorChangeScript {

	// Use this for initialization
	public void Start () {
		base.Start ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public override Color MyColor {
		get
		{
			return ((SpriteRenderer)gameObject.GetComponent<Renderer>()).color;
		}
		set
		{
			((SpriteRenderer)gameObject.GetComponent<Renderer>()).color = value;
		}
	}
}
