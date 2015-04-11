using UnityEngine;
using System.Collections;

public class BossEnemyScript : MonoBehaviour {

	public GameObject enemy;

	public float spawnFrequency = 1;
	private Transform groundCheck;

	private float dur;

	// Use this for initialization
	void Start () {
		groundCheck = groundCheck = transform.Find("groundCheck");
		dur = spawnFrequency;
	}
	
	// Update is called once per frame
	void Update () {
		dur -= Time.deltaTime;
		if (dur <= 0) {
			dur+=spawnFrequency;
			GameObject ghost = Instantiate(enemy, new Vector3(groundCheck.position.x, transform.position.y, transform.position.z), Quaternion.identity) as GameObject;
			ghost.GetComponent<BasicEnemyAI>().doFall = true;
			if(GetComponent<BasicEnemyAI>().facingRight)
				ghost.GetComponent<BasicEnemyAI>().Flip();
		}
	}
}
