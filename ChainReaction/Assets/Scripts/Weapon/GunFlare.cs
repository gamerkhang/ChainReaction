using UnityEngine;
using System.Collections;

public class GunFlare : MonoBehaviour {
	private Animator animator;

	// Use this for initialization
	void Start () {
		animator = this.GetComponent<Animator>();
	}

	// Update is called once per frame
	void Update () {
		if(animator.GetCurrentAnimatorStateInfo(0).IsName("GunEffectEnd")) {
			Destroy (transform.root.gameObject);
		}
	}
}
