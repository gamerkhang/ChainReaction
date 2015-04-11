using UnityEngine;
using System.Collections;

public class LevelSelect : MonoBehaviour {
	public Texture backgroundTexture;
	public GameObject sound;

	public GUIStyle image1;
	public GUIStyle image2;
	public GUIStyle image3;
	public GUIStyle image4;
	public GUIStyle image5;
	public GUIStyle NA;
	public GUIStyle back;
	
	public float xLoc;
	public float xLoc2;
	public float xLoc3;
	public float xLoc4;
	public float y1;
	public float y2;
	public float y3;
	public float y4;

	public float buttonWidth;
	public float buttonHeight;
	
	
	void OnGUI(){
		
		//Display our background Texture
		GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), backgroundTexture);
		
		if(GUI.Button(new Rect(Screen.width * xLoc, Screen.height * y1, Screen.width * buttonWidth, Screen.height * buttonHeight),"Training" , image1)){
			Destroy (sound);
			Application.LoadLevel("Prototype");
		}
		
		if(GUI.Button(new Rect(Screen.width * xLoc2, Screen.height * y1, Screen.width * buttonWidth, Screen.height * buttonHeight),"Level 1-1", image2)){
			Destroy (sound);
			Application.LoadLevel("Light2");
		}
		
		if(GUI.Button(new Rect(Screen.width * xLoc3, Screen.height * y1, Screen.width * buttonWidth, Screen.height * buttonHeight),"Level 1-2", image3)){
			Destroy (sound);
			Application.LoadLevel("Light3");
		}

		if(GUI.Button(new Rect(Screen.width * xLoc, Screen.height * y2, Screen.width * buttonWidth, Screen.height * buttonHeight),"Level 2-1", image4)){
			Destroy (sound);
			Application.LoadLevel("Light4");
		}
		
		if(GUI.Button(new Rect(Screen.width * xLoc2, Screen.height * y2, Screen.width * buttonWidth, Screen.height * buttonHeight),"Level 2-2", image1)){
			Destroy (sound);
			Application.LoadLevel("Light5");
		}
		
		if(GUI.Button(new Rect(Screen.width * xLoc3, Screen.height * y2, Screen.width * buttonWidth, Screen.height * buttonHeight),"Level 3-1", image3)){
			Destroy (sound);
			Application.LoadLevel(9);
		}

		if(GUI.Button(new Rect(Screen.width * xLoc, Screen.height * y3, Screen.width * buttonWidth, Screen.height * buttonHeight),"Level 3-2", NA)){

		}
		
		if(GUI.Button(new Rect(Screen.width * xLoc2, Screen.height * y3, Screen.width * buttonWidth, Screen.height * buttonHeight),"Level 4-1", NA)){
			
		}
		
		if(GUI.Button(new Rect(Screen.width * xLoc3, Screen.height * y3, Screen.width * buttonWidth, Screen.height * buttonHeight),"THE BIG BOSS", image5)){
			Destroy (sound);
			Application.LoadLevel("DanFrostAnimTest");
		}

		if(GUI.Button(new Rect(Screen.width * xLoc4, Screen.height * y4, Screen.width * .1f, Screen.height * .05f),"Back", back)){
			Application.LoadLevel("MainMenu");
		}
		
		
	}
}
