using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BasicGunBeam : MonoBehaviour {
	public float duration;
	public Vector2 direction { get; set; }
	public float speed { get; set; }
	public Color color;
	public float multiplier { get; set; }

	private HashSet<string> ignore;

	void Awake() {
		ignore = new HashSet<string>();
		ignore.Add("Player");
		ignore.Add("Enemy");
		ignore.Add("Light");
		ignore.Add("LightIgnore");
		ignore.Add("Button");
		ignore.Add("EndDoor");
	}

	void Start() {
		Destroy(transform.root.gameObject, duration);
	}

	// Update is called once per frame
	void Update () {
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == "LightObject") {
			this.GetComponentInParent<Rigidbody2D>().velocity = new Vector2(0,0);
			RaycastHit2D cast = Physics2D.Raycast(transform.position, new Vector2(direction.x, direction.y));
			if (cast != null)
			{
				BasicGunBeam beam = other.gameObject.GetComponentInParent<BasicGunBeam>();
				this.GetComponentInParent<Rigidbody2D>().velocity = new Vector2(0,0);
				other.GetComponentInParent<MirrorFlip>().reflect (this, cast.point, speed);
			}
		}
		else if(other.gameObject.layer == 10) {
			other.GetComponentInParent<Attackable>().TakeHit(color, multiplier);
			Destroy (this.gameObject, 0.05f);
		}
		else if(!ignore.Contains(other.gameObject.tag)) {
			Destroy(this.gameObject);
		}
	}
}
