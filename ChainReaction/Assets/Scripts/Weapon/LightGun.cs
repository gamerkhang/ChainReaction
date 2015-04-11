using UnityEngine;
using System.Collections;

public class LightGun : MonoBehaviour {
	public GunFlare gunFlare;
	public Rigidbody2D gunBeam;
	public PlayerControl playerCtrl;
	public bool facingRight;
	public float speed;
	public float multiplier;
	public Color currColor;
	public float cooldown = 0.2f;
	private float timeElapsed;
	
	// Use this for initialization
	void Start () {
		playerCtrl = this.GetComponentInParent<PlayerControl>();
		facingRight = playerCtrl.facingRight;
	}
	
	// Update is called once per frame
	void Update () {
		timeElapsed += Time.deltaTime;
		Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		Vector3 dir = pos - transform.position;
		//if(facingRight != playerCtrl.facingRight)
		//	Flip ();
		float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
		transform.localRotation = Quaternion.AngleAxis(angle, Vector3.forward);

		//angle = Mathf.Atan2(dir.y, facingRight ? -dir.x : dir.x) * Mathf.Rad2Deg;
		//if(!facingRight)
		//	transform.localRotation = Quaternion.AngleAxis(angle, Vector3.forward);
		if(Input.GetMouseButton(0) && !this.GetComponentInParent<PlayerHealth>().isDead && timeElapsed >= cooldown) {
			//Gun flare effect
			Instantiate(gunFlare, transform.position, transform.rotation);

			//Shoot out a gun 
			Rigidbody2D gunBeamInstance = Instantiate(gunBeam, new Vector3(transform.position.x, transform.position.y, 0f), transform.localRotation)
				as Rigidbody2D;
			dir.z = 0;
			dir.Normalize(); 
			gunBeamInstance.velocity = new Vector2(speed * dir.x, speed * dir.y);
			gunBeamInstance.GetComponentInParent<SpriteRenderer>().color = currColor;
			BasicGunBeam currBeam =  gunBeamInstance.GetComponentInParent<BasicGunBeam>();
			currBeam.direction = dir;
			currBeam.speed = speed;
			currBeam.color = currColor;
			currBeam.multiplier = multiplier;
			timeElapsed = 0;
		}
	}

	//private void Flip() {
		//facingRight = !facingRight;
		//var theScale = transform.localScale;
		//theScale.y *= -1;
		//theScale.x *= -1;
		//transform.localScale = theScale;
	//}
	// Kills the object after a certain amount of time
	private IEnumerator Kill(GameObject gameObject, float time) {
		yield return new WaitForSeconds (time);
		Destroy (gameObject);
	}
}
