using UnityEngine;
using System.Collections;

public class Floater : MonoBehaviour {
	public Transform target;
	public int moveSpeed;
	public int rotationSpeed;
	public float minDist = 5f;
	public float maxDist = 20f;
	public float damageFactor = 0.2f;
	private Transform myTransform;
	public bool dead = false;

	public AudioClip[] hitClips;
	private float clipEnd;

	public bool facingLeft = true;

	// Use this for initialization
	void Awake() {
		myTransform = transform;
	}
	void Start () {
		GameObject go = GameObject.FindGameObjectWithTag("Player");
		target = go.transform;
	}
	
	
	// Update is called once per frame
	void Update () {
		Vector3 pos2 = transform.position;
		pos2.z = 0;
		transform.position = pos2;

		float dist = Vector3.Distance (target.position, myTransform.position);
		Vector3 pos = target.position - myTransform.position;

		/*
		Vector3 dir = target.position - myTransform.position;
		dir.z = 0.0f; // Only needed if objects don't share 'z' value
		if (dir != Vector3.zero) {
			myTransform.rotation = Quaternion.Slerp (myTransform.rotation,
			                                         Quaternion.FromToRotation(Vector3.right, dir), rotationSpeed * Time.deltaTime);
		}
		*/
		//Move Towards Target
		if (dist <= maxDist && dist >= minDist && !dead) {
			myTransform.position += (target.position - myTransform.position).normalized * moveSpeed * Time.deltaTime;
		}
		if ((pos.x >= 0f && facingLeft) || (pos.x <= 0f && !facingLeft))
			Flip();
	}

	void Flip ()
	{
		// Switch the way the player is labelled as facing.
		facingLeft = !facingLeft;
		
		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
	
	void OnCollisionEnter2D(Collision2D coll)
	{
		if (coll.gameObject.tag == "Player") {
			this.GetComponentInParent<FloaterDeath>().kill ();
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == "Light") {
			if (Time.time > clipEnd){
				int i = Random.Range(0, hitClips.Length);
				AudioSource.PlayClipAtPoint(hitClips[i], transform.position);
				clipEnd = Time.time + hitClips[i].length;
			}
		}
		else if (other.gameObject.tag == "Player") {
			other.GetComponentInParent<PlayerHealth>().TakeDamage2(transform, new Color(0f,0f, 0f, 0f), damageFactor);
			this.GetComponentInParent<FloaterDeath>().delayedDeath(0.5f);
		//	StartCoroutine(WaitMethod());
		}
	}
}
