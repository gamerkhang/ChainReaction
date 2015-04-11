using UnityEngine;
using System.Collections;

public class FloaterDeath : Killable{
	Animator anim;
	bool playSound = false;


	// Use this for initialization
	void Start () {
		anim = GetComponentInParent<Animator>();
	}

	public override void kill() {
		delayedDeath (0.5f);
	}

	public void delayedDeath(float time) {
		this.GetComponentInParent<Floater>().dead = true;
		StartCoroutine(WaitMethod (time));
		if(!playSound){
			GetComponent<AudioSource>().Play();
			playSound = true;
		}
		anim.SetBool ("is_dead", true);
		Destroy(this.gameObject.GetComponent<Collider2D>());
		Destroy (this.gameObject, 1f);
	}

	IEnumerator WaitMethod(float time) {
		yield return new WaitForSeconds(time);
	}

	// Update is called once per frame
	void Update () {
	
	}
}
