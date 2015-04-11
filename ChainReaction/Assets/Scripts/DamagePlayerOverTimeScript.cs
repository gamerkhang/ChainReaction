using UnityEngine;
using System.Collections;

public class DamagePlayerOverTimeScript : MonoBehaviour {

	public float damageFactor;
	public Color damageColor;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnTriggerStay2D(Collider2D col)
	{
		// If the alien hits the trigger...
		if (col.gameObject.layer == LayerMask.NameToLayer("Player")) {
			PlayerHealth playerHealth = col.GetComponent<PlayerHealth>();
//			ColorChangeScript s = col.gameObject.GetComponent<ColorChangeScript>();
//			s.applyDamage(damageColor, damageFactor*Time.deltaTime);
			playerHealth.TakeDamage2(transform, damageColor, damageFactor*Time.deltaTime);

		}
	}
}
