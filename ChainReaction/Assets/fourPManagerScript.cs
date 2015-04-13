using UnityEngine;
using System.Collections;

public class fourPManagerScript : MonoBehaviour {

	public GameObject player1;
	public GameObject player2;
	public GameObject player3;
	public GameObject player4;
	public ChainScript p1Chain;
	public ChainScript p2Chain;
	public ChainScript p3Chain;
	public ChainScript p4Chain;
	public bool p1Lose = false;
	public bool p2Lose = false;
	public bool p3Lose = false;
	public bool p4Lose = false;
	public int level = 0;
	// Use this for initialization
	void Awake () {
		p1Chain = player1.GetComponent<ChainScript> ();
		p2Chain = player2.GetComponent<ChainScript> ();
		p3Chain = player3.GetComponent<ChainScript> ();
		p4Chain = player4.GetComponent<ChainScript> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape)) {
			Application.LoadLevel(0);
		}
		if (p1Chain.hasLost && p1Lose == false) {
			p1Lose = true;
			foreach(SpriteRenderer r in player1.GetComponentsInChildren<SpriteRenderer>())
			{
				r.enabled = false;

			}
			foreach(Collider2D r in player1.GetComponentsInChildren<Collider2D>())
			{
				r.enabled = false;
			}
		} else if (p2Chain.hasLost && p2Lose == false) {
			p2Lose = true;
			foreach(SpriteRenderer r in player2.GetComponentsInChildren<SpriteRenderer>())
			{
				r.enabled = false;
			}
			foreach(Collider2D r in player2.GetComponentsInChildren<Collider2D>())
			{
				r.enabled = false;
			}

		}else if (p3Chain.hasLost&& p3Lose == false) {
			p3Lose = true;
			foreach(SpriteRenderer r in player3.GetComponentsInChildren<SpriteRenderer>())
			{
				r.enabled = false;
			}
			foreach(Collider2D r in player3.GetComponentsInChildren<Collider2D>())
			{
				r.enabled = false;
			}

		}else if (p4Chain.hasLost && p4Lose == false) {
			p4Lose = true;
			foreach(SpriteRenderer r in player4.GetComponentsInChildren<SpriteRenderer>())
			{
				r.enabled = false;
			}
			foreach(Collider2D r in player4.GetComponentsInChildren<Collider2D>())
			{
				r.enabled = false;
			}

		}


		if (p1Chain.hasLost && p2Chain.hasLost && p3Chain.hasLost && !p4Chain.hasLost) {
			level = 4;
			Invoke("load",1);
		}else if (p1Chain.hasLost && p2Chain.hasLost && !p3Chain.hasLost && p4Chain.hasLost) {
			level = 3;
			Invoke("load",1);
		}else if (p1Chain.hasLost && !p2Chain.hasLost && p3Chain.hasLost && p4Chain.hasLost) {
			level = 2;
			Invoke("load",1);
		}else if (!p1Chain.hasLost && p2Chain.hasLost && p3Chain.hasLost && p4Chain.hasLost) {
			level = 1;
			Invoke("load",1);
		}
	}
	void load()
	{
		Application.LoadLevel (level);
	}
}