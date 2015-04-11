using UnityEngine;
using System.Collections;

public class TestColorChangeScript : MonoBehaviour {

	ColorChangeScript script;
	static Color[] colorLoop = new []{Color.red, Color.blue, Color.green, Color.black, Color.white};
	int currentState;
	int step;
	const int maxStep = 100;
	const float multiplier = 0.01f;

	// Use this for initialization
	void Start () {
		script = gameObject.GetComponent<ColorChangeScript> ();
	}
	
	void FixedUpdate () {
		script.applyDamage(colorLoop[currentState], multiplier);
		if (++step >= maxStep) {
			currentState = (currentState + 1) % colorLoop.Length;
			step = 0;
		}
	}
}
