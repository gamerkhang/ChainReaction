using UnityEngine;
using System.Collections;

public class Attackable : MonoBehaviour {
	public float defense;

	// Use this for initialization
	void Start () {
		if(defense == 0f)
			defense = 1f;
	}
	
	// Update is called once per frame
	void Update () {
		if(this.GetComponentInParent<ColorChangeScript>().IsWhite())
			this.GetComponentInParent<Killable>().kill();
	}

	public void TakeHit(Color color, float multiplier) {
		this.GetComponentInParent<ColorChangeScript>().applyDamage(color, multiplier / defense);
	}
}
