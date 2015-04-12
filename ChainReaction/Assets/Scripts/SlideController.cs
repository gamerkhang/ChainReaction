using UnityEngine;
using System.Collections;

public class SlideController : MonoBehaviour {
	public float speed;
	private GameObject platformprefab;
	private GameObject lastCreatedPlatform;
	private Vector3 lastPlatformPosition;
	public Vector3 offset;

	void Awake()
	{
		speed = 6f;
		lastPlatformPosition = new Vector3 (0, 0, 0);
		offset = new Vector3 (1, 0, 0);
	}
	// Use this for initialization
	void Start () {
		InvokeRepeating ("CreatePlatform", 0, 1f);
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate(-1 * Time.deltaTime, 0, 0 );

	}
	void CreatePlatform()
	{
		lastCreatedPlatform = (GameObject)Instantiate (platformprefab, lastPlatformPosition + offset , Quaternion.identity);	
		lastPlatformPosition = lastCreatedPlatform.transform.position;
	}
}
