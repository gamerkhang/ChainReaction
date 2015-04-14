using UnityEngine;
using System.Collections;

public class ChainHeadScript : MonoBehaviour {
	public AudioClip killSound;
	public AudioClip hitSound;
	public AudioClip winSound;
	public AudioSource source;
	public bool isHead = false;
	public ChainScript chain;

	// Use this for initialization

	void Start(){
	}
	// Update is called once per frame
	void Update () {
	 if (chain == null) {
			chain = transform.GetComponentInParent<ChainScript> ();
			source = chain.GetComponent<AudioSource> ();
		}
	}
	void OnTriggerEnter2D(Collider2D coll) {

	if (chain != null) {
			int currentLength = chain.currentChainLength;
			if (isHead == true && coll.gameObject.tag == "Enemy") {
				if(source != null)
				source.PlayOneShot (killSound);

				Camera.main.GetComponent<CameraShakeScript>().shake = .2f;
				for (int i = 0; i < 5; i++) {
					chain.addChainLength ();
				}

				coll.transform.GetComponent<MobScript> ().mobDie ();
			} else if (isHead != true && coll.gameObject.tag == "Enemy") {
				if (source != null)
					source.PlayOneShot (hitSound);

				coll.transform.GetComponent<MobScript> ().mobDie ();

				//player hit detection and point attribution

				//Player 1 gets hit and im not player 1
			} else if (isHead == true && chain.playerNumber != 0 && coll.gameObject.tag == "P1") {
				coll.GetComponentInParent<ChainScript>().removeStock();
				if(source != null)
					source.PlayOneShot (winSound);
				for(int i = 0; i < (currentLength * .75f); i++)
				chain.subChainLength();
			} else if (isHead == true && chain.playerNumber != 1 && coll.gameObject.tag == "P2") {
				coll.GetComponentInParent<ChainScript>().removeStock();
				if(source != null)
					source.PlayOneShot (winSound);
				for(int i = 0; i < (currentLength * .75f); i++)
					chain.subChainLength();
			} else if (isHead == true && chain.playerNumber != 2 && coll.gameObject.tag == "P3") {
				coll.GetComponentInParent<ChainScript>().removeStock();
				if(source != null)
					source.PlayOneShot (winSound);
				for(int i = 0; i < (currentLength * .75f); i++)
					chain.subChainLength();
			} else if (isHead == true && chain.playerNumber != 3 && coll.gameObject.tag == "P4") {
				coll.GetComponentInParent<ChainScript>().removeStock();
				if(source != null)
					source.PlayOneShot (winSound);
				for(int i = 0; i < (currentLength * .75f); i++)
					chain.subChainLength();
			}

		}
	}

}
