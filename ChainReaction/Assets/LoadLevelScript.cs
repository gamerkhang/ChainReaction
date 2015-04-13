using UnityEngine;
using System.Collections;

public class LoadLevelScript : MonoBehaviour {
	public int level;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnMouseDown() {
		Application.LoadLevel(level);
	}
}
