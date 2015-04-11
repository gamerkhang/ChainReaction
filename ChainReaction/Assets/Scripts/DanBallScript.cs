using UnityEngine;
using System.Collections;

public class DanBallScript : MonoBehaviour {

	public Color damageColor;
	public float ttl = 3.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		ttl -= Time.deltaTime;
		if(ttl<=0)
			Destroy(transform.gameObject);
	}

	void OnTriggerEnter2D(Collider2D coll)
	{
		if (coll.gameObject.tag == "Player") {
			Debug.Log("damaged!");
			coll.gameObject.transform.GetComponent<PlayerHealth>().TakeDamage2(transform,damageColor,.15f);
			Camera.main.GetComponent<CameraShakeScript>().shake = .5f;
			Destroy(transform.gameObject);
		}
		else if(coll.gameObject.tag != "Enemy"){
			Destroy(transform.gameObject);
		}
	}
}
