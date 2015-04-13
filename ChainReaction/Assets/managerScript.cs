using UnityEngine;
using System.Collections;

public class managerScript : MonoBehaviour {
	public int p1Score;
	public int p2Score;
	public GameObject player1;
	public GameObject player2;
	public ChainScript p1Chain;
	public ChainScript p2Chain;
	public bool endGame = false;
	public int i2;
	// Use this for initialization
	void Awake () {
		p1Chain = player1.GetComponent<ChainScript> ();
		p2Chain = player2.GetComponent<ChainScript> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape)) {
			Application.LoadLevel(0);
		}
		 i2 = 0;
		if (p1Chain.hasLost ) {
			//Debug.Log ("P2 has Won");
			 i2 = 2;
			Invoke("load",1);
		}
		if (p2Chain.hasLost ) {
			//Debug.Log ("P1 has Won");
			i2 = 1;
			Invoke("load",1);
		}

	}

	void load()
			       {
				Application.LoadLevel (i2);
			}
}
