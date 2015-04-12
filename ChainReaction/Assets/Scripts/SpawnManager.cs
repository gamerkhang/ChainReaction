using UnityEngine;
using System.Collections;

public class SpawnManager : MonoBehaviour {
	// counter for how many mobs are still live
	public int mobCounter; 
	public GameObject mob1;


	public float maxBoundaryX;
	public float maxBoundaryY;
	public float minBoundaryX;
	public float minBoundaryY;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if(mobCounter < 10){
			SpawnMob ();
			mobCounter+=1;
		}
	}
	void SpawnMob(){
		Instantiate(mob1, new Vector3(Random.Range (minBoundaryX,maxBoundaryX), Random.Range(minBoundaryY,maxBoundaryY), 0), Quaternion.Euler(0, 0, 0));
	}
}
