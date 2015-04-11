using UnityEngine;
using System.Collections;

public class SlamControlScript : MonoBehaviour {
	public GameObject ball;
	private float shotTimer;
	private float timeToShoot;
	private bool isShooting = false;
	public Transform mouth;
	public GameObject player;
	public float ballSpeed = 200f;
	public Animator anim;
	public bool headRight = false;
	public Vector3 moveTo;
	private float maxSpeed;

	public float movingSpeed = 15;
	public float spawningSpeed = 50;
	public float minSqrtDist = 5;
	public float maxSqrtDist = 10;
	
	// Use this for initialization
	void Start () {
		shotTimer = 0;
		Camera.main.GetComponent<SongSelectScript>().currentSong = 1;
		timeToShoot = Random.Range(1,3);
		mouth = transform.Find ("Mouth");
		anim = GetComponent<Animator>();
		player = GameObject.FindGameObjectWithTag("Player");
		maxSpeed = spawningSpeed;

	}
	
	// Update is called once per frame
	void Update () {
	//between 1-3 seconds throw a ball at the player
			Vector3 angleTo = moveTo - transform.position;
			angleTo.Normalize ();
			float dist = Vector3.Distance (transform.position, moveTo);
			float speed = maxSpeed * Time.deltaTime;
			transform.position += new Vector3 (angleTo.x * Mathf.Min (dist, speed), angleTo.y * Mathf.Min (dist, speed), 0);
			if (dist < speed) {
				maxSpeed = movingSpeed;
				float rangeX = Random.Range (minSqrtDist, maxSqrtDist);
				if (Random.Range (0, 2) == 1)
					rangeX *= -1;
				float rangeY = Random.Range (minSqrtDist, maxSqrtDist);
				moveTo = new Vector3(player.transform.position.x + rangeX, player.transform.position.y + rangeY, transform.position.z);
			}
			if(shotTimer >= timeToShoot)
			{
			isShooting = false;
			shotTimer = 0;
			timeToShoot = Random.Range(1,4);
			GameObject Bball;
				Bball = Instantiate(ball,mouth.position,Quaternion.identity) as GameObject;
				Bball.GetComponent<Rigidbody2D>().gravityScale = .5f;
				Bball.GetComponent<Rigidbody2D>().velocity = new Vector2((player.transform.position.x - this.transform.position.x),
			                             (player.transform.position.y - this.transform.position.y)).normalized 
											* ballSpeed;
				int col = Random.Range(1,4);
				if(col == 1)
				{
					Bball.transform.GetComponent<Renderer>().material.color = Color.red;
				Bball.GetComponent<SlamBallScript>().myColor = 0;
				}else if(col == 2)
				{
					Bball.transform.GetComponent<Renderer>().material.color = Color.blue;
					Bball.GetComponent<SlamBallScript>().myColor = 1;
				}else if (col ==3)
				{
					Bball.transform.GetComponent<Renderer>().material.color = Color.green;
					Bball.GetComponent<SlamBallScript>().myColor = 2;
				}
			anim.SetTrigger("Open");
			}
			if(shotTimer >= 1 && timeToShoot != 1 && !isShooting)
			{
				GameObject Bball;
				Bball = Instantiate(ball,mouth.position,Quaternion.identity) as GameObject;
				isShooting = true;
			float randX = Random.value * 5;
				if(player.transform.position.x > this.transform.position.x)
				{
					//shoot to right
				Bball.GetComponent<Rigidbody2D>().velocity = new Vector2(randX,20);
				}else
			{
				
				Bball.GetComponent<Rigidbody2D>().velocity = new Vector2(-randX,20);
			}

				
			
				int col = Random.Range(1,4);
				
			if(col == 1)
			{
				Bball.transform.GetComponent<Renderer>().material.color = Color.red;
				Bball.GetComponent<SlamBallScript>().myColor = 0;
			}else if(col == 2)
			{
				Bball.transform.GetComponent<Renderer>().material.color = Color.blue;
				Bball.GetComponent<SlamBallScript>().myColor = 1;
			}else if (col ==3)
			{
				Bball.transform.GetComponent<Renderer>().material.color = Color.green;
				Bball.GetComponent<SlamBallScript>().myColor = 2;
			}
				anim.SetTrigger("Open");

			}
				shotTimer += Time.deltaTime;


	}
}
