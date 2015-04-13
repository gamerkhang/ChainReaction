using UnityEngine;
using System.Collections;

public class CameraShakeScript : MonoBehaviour {
	
	// How long the object should shake for.
	public float shake = 0f;
	
	// Amplitude of the shake. A larger value shakes the camera harder.
	public float shakeAmount = 2f;
	public float decreaseFactor = 1.0f;
	
	public Vector3 originalPos;

	private bool shakeStart;
	
	void Awake()
	{
		originalPos = transform.position;
	}

	
	void Update()
	{
		//if shakestart then set original and start shaking
	
		//if shake has started and still shake left
		if (shake > 0 )
		{
			Vector3 newVect = new Vector3(Camera.main.transform.localPosition.x + Random.insideUnitCircle.x * shakeAmount * shake, 
			                          Camera.main.transform.localPosition.y + Random.insideUnitCircle.y * shakeAmount * shake, 
			                          Camera.main.transform.localPosition.z);
			Camera.main.transform.localPosition = newVect;
			
			shake -= Time.deltaTime * decreaseFactor;
		}
		else if(shake <= 0 )
		{
			shake = 0f;
			Camera.main.transform.localPosition = originalPos;
		}
		
	}
}

