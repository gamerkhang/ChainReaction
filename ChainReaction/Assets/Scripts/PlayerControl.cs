using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour
{

	public bool facingRight = true;			// For determining which way the player is currently facing.
	public float ho;
				// Condition for whether the player should jump.
	public bool isDashing = false;
	public bool jumping = false;
	private Transform wallCheck;
	public bool wallTouch = false;
	public bool wallHold = false;
	public float moveForce = 100f;			// Amount of force added to move the player left and right.
	public float maxSpeed = 5f;				// The fastest the player can travel in the x axis.
	public AudioClip[] jumpClips;			// Array of clips for when the player jumps.
	public float jumpForce = 1000f;			// Amount of force added when the player jumps.
	public AudioClip[] taunts;				// Array of clips for when the player taunts.
	public float tauntProbability = 50f;	// Chance of a taunt happening.
	public float tauntDelay = 1f;			// Delay for when the taunt should happen.
	public int jumps;	
	public float jumpDelay = 1f;
	private int tauntIndex;					// The index of the taunts array indicating the most recent taunt.
	private Transform groundCheck;			// A position marking where to check if the player is grounded.
	private Transform groundCheck2;
	public bool grounded = false;			// Whether or not the player is grounded.
	public bool grounded2 = false;
	private Animator anim;					// Reference to the player's animator component.
	private float timeStamp;
	private float swapDelay;
	private float dashTimer;
	public float dashDelay;
	private bool canBoost = true;
	public Vector2 dashSpeed = new Vector2(10,0);
	public float dashDur = .7f;

	private bool isPause = false;
	public GUIStyle menuStyle;
	public GUIStyle image1;
	private float clipEnd;
	public AudioClip[] dashClips;
	public AudioClip[] fireClips;

	void Awake()
	{
		// Setting up references.
		groundCheck = transform.Find("groundCheck");
		groundCheck2 = transform.Find ("groundCheck2");
		wallCheck = transform.Find ("wallCheck");
		anim = this.GetComponent<Animator>();
		jumps = 1;
	}

	void OnEnterCollision2D(Collision2D coll)
	{

	}

	void OnTriggerEnter2D(Collider2D coll)
	{
		if (coll.gameObject.tag == "Enemy") {
//			anim.SetTrigger ("Dead");
		}
	}

	void Update ()
	{
		if(GetComponent<Rigidbody2D>().velocity.y > 20)
		{
			GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, 20);
		}else if(GetComponent<Rigidbody2D>().velocity.y < -20)
		{
			GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, -20);
		}
		if(Input.GetKeyDown (KeyCode.M) && !isPause)
		{
			anim.SetTrigger ("Dead");
		}
		if(Input.GetKeyDown (KeyCode.G) && !isPause)
			Camera.main.GetComponent<CameraShakeScript>().shake = 1f;

		// The player is grounded if a linecast to the groundcheck position hits anything on the ground layer.
		grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
		grounded2 = Physics2D.Linecast(transform.position, groundCheck2.position, 1 << LayerMask.NameToLayer("Ground"));

		wallTouch = Physics2D.Linecast(transform.position, wallCheck.position, 1 << LayerMask.NameToLayer("Ground"));
		// If the jump button is pressed and the player is grounded then the player should jump.
	
		if(Input.GetButton("Fire1") && !isPause)
		{
			if (Time.time > clipEnd){
				int i = Random.Range(0, fireClips.Length);
				AudioSource.PlayClipAtPoint(fireClips[i], transform.position);
				clipEnd = Time.time + fireClips[i].length;
			}
			anim.SetBool("Fire", true);
		}else
		{
			anim.SetBool("Fire", false);
		}
		if(Input.GetButtonDown("Jump") && jumps !=0 && timeStamp <= Time.time  && !isPause && !isDashing)
		{
			//Debug.Log ("jumped");
			// Play a random jump audio clip.
			//int i = Random.Range(0, jumpClips.Length);
			//	AudioSource.PlayClipAtPoint(jumpClips[i], transform.position);
			GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, 0);
			// Add a vertical force to the player.
			GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, jumpForce));
			
			// Make sure the player can't jump again until the jump conditions from Update are satisfied.
			anim.SetBool("Jump", true);
			jumps = 0;
			jumping = true;
			grounded = false;
			timeStamp = Time.time + jumpDelay;

			if (Time.time > clipEnd){
				int i = Random.Range(0, jumpClips.Length);
				AudioSource.PlayClipAtPoint(jumpClips[i], transform.position);
				clipEnd = Time.time + jumpClips[i].length;
			}
		}

		if((grounded||grounded2) && jumps == 0 && GetComponent<Rigidbody2D>().velocity.y <= 0 && timeStamp <= Time.time)
		{
			//Debug.Log("reset jump" + "grounded is :" + grounded + "number of Jumps" + jumps + "current speed" + rigidbody2D.velocity.y );
			jumps = 1;
			jumping = false;
		}
		
		if(grounded || grounded2)
		{
			anim.SetBool("Jump", false);
			jumping = false;
			//Debug.Log("On the ground");
		}
		else
		{
			anim.SetBool("Jump", true);
		}


		// Cache the horizontal input.
		float h = Input.GetAxis("Horizontal1");
		ho = h;
		// The Speed animator parameter is set to the absolute value of the horizontal input.
		//anim.SetFloat("Speed", Mathf.Abs(h));

		if(!isDashing && (grounded || grounded2))
		{
		// If the player is changing direction (h has a different sign to velocity.x) or hasn't reached maxSpeed yet...
			if(h != 0)
				// ... add a force to the player.
				GetComponent<Rigidbody2D>().velocity = new Vector2(h * maxSpeed, GetComponent<Rigidbody2D>().velocity.y);

			// If the player's horizontal velocity is greater than the maxSpeed...
			if(Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x) > maxSpeed)
				// ... set the player's velocity to the maxSpeed in the x axis.
				GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Sign(GetComponent<Rigidbody2D>().velocity.x) * maxSpeed, GetComponent<Rigidbody2D>().velocity.y);

			// If the input is moving the player right and the player is facing left...
			if(h > 0 && !facingRight)
				// ... flip the player.
				Flip();
			// Otherwise if the input is moving the player left and the player is facing right...
			else if(h < 0 && facingRight)
				// ... flip the player.
				Flip();

			if(Mathf.Abs (h) <= 0.1f)
			{
				GetComponent<Rigidbody2D>().velocity = new Vector2(0, GetComponent<Rigidbody2D>().velocity.y);
			}

		}else if(!isDashing && !(grounded || grounded2) && swapDelay <= Time.time)
		{
			if(h* GetComponent<Rigidbody2D>().velocity.x < maxSpeed)
			{
				GetComponent<Rigidbody2D>().AddForce(Vector2.right * h * moveForce);
			}

			if(Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x) > maxSpeed)
			{	// ... set the player's velocity to the maxSpeed in the x axis.
				GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Sign(GetComponent<Rigidbody2D>().velocity.x) * maxSpeed, GetComponent<Rigidbody2D>().velocity.y);
			}
			if(h > 0 && !facingRight)
				// ... flip the player.
				Flip();
			// Otherwise if the input is moving the player left and the player is facing right...
			else if(h < 0 && facingRight)
				// ... flip the player.
				Flip();
		}

		if((grounded||grounded2) && Input.GetButtonDown("Fire2") && canBoost  && !isPause)
		{
			StartCoroutine( Dash(dashDur) );
			isDashing = true;
		}
		//limit fall speed

		if(Mathf.Abs (GetComponent<Rigidbody2D>().velocity.x) > 0 && !isDashing){
			anim.SetBool("Running",true);
		}else{
			anim.SetBool("Running",false);
		}

		if(wallTouch && !(grounded || grounded2))
		{
			if((facingRight && h >= .5 ) || (!facingRight && h <= -.5))
			{
			wallHold = true;
			GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, -0.5f);
			anim.SetBool ("WallHold", true);
			}else{
				wallHold = false;
			}
		}else
		{
			wallHold = false;
		}
		if(wallHold)
		{
				if(Input.GetButtonDown("Jump"))
				{
				float dir = 0;
					if(facingRight)
					{
					dir = 1;
					}
					else{
					dir = -1;
					}
					GetComponent<Rigidbody2D>().AddForce(new Vector2(-h *jumpForce, jumpForce));
					anim.SetBool ("Jump",true);
					anim.SetBool ("WallHold",false); 
					swapDelay = Time.time + .15f;
					wallHold = false;
						int i = Random.Range(0, jumpClips.Length);
						AudioSource.PlayClipAtPoint(jumpClips[i], transform.position);
						clipEnd = Time.time + jumpClips[i].length;
				}

		}else
		{
				anim.SetBool ("WallHold",false);
		}

		if (Input.GetKeyDown (KeyCode.Escape)) {
			isPause = togglePause ();
		}

	}

	IEnumerator Dash(float dashDur) //Coroutine with a single input of a float called boostDur, which we can feed a number when calling
	{
		float time = 0; //create float to store the time this coroutine is operating
		canBoost = false; //set canBoost to false so that we can't keep boosting while boosting
		anim.SetBool ("Dashing",true);
		if (Time.time > clipEnd){
			int i = Random.Range(0, dashClips.Length);
			AudioSource.PlayClipAtPoint(dashClips[i], transform.position);
			clipEnd = Time.time + dashClips[i].length;
		}
		while(dashDur > time) //we call this loop every frame while our custom boostDuration is a higher value than the "time" variable in this coroutine
		{
			if(wallTouch)
			{
				dashDur = 0;
			}
			time += Time.deltaTime; //Increase our "time" variable by the amount of time that it has been since the last update
			if(facingRight)
			{
			GetComponent<Rigidbody2D>().velocity = dashSpeed; //set our rigidbody velocity to a custom velocity every frame, so that we get a steady boost direction like in Megaman
			}else
			{
			GetComponent<Rigidbody2D>().velocity = -dashSpeed;
			}

			yield return 0; //go to next frame
		}
		yield return new WaitForSeconds(dashDelay); //Cooldown time for being able to boost again, if you'd like.
		canBoost = true; //set back to true so that we can boost again.
		isDashing =false;
		anim.SetBool ("Dashing",false);
	}

	
	void Flip ()
	{
		// Switch the way the player is labelled as facing.
		facingRight = !facingRight;

		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
		foreach (Transform t in transform.GetComponentsInChildren<Transform>()) {
			if(t.tag.Equals("Chain"))
			{
				t.localScale = theScale;
			}
		}
	}

	void OnGUI()
	{

		if (isPause) {
			GUI.Window(0, new Rect (Screen.width * .375f, Screen.height * .25f, Screen.width* .25f, Screen.height * .5f), InitialWindow, "Pause Menu", menuStyle);
		}
	}

	void InitialWindow(int windowID) {
		if (GUI.Button (new Rect (Screen.width * .075f, Screen.height * .1f, Screen.width * .1f, Screen.height * .05f), "Level Select", image1)) {
			isPause = togglePause();
			Application.LoadLevel("LevelSelect");
		}

		if (GUI.Button (new Rect (Screen.width * .075f, Screen.height * .2f, Screen.width * .1f, Screen.height * .05f), "Restart Level", image1)) {
			isPause = togglePause ();
			Application.LoadLevel (Application.loadedLevel);
		}

		if (GUI.Button (new Rect (Screen.width * .075f, Screen.height * .3f, Screen.width * .1f, Screen.height * .05f), "Main Menu", image1)) {
			isPause = togglePause();
			Application.LoadLevel("MainMenu");
		}

		if (GUI.Button (new Rect (Screen.width * .075f, Screen.height * .4f, Screen.width* .1f, Screen.height * .05f), "Return", image1))
			isPause = togglePause();
		
	}



	bool togglePause()
	{
		if(Time.timeScale == 0f)
		{
			Time.timeScale = 1f;
			return(false);
		}
		else
		{
			Time.timeScale = 0f;
			return(true);
		}
	}
}
