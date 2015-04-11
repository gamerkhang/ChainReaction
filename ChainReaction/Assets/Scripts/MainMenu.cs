using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {
	public Texture backgroundTexture;
	public GameObject sound;
	
	public GUIStyle image1;


	public float xLoc;
	public float y1;
	public float y2;
	public float y3;
	public float y4;


	void OnGUI(){
		
		//Display our background Texture
		GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), backgroundTexture);

		if(GUI.Button(new Rect(Screen.width * xLoc, Screen.height * y1, Screen.width * .5f, Screen.height * .1f), "Start", image1)){
			Destroy (sound);
			Application.LoadLevel("Prototype");
		}

		if(GUI.Button(new Rect(Screen.width * xLoc, Screen.height * y2, Screen.width * .5f, Screen.height * .1f), "Level Select", image1)){
			Application.LoadLevel("LevelSelect");
		}

		if(GUI.Button(new Rect(Screen.width * xLoc, Screen.height * y3, Screen.width * .5f, Screen.height * .1f), "Options", image1)){
			Application.LoadLevel("Options");
		}

		if(GUI.Button(new Rect(Screen.width * xLoc, Screen.height * y4, Screen.width * .5f, Screen.height * .1f), "Quit", image1)){
			Application.Quit();
		}

		
	}
}
