using UnityEngine;
using System.Collections;

public class SongSelectScript : MonoBehaviour {
	public AudioClip[] myMusic;
	public int currentSong;
	bool swapped =false;
	// Use this for initialization
	void Start () {
		currentSong = 0;
		GetComponent<AudioSource>().clip = myMusic[0];
		GetComponent<AudioSource>().Play ();
	}
	
	// Update is called once per frame
	void Update () {
		if(currentSong != 0 && !swapped)
		{
			GetComponent<AudioSource>().clip = myMusic[currentSong];
			swapped = true;
			GetComponent<AudioSource>().Play();
		}
	}
}
