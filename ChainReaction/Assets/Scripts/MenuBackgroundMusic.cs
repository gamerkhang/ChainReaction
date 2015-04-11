using UnityEngine;
using System.Collections;

public class MenuBackgroundMusic : MonoBehaviour {

	private static MenuBackgroundMusic instance = null;
	public static MenuBackgroundMusic Instance {
		get { return instance; }
	}
	void Awake() {
		if (instance != null && instance != this) {
			Destroy(this.gameObject);
			return;
		} else {
			instance = this;
		}
		DontDestroyOnLoad(this.gameObject);
	}

	void Update(){
		GetComponent<AudioListener>().GetComponent<AudioSource>().volume = Options.s;
		if (CameraFollowPlayer.musicOff)
						Destroy (gameObject);
	}
	
}
