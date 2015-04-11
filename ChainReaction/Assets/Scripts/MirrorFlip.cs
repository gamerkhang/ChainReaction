using UnityEngine;
using System.Collections;
using System.Linq;

public class MirrorFlip : MonoBehaviour {
	public GameObject upMirror;
	public GameObject rightMirror;
	public GameObject leftMirror;

	public GameObject[] mirrorList;
	public Vector2[] normalList;

	public Vector2 currNormal;
	public GameObject currMirror;
	public int mirror;
	// 0 = up, 1 = right, 2 = left;
	
	// Use this for initialization
	void Start () {
		mirrorList = new GameObject[] { upMirror, rightMirror, leftMirror } ;
		for(int i = 0; i < mirrorList.Length; i++)
			mirrorList[i].GetComponent<Renderer>().enabled = false;
		mirror = Mathf.Clamp(mirror, 0, mirrorList.Length - 1);
		currMirror = mirrorList[mirror];
		currMirror.GetComponent<Renderer>().enabled = true;
		Vector2 normalUp = new Vector2(0f, 1f);
		Vector2 normalRight = new Vector2(1f, 1f);
		normalRight.Normalize();
		Vector2 normalLeft = new Vector2(-1f, 1f);
		normalLeft.Normalize();

		normalList = new Vector2[] { normalUp, normalRight, normalLeft } ;
		currNormal = normalList[mirror];
	}

	private void showNextMirror() {
		mirror = (mirror + 1) % mirrorList.Length;
		for(int i = 0; i < mirrorList.Length; i++)
			if( i != mirror)
				mirrorList[i].GetComponent<Renderer>().enabled = false;
		currMirror = mirrorList[mirror];
		currMirror.GetComponent<Renderer>().enabled = true;
		currNormal = normalList[mirror];
	}

	void OnTriggerStay2D(Collider2D other) {
		if (other.gameObject.tag == "Player" && Input.GetKeyDown (KeyCode.E)) {
			Flip ();
		}
	}
	
	public void Flip(){
		showNextMirror();
		GetComponent<AudioSource>().Play();
	}

	//TODO: turn this into it's own script called Reflector
	//TODO: Make GunBeam actually be a script called Reflectable which has a direction and a rigidbody2D
	public void reflect(BasicGunBeam beam, Vector2 point, float speed) {
		Vector2 reflection = beam.direction - 2 * (Vector2.Dot(beam.direction, currNormal)) * currNormal;
		reflection.Normalize();

		beam.GetComponent<Rigidbody2D>().velocity = new Vector2(speed * reflection.x, speed * reflection.y);
		var angle = Mathf.Atan2(reflection.y, reflection.x) * Mathf.Rad2Deg;
		beam.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
		beam.direction = reflection;
	}
	
	// Update is called once per frame
	void Update () {


	}

}
