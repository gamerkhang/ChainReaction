using UnityEngine;
using System.Collections;

public class BasicEnemyAI : MonoBehaviour {

	private Transform groundCheck;	
	private Transform groundCheck2;	
	public bool facingRight = false;
	bool wasGrounded;
	public bool doFall = false;

	public float moveForce = 100f;
	public float maxSpeed = 4f;			

	// Use this for initialization
	void Start () {
		groundCheck = transform.Find("groundCheck");
		groundCheck2 = transform.Find("groundCheck2");
		wasGrounded = Physics2D.Linecast(transform.position, groundCheck2.position, 1 << LayerMask.NameToLayer("Ground"));
	}
	
	// Update is called once per frame
	void Update () {
		if(GetComponent<Rigidbody2D>().velocity.y > 20)
		{
			GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, 20);
		}
		float dir = 1;
		if (!facingRight) {
			dir = -1;
		}
		GetComponent<Rigidbody2D>().AddForce(Vector2.right * dir * moveForce);
	
		if(Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x) > maxSpeed)
		{	// ... set the player's velocity to the maxSpeed in the x axis.
			GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Sign(GetComponent<Rigidbody2D>().velocity.x) * maxSpeed, GetComponent<Rigidbody2D>().velocity.y);

		}
		bool willBeGrounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
		if (wasGrounded&&!willBeGrounded&&!doFall) {
			Flip ();
		}
		wasGrounded = Physics2D.Linecast(transform.position, groundCheck2.position, 1 << LayerMask.NameToLayer("Ground"));
	}

	public void Flip ()
	{

		// Switch the way the player is labelled as facing.
		facingRight = !facingRight;
		
		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}
