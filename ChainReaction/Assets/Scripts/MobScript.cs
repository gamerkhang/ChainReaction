using UnityEngine;
using System.Collections;

public class MobScript : MonoBehaviour {
	public GameObject mob1;
	public float speed;
	public float maxBoundaryX;
	public float maxBoundaryY;
	public float minBoundaryX;
	public float minBoundaryY;
	//random position on the map 
	public Vector3 targetPosition;
	public Vector2 direction;


	// Use this for initialization
	void Start () {
		targetPosition.x = Random.Range (minBoundaryX,maxBoundaryX);
		targetPosition.y = Random.Range (minBoundaryX, maxBoundaryY);
		targetPosition.z = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (Vector3.Distance(transform.position,targetPosition) < .1f){
			targetPosition.x = Random.Range (minBoundaryX,maxBoundaryX);
			targetPosition.y = Random.Range (minBoundaryX, maxBoundaryY);
			targetPosition.z = 0;
		}

	}

	void FixedUpdate(){
		direction = (targetPosition - this.transform.position).normalized;
		this.transform.GetComponent<Rigidbody2D> ().velocity = direction * speed;
	}
}
