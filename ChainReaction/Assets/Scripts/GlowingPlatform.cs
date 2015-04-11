using UnityEngine;
using System.Collections;

public class GlowingPlatform : MonoBehaviour {
	public bool changeUP = false;
	float dAlpha = 1f;
	public GameObject plat;
	public Color start;
	public Color end;
	public Color newC;
	public float speed = 1f;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		GetComponent<Renderer>().material.color = Color.Lerp(start, end, Mathf.PingPong(Time.time * speed, 1.0f));
	}
}
