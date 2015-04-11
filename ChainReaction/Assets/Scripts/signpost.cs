using UnityEngine;
using System.Collections;

public class signpost : MonoBehaviour {
	private bool showText = false;
	public Texture2D textureToDisplay;
	public string helptext;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	}



	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == "Player") {
			showText = true;
		}
	}

	void OnTriggerExit2D(Collider2D other) {
		if (other.gameObject.tag == "Player") {
			showText = false;
		}
	}

	void OnGUI()
	{
		var centeredStyle = GUI.skin.GetStyle("Label");
		centeredStyle.alignment = TextAnchor.UpperCenter;
		if (showText) {
			GUI.Label (new Rect (Screen.width/2-150, 0, textureToDisplay.width, textureToDisplay.height), textureToDisplay);
			GUI.Label (new Rect (Screen.width/2-125, 25, textureToDisplay.width-50, textureToDisplay.height), helptext);
		}
	}

}
