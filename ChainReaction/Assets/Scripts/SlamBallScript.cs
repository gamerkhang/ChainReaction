using UnityEngine;
using System.Collections;

public class SlamBallScript : MonoBehaviour {
	public float damage = 10f;
	public Color color;
	public int myColor;
	// Use this for initialization
	void Start () {
		Destroy (gameObject,3);
		if(myColor == 0)
		{
			Debug.Log ("red");
			color = new Color(.2f,.5f,.5f);
		}else if(myColor == 1)
		{
			
			Debug.Log ("blue");
			color = new Color(.5f,.5f,.2f);
		}else
		{
			
			Debug.Log ("green");
			color = new Color(.5f,.2f,.5f);
		}
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnCollisionEnter2D(Collision2D coll)
	{
		if (coll.gameObject.tag == "Player") {
			coll.gameObject.transform.GetComponent<PlayerHealth>().TakeDamage2(transform,color,.3f);
			coll.gameObject.transform.GetComponent<SpriteColorChangeScript>().applyDamage(color,.3f);
			Camera.main.GetComponent<CameraShakeScript>().shake = .5f;
			Destroy(transform.gameObject);
		}
	}
}
