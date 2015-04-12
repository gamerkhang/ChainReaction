using UnityEngine;
using System.Collections;

public class ChainHeadScript : MonoBehaviour {
	public AudioClip killSound;
	public AudioSource source;
	public bool isHead = false;
	public ChainScript chain;

	// Use this for initialization
	void Start () {
		chain = transform.GetComponentInParent<ChainScript> ();
		source = chain.GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
	 if (chain == null) {
			chain = transform.GetComponentInParent<ChainScript> ();
			source = chain.GetComponent<AudioSource> ();
		}
	}
	void OnTriggerEnter2D(Collider2D coll) {
	if (source != null && chain != null) {
			if (isHead == true && coll.gameObject.tag == "Enemy") {
				source.PlayOneShot (killSound);
				for (int i = 0; i < 4; i++) {
					chain.addChainLength ();
				}

				coll.transform.GetComponent<MobScript> ().mobDie ();
			} else if (isHead != true && coll.gameObject.tag == "Enemy") {
				if (source != null)
					source.PlayOneShot (killSound);

				for (int i = 0; i < 2; i++) {
					chain.subChainLength ();
				}
				coll.transform.GetComponent<MobScript> ().mobDie ();

				//player hit detection and point attribution

				//Player 1 gets hit
			} else if (isHead == true && chain.playerNumber != 0 && coll.gameObject.tag == "P1") {
				if (chain.playerNumber == 1) {
					print ("Player 2 gained a point");
				} else if (chain.playerNumber == 2) {
					print ("Player 3 gained a point");
				} else if (chain.playerNumber == 3) {
					print ("Player 4 gained a point");
				}

			}

		//Player 2 gets hit
		else if (isHead == true && coll.gameObject.tag == "P2") {
				print ("Player 2 got hit");
				if (chain.playerNumber == 0) {
					print ("Player 1 gained a point");
				} else if (chain.playerNumber == 2) {
					print ("Player 3 gained a point");
				} else if (chain.playerNumber == 3) {
					print ("Player 4 gained a point");
				}
			}

		//Player 3 gets hit
		else if (isHead == true && coll.gameObject.tag == "P3") {
				if (chain.playerNumber == 1) {
					print ("Player 2 gained a point");
				} else if (chain.playerNumber == 0) {
					print ("Player 1 gained a point");
				} else if (chain.playerNumber == 3) {
					print ("Player 4 gained a point");
				}
			}

		//Player 4 gets hit
		else if (isHead == true && coll.gameObject.tag == "P4") {
				if (chain.playerNumber == 1) {
					print ("Player 2 gained a point");
				} else if (chain.playerNumber == 2) {
					print ("Player 3 gained a point");
				} else if (chain.playerNumber == 0) {
					print ("Player 1 gained a point");
				}
			}

		}
	}

}
