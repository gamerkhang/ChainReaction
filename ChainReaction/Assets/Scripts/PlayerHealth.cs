using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{	
	public float healthFactor = 100f;					// The player's health.
	public float[] health; // 0 = red, 1 = blue, 2 = green
	public float repeatDamagePeriod = 2f;		// How frequently the player can be damaged.
	public AudioClip[] ouchClips;			// Array of clips to play when the player is damaged.
	public float hurtForce = 10f;				// The force with which the player is pushed when hurt.
	public float damageAmount = 10f;			// The amount of damage to take when enemies touch the player

	private SpriteRenderer[] healthBar;			// Reference to the sprite renderer of the health bar. 0 = red, 1 = blue, 2 = green
	private float lastHitTime;					// The time at which the player was last hit.
	private Vector3 healthScale;				// The local scale of the health bar initially (with full health).
//	private PlayerControl playerControl;		// Reference to the PlayerControl script.
	private Animator anim;						// Reference to the Animator on the player

	public bool isDead = false;
	private bool deathShake = false;
	public GUIStyle menuStyle;
	public GUIStyle image1;
	private bool openLoseScreen = false;

	ColorChangeScript script;
	public AudioClip deadClip;	
	private float clipEnd;
	private bool playDeathSound = true;

	void Awake ()
	{
		// Setting up references.
//		playerControl = GetComponent<PlayerControl>();
		health = new float[] { healthFactor, healthFactor, healthFactor } ;
		healthBar = new SpriteRenderer[] 
		{
			GameObject.Find("RedHealthBar").GetComponent<SpriteRenderer>(), 
			GameObject.Find("BlueHealthBar").GetComponent<SpriteRenderer>(),
			GameObject.Find("GreenHealthBar").GetComponent<SpriteRenderer>()
		};
		anim = GetComponent<Animator>();

		// Getting the intial scale of the healthbar (whilst the player has full health).
		healthScale = healthBar[0].transform.localScale;
		script = gameObject.GetComponent<ColorChangeScript> ();

		UpdateHealthBar ();
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == "Spikes") {
			isDead = true;
		}
	}

	void Update(){
//		if (health <= 0f)
		if(script.IsBlack())
			isDead = true;

		UpdateHealthBar();
	}
	/*
	void OnCollisionEnter2D (Collision2D col)
	{
		// If the colliding gameobject is an Enemy...
		if(col.gameObject.tag == "Enemy")
		{
			// ... and if the time exceeds the time of the last hit plus the time between hits...
			if (Time.time > lastHitTime + repeatDamagePeriod) 
			{
				// ... and if the player still has health...
				if(health > 0f)
				{
					// ... take damage and reset the lastHitTime.
					TakeDamage(col.transform); 
					lastHitTime = Time.time; 
					script.applyDamage(Color.black, damageAmount * .01f);

					if(health <= 0f)
						isDead = true;
				}
				// If the player doesn't have health, do some stuff, let him fall into the river to reload the level.
				else
				{
					isDead = true;
				}
			}
		}
	}
	*/
	IEnumerator MyMethod() {
		yield return new WaitForSeconds(1f);
		openLoseScreen = true;
		// ... disable user Player Control script
		GetComponent<PlayerControl>().enabled = false;
	}

	void OnGUI()
	{
		if (isDead) {
			if (playDeathSound){
				AudioSource.PlayClipAtPoint(deadClip, transform.position);
				playDeathSound = false;
			}
			if(!deathShake) {
				Camera.main.GetComponent<CameraShakeScript>().shake = .5f;
				deathShake = true;
			}
			anim.SetTrigger("Dead");
			if (Input.GetKeyDown (KeyCode.R)) {
				Application.LoadLevel (Application.loadedLevel);
			}
			StartCoroutine(MyMethod());
			if(openLoseScreen)
				GUI.Window (0, new Rect (Screen.width * .25f, Screen.height * .1f, Screen.width * .5f, Screen.height * .75f), InitialWindow, "", menuStyle);
		}
	}

	void InitialWindow(int windowID) {
		if (GUI.Button (new Rect (Screen.width * .38f, Screen.height * .60f, Screen.width * .1f, Screen.height * .1f), "Quit", image1)) {
			Application.LoadLevel("MainMenu");
		}
		
	}

	void TakeDamage (Transform enemy)
	{
		// Make sure the player can't jump.
		//playerControl.jump = false;

		// Create a vector that's from the enemy to the player with an upwards boost.
		Vector3 hurtVector = transform.position - enemy.position + Vector3.up * 5f;

		// Add a force to the player in the direction of the vector and multiply by the hurtForce.
		GetComponent<Rigidbody2D>().AddForce(hurtVector * hurtForce);

		// Reduce the player's health by 10.
		UpdateHealthAmounts();

		// Update what the health bar looks like.
		UpdateHealthBar();

		// Play a random clip of the player getting hurt.
//		int i = Random.Range (0, ouchClips.Length);
//		AudioSource.PlayClipAtPoint(ouchClips[i], transform.position);
	}

	public void TakeDamage2 (Transform enemy, Color damageColor, float damageFactor)
	{
			// ... and if the player still has health...
			if (!script.IsBlack()) {
				// Make sure the player can't jump.
				//playerControl.jump = false;
		
				// Create a vector that's from the enemy to the player with an upwards boost.
//				Vector3 hurtVector = transform.position - enemy.position + Vector3.up * 5f;
		
				// Add a force to the player in the direction of the vector and multiply by the hurtForce.
//				rigidbody2D.AddForce (hurtVector * hurtForce);
		
				// Reduce the player's health by 10.
				script.applyDamage (damageColor, damageFactor);
				//Debug.Log("Before: \nR:" + health[0] + "\nG:" + health[1] + "\nB:" + health[2]);
				UpdateHealthAmounts();
				//Debug.Log("After: \nR:" + health[0] + "\nG:" + health[1] + "\nB:" + health[2]);
			// Update what the health bar looks like.
				UpdateHealthBar ();

				lastHitTime = Time.time; 
				
		
				Camera.main.GetComponent<CameraShakeScript>().shake = .05f;
				// Play a random clip of the player getting hurt.
				if (Time.time > clipEnd){
						int i = Random.Range (0, ouchClips.Length);
					AudioSource.PlayClipAtPoint(ouchClips[i], transform.position);
					clipEnd = Time.time + ouchClips[i].length;
				}
			}
	}


	public void UpdateHealthBar ()
	{
		// Set the health bar's colour to proportion of the way between green and red based on the player's health.
		healthBar[0].material.color = Color.Lerp(Color.red, Color.black, 1 - health[0] * 0.01f);
		// Set the scale of the health bar to be proportional to the player's health.
		healthBar[0].transform.localScale = new Vector3(healthScale.x * health[0] * 0.01f, 1, 1);
		healthBar[1].material.color = Color.Lerp(Color.green, Color.black, 1 - health[1] * 0.01f);
		// Set the scale of the health bar to be proportional to the player's health.
		healthBar[1].transform.localScale = new Vector3(healthScale.x * health[1] * 0.01f, 1, 1);
		healthBar[2].material.color = Color.Lerp(Color.blue, Color.black, 1 - health[2] * 0.01f);
		// Set the scale of the health bar to be proportional to the player's health.
		healthBar[2].transform.localScale = new Vector3(healthScale.x * health[2] * 0.01f, 1, 1);
	}

	private void UpdateHealthAmounts() {
		health [0] = script.MyColor.r * healthFactor;
		health [1] = script.MyColor.g * healthFactor;
		health [2] = script.MyColor.b * healthFactor;
	}

	public float healthAverage () {
		return (health[0] + health[1] + health[2]) / health.Length;
	}
	
	public void Heal (float healthBonus)
	{
		script.applyDamage(Color.white, healthBonus * .01f);
		UpdateHealthAmounts();
		UpdateHealthBar();

	}

}
