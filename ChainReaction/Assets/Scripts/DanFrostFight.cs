using UnityEngine;
using System.Collections;

public class DanFrostFight : MonoBehaviour {

	public GameObject projectile;
	public GameObject spawn;

	public const int SPAWN = 0;
	public const int PAUSED = 2;
	public const int MOVING = 3;
	public const int SHOOTING = 4;
	public const int SHOOTING_SPAWN = 5;
	public const int DEAD = 6;

	public float movingSpeed = 2;
	public float playerFarDist = 20;
	public float playerFarSpeed = 5;
	public float minMoveSeek = 2;
	public float maxMoveSeek = 4;

	public float spawningSpeed = 1;
	public float spawningDelay = 2;
	public float spawningDistance = 10;

	public float dyingSpeed = 1;

	public float shootDelay = 1;
	public float shootEndDelay = 0.5f;
	public float shootSpeed = 0.25f;
	public float spawnShootSpeed = 1f;
	public float shootProjectileSpeed = 5;
	public float shootWiggleMoveFactor = 1.10f;
	public float shootWiggleDistFactor = 1.10f;
	public float shootWiggleStartSpeed = 5f;
	public float shootWiggleMaxSpeed = 20f;
	public float shootWiggleStartDist = 0.25f;
	public float shootWiggleMaxDist = 1f;
	public float shootDuration = 5;
	public int totalShots = 10;
	public int spawnShots = 4;

	private int shotsLeft;
	private int shotsSpawned;
	
	private float stateDur;
	private float currentShootWiggleDist;
	private float currentShootWiggleSpeed;
	private Vector2 targetPosition;
	private float maxSpeed;
	private float timer;
	private Vector3 shootStartPos;

	public float deathFallTime = 12;
	public float deathWiggleDistance = 0.05f;
	public float deathWiggleMinDistance = 0.025f;
	public float deathFallSpeed = 10f;
	public float deathFallDist = 0.125f;

	public Transform mouth;

	private Animator anim;

	private GameObject player;

	public GameObject nextBoss;

	private int _state = SPAWN;

	public int state {
		get
		{
			return _state;
		}
		set
		{
			_state = value;
			stateDur = 0;
			timer = 0;
		}
	}

	// Use this for initialization
	void Start () {
		Camera.main.GetComponent<CameraShakeScript>().shake = 0.5f;
		timer = 0.45f;
		shotsLeft = totalShots;
		shotsSpawned = spawnShots;
		mouth = transform.Find ("Mouth");
		player = GameObject.FindGameObjectWithTag ("Player");
		targetPosition = new Vector2 (transform.position.x, transform.position.y + spawningDistance);
		maxSpeed = spawningSpeed;
		anim = this.GetComponent<Animator>();
		player.gameObject.GetComponent<PlayerControl> ().enabled = false;
		player.GetComponentInChildren<LightGun> ().enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		stateDur += Time.deltaTime;
		if (timer > 0 && stateDur > timer)
						onTimerUp ();
		Vector2 myPosition = getPositionAs2();
		Vector2 angleTo = targetPosition - myPosition;
		angleTo.Normalize ();
		float dist = Vector2.Distance (myPosition, targetPosition);
		float speed = maxSpeed * Time.deltaTime;
		transform.position += new Vector3 (angleTo.x * Mathf.Min (dist, speed), angleTo.y * Mathf.Min (dist, speed), 0);
		if (dist < speed)
						onFinishedMovement ();
	}

	private void onFinishedMovement(){
		if (state==SPAWN) {
			state = PAUSED;
			timer = spawningDelay;
		}
		else if(state==MOVING){
			if(stateDur > shootDuration){
				if(Random.Range(0, shotsLeft--)<shotsSpawned){
					shotsSpawned--;
					state = SHOOTING_SPAWN;
				}
				else{
					state = SHOOTING;
					shootStartPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
					currentShootWiggleSpeed = shootWiggleStartSpeed;
					currentShootWiggleDist = shootWiggleStartDist;
				}
				if(shotsLeft<=0){
					shotsLeft = totalShots;
					shotsSpawned = spawnShots;
				}
				timer = shootDelay;
				anim.SetTrigger("startAttack");
			}
			else{
				if(Vector3.Distance(player.transform.position, transform.position)>playerFarDist)
					maxSpeed = playerFarSpeed;
				else
					maxSpeed = movingSpeed;
				targetPosition = selectRandomPointNear(player.transform.position, minMoveSeek, maxMoveSeek);
			}
		}
		else if(state==SHOOTING){
			if(stateDur > shootDelay){
				targetPosition = selectRandomPointNear(shootStartPos, currentShootWiggleDist, currentShootWiggleDist, true);
				maxSpeed = currentShootWiggleSpeed;
				currentShootWiggleDist*=shootWiggleDistFactor;
				currentShootWiggleSpeed*=shootWiggleMoveFactor;
			}
		}
		else if(state==DEAD){
			float rangeX = Random.Range (deathWiggleMinDistance, deathWiggleDistance);
			if(transform.position.x>shootStartPos.x)
				rangeX*=-1;
			targetPosition = new Vector2 (shootStartPos.x + rangeX, transform.position.y - deathFallDist);
		}
	}

	private void onTimerUp(){
		if (state==PAUSED) {
			player.gameObject.GetComponent<PlayerControl> ().enabled = true;
			player.GetComponentInChildren<LightGun> ().enabled = true;
			state= MOVING;
			if(Vector3.Distance(player.transform.position, transform.position)>playerFarDist)
				maxSpeed = playerFarSpeed;
			else
				maxSpeed = movingSpeed;
			targetPosition = selectRandomPointNear(player.transform.position, minMoveSeek, maxMoveSeek);
		}
		else if(state==SPAWN){
			Camera.main.GetComponent<CameraShakeScript>().shake = 0.5f;
			timer += 0.45f;
		}
		else if(state==SHOOTING){
			//shoot a projectile here
			GameObject proj = Instantiate(projectile,mouth.position,Quaternion.identity) as GameObject;
			proj.GetComponent<Rigidbody2D>().velocity = new Vector2((player.transform.position.x  + 2 - this.transform.position.x),
			                                         (player.transform.position.y + 3f - this.transform.position.y)).normalized * shootProjectileSpeed;
			DanBallScript script = proj.GetComponent<DanBallScript>();
			int rand = Random.Range(0, 4);
			if(rand==0){
				((SpriteRenderer)proj.GetComponent<Renderer>()).color = Color.red;
				script.damageColor = new Color(0f, 0.5f, 0.5f);
			}
			else if(rand==1){
				((SpriteRenderer)proj.GetComponent<Renderer>()).color = Color.green;
				script.damageColor = new Color(0.5f, 0f, 0.5f);
			}
			else {
				((SpriteRenderer)proj.GetComponent<Renderer>()).color = Color.blue;
				script.damageColor = new Color(0.5f, 0.5f, 0f);
			}
			timer+=shootSpeed;
			if(stateDur > shootDuration){
				state = PAUSED;
				timer = shootEndDelay;
				anim.SetTrigger("endAttack");
			}
		}
		else if(state==SHOOTING_SPAWN){
			GameObject proj = Instantiate(spawn,mouth.position,Quaternion.identity) as GameObject;
			if(player.transform.position.x < transform.position.x)
				proj.GetComponent<BasicEnemyAI>().Flip();
			timer+=spawnShootSpeed;
			if(stateDur > shootDuration){
				state = PAUSED;
				timer = shootEndDelay;
				anim.SetTrigger("endAttack");
			}
		}
		else if(state==DEAD){
			GameObject barkley = Instantiate(nextBoss,transform.position,Quaternion.identity) as GameObject;
			barkley.GetComponent<SlamControlScript>().moveTo = shootStartPos;

			Destroy(gameObject);
		}
	}

	private Vector2 selectRandomPointNear(Vector3 point, float minSqrtDist, float maxSqrtDist){
		return selectRandomPointNear (point, minSqrtDist, maxSqrtDist, false);
	}

	private Vector2 selectRandomPointNear(Vector3 point, float minSqrtDist, float maxSqrtDist, bool useNegatY){
		float rangeX = Random.Range (minSqrtDist, maxSqrtDist);
		if (Random.Range (0, 2) == 1)
			rangeX *= -1;
		float rangeY = Random.Range (minSqrtDist, maxSqrtDist);
		if (useNegatY&&Random.Range (0, 2) == 1)
			rangeY *= -1;
		return new Vector2(point.x + rangeX, point.y + rangeY);
	}

	private Vector2 getPositionAs2(){
		return new Vector2 (transform.position.x, transform.position.y);
	}

	public void onDeath(){
		if (state == DEAD)
						return;
		state = DEAD;
		anim.SetTrigger ("death");
		maxSpeed = deathFallSpeed;
		timer = deathFallTime;
		shootStartPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
		float rangeX = Random.Range (deathWiggleMinDistance, deathWiggleDistance);
		targetPosition = new Vector2 (shootStartPos.x + rangeX, transform.position.y - deathFallDist);
	}
}
