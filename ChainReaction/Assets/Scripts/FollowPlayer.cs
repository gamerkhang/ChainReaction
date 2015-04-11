using UnityEngine;
using System.Collections;

public class FollowPlayer : MonoBehaviour
{
	public Vector3 offset;			// The offset at which the Health Bar follows the player.
	
	private Transform player;		// Reference to the player.


	void Awake ()
	{
		// Setting up the reference.
		player = GameObject.FindGameObjectWithTag("Player").transform;
	}

	void Update ()
	{

		// Set the position to the player's position with the offset.
//		transform.position = player.position + offset;

		Vector3 pos2 = new Vector3(Screen.width * .1f, Screen.height * .9f, 10);
		transform.position = Camera.main.ScreenToWorldPoint(pos2);
	}
}
