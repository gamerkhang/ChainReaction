using UnityEngine;
using System.Collections;

public class CompleteScreen : MonoBehaviour {
	public Texture backgroundTexture;
	public GameObject sound;
	
	public GUIStyle image1;
	
	
	public float xLoc;
	public float y1;

	
	
	void OnGUI(){
		
		//Display our background Texture
		GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), backgroundTexture);
		

		
		if(GUI.Button(new Rect(Screen.width * xLoc, Screen.height * y1, Screen.width * .25f, Screen.height * .1f), "Level Select", image1)){
			Application.LoadLevel("LevelSelect");
		}
		
		
	}
}
