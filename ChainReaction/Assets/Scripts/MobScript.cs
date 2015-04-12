using UnityEngine;
using System.Collections;

public class MobScript : MonoBehaviour {
	public GameObject mob1;
	public float speed;
	public float maxBoundaryX;
	public float maxBoundaryY;
	public float minBoundaryX;
	public float minBoundaryY;
	public SpawnManager spawn;
	//random position on the map 
	public Vector3 targetPosition;
	public Vector2 direction;
	// For determining which way the player is currently facing.
	public bool facingRight = true;		
	public bool isDead = false;

	// Use this for initialization
	void Start () {
		targetPosition.x = Random.Range (minBoundaryX,maxBoundaryX);
		targetPosition.y = Random.Range (minBoundaryY, maxBoundaryY);
		targetPosition.z = 0;
		spawn = FindObjectOfType<SpawnManager> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Vector3.Distance(transform.position,targetPosition) < .3f){
			targetPosition.x = Random.Range (minBoundaryX,maxBoundaryX);
			targetPosition.y = Random.Range (minBoundaryY, maxBoundaryY);
			targetPosition.z = 0;
		}
		Vector2 pos = targetPosition - this.transform.position;
		if ((pos.x >= 0f && !facingRight) || (pos.x <= 0f && facingRight)) {
			transform.localScale = new Vector3(-transform.localScale.x,transform.localScale.y,1);
			facingRight = !facingRight;
		}
	}

	void FixedUpdate(){
		if (isDead != true) {
			direction = (targetPosition - this.transform.position).normalized;
			this.transform.GetComponent<Rigidbody2D> ().velocity = direction * speed;
		} else {
			this.transform.GetComponent<Rigidbody2D> ().velocity = Vector2.zero;
		}
	}
	public void mobDie()
	{
		spawn.mobCounter--;
		transform.GetComponent<Collider2D> ().enabled = false;
		Animator anim = transform.GetComponent<Animator> ();
		isDead = true;
		if (anim != null) {
			anim.SetTrigger("isDead");
		}

		Destroy (this.gameObject,2);

	}
}
