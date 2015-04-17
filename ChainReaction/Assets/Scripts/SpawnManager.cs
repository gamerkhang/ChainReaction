using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class SpawnManager : MonoBehaviour {
	// counter for how many mobs are still live
	public int mobCounter; 
	public int maxMobs;
	public GameObject spawnMarker;
	public GameObject[] mobArray;
	public List<GameObject> mobList= new List<GameObject>();
	public float maxBoundaryX;
	public float maxBoundaryY;
	public float minBoundaryX;
	public float minBoundaryY;
	public float spawnTimer;
	public float spawnDelay;
	public float spawnMarkerDelay;
	// Use this for initialization
	void Start () {
		spawnTimer = 0;
	}
	
	// Update is called once per frame
	void Update () {
		spawnTimer += Time.deltaTime;
		if((mobCounter < maxMobs) && (spawnTimer > spawnDelay)){
			StartCoroutine(SpawnMob(spawnTimer));
			spawnTimer = 0;
		}

	}

	IEnumerator SpawnMob(float spawnDelay){
		Vector3 pos = new Vector3 (Random.Range (minBoundaryX, maxBoundaryX),
		                          Random.Range (minBoundaryY, maxBoundaryY), 0);
		GameObject spawnPoint = (GameObject) Instantiate(spawnMarker, pos, Quaternion.identity);
		yield return new WaitForSeconds(spawnDelay);
		Destroy (spawnPoint);
		Instantiate (mobArray[Random.Range (0,3)], pos, Quaternion.identity);
	}
}
