using UnityEngine;
using System.Collections;

public class SlideController : MonoBehaviour {
	/*public float speed;
	private GameObject platformprefab;
	private GameObject lastCreatedPlatform;
	private Vector3 lastPlatformPosition;
	public Vector3 offset;
	*/
	public GameObject jumpTerrain;
	public GameObject platformTerrain;
	public GameObject player;
	public float startSpawnPosition = 11.2f;
	public int spawnYPos = 0;
	int randomChoice;
	float lastPosition;
	//death reference
	GameObject spike;
	GameObject platform;
	GameObject theplayer;
	bool canSpawn = true;



	// Use this for initialization
	void Start () {
		//InvokeRepeating ("CreatePlatform", 0, 1f);
		lastPosition = startSpawnPosition;

		spike = GameObject.Find ("Spikes");
		//platform = GameObject.FindGameObjectWithTag ("plat");
		theplayer = GameObject.Find ("Cube");
	}
	
	// Update is called once per frame
	void Update () {
		//if this platform's position is passed the spike, spawn another 
		if (gameObject.transform.position.x <= spike.transform.position.x - 3 && canSpawn == true){ //&& theplayer.transform.x >= lastPosition-3) {
			canSpawn = false;
			randomChoice = Random.Range(1,10);
			SpawnTerrain(randomChoice);
			Destroy (gameObject);
		}
		transform.Translate(-10 * Time.deltaTime, 0, 0 );

	}
	void SpawnTerrain(int rand){
		if (rand >= 1 && rand <= 4)
		{
			Instantiate(jumpTerrain, new Vector3(lastPosition + 4, spawnYPos, 0), Quaternion.Euler(0, 0, 0));
			// same as start spawn position as starting terrain

			lastPosition += 8.2f; //this # is random af idk
		}
		
		if (rand >= 5 && rand <= 8)
		{
			Instantiate(platformTerrain, new Vector3(lastPosition, spawnYPos, 0), Quaternion.Euler(0, 0, 0));
			lastPosition += 8.2f;
		}
		
		if (rand >= 9 && rand <= 10)

		{
			Instantiate(platformTerrain, new Vector3(lastPosition, spawnYPos, 0), Quaternion.Euler(0, 0, 0));
			lastPosition += 8.2f;
		}
		

		canSpawn = true;

	}


}
