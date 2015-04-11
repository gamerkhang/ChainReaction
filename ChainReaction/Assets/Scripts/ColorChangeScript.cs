using UnityEngine;
using System.Collections;

public abstract class ColorChangeScript : MonoBehaviour {

	public Component[] colorChangeComponents;

	// Use this for initialization
	public void Start () {
		colorChangeComponents = gameObject.GetComponentsInChildren<ColorChangeScript> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	protected Color generateNextColor(Color damage, Color current, float multiplier){
		float r = Mathf.Max (0, Mathf.Min (1, current.r + (damage.r-0.5f)*2 * multiplier));
		float g = Mathf.Max (0, Mathf.Min (1, current.g + (damage.g-0.5f)*2 * multiplier));
		float b = Mathf.Max (0, Mathf.Min (1, current.b + (damage.b-0.5f)*2 * multiplier));
		Color ans = new Color (r, g, b, 1);
		return ans;

	}

	public abstract Color MyColor {
				get;
				set;
	}

	public virtual void applyDamage(Color damage, float multiplier){
		MyColor = generateNextColor (damage, MyColor, multiplier);
		foreach (ColorChangeScript s in colorChangeComponents) {
			if(!s.Equals(this))
				s.applyDamage(damage, multiplier);
		}
	}

	public bool IsBlack(){
		return MyColor.r<=0&&MyColor.g<=0&&MyColor.b<=0;
	}

	public bool IsWhite(){
		return MyColor.r>=1&&MyColor.g>=1&&MyColor.b>=1;
	}

}
