using UnityEngine;
using System.Collections;

public class Options : MonoBehaviour {
	public GameObject obj;
	public Texture backgroundTexture;
	
	public GUIStyle image1;
	public GUIStyle image2;

	public float xLoc;
	public float xLoc2;
	public float xLoc3;
	public float y1;
	public float y2;
	public float y3;

	public static float s = 1.0f;

//	AudioListener main;


	void Start()
	{
//		main = obj.GetComponent<AudioListener>();
	}
	void Update()
	{
//		main.audio.volume = s;
	}

	void OnGUI(){
		
		//Display our background Texture
		GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), backgroundTexture);

		GUI.Label(new Rect(Screen.width * xLoc2, Screen.height * y1, Screen.width * .2f, Screen.height * .1f), "Volume", image2);

		s = GUI.HorizontalSlider (new Rect(Screen.width * xLoc3, Screen.height * y2, Screen.width * .45f, Screen.height * .1f), s, 0.0F, 1.0F);
		GUI.color = Color.white;
//		GUI.Button (new Rect(Screen.width * xLoc, Screen.height * y1, Screen.width * .5f, Screen.height * .1f), "");
		
		if(GUI.Button(new Rect(Screen.width * xLoc, Screen.height * y3, Screen.width * .5f, Screen.height * .1f), "Back", image1)){
			Application.LoadLevel("MainMenu");
		}
		
		
	}
}
