using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {
	public GameObject door;
	public GameObject[] enemies;
	Animator anim;
	bool allDead;
	// Use this for initialization
	void Start () {
		for(int i = 0; i < enemies.Length; i++){
			enemies[i].SetActive(false);
		}
		anim = door.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		allDead = true;
		for(int i = 0; i < enemies.Length; i++){
			if(enemies[i] != null)
				allDead = false;
		}
		if(allDead)
			anim.SetBool ("button_pressed", true);
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == "Player") {
			for(int i = 0; i < enemies.Length; i++){
				if(enemies[i] != null)
					enemies[i].SetActive(true);
			}
		}
		
	}
}
