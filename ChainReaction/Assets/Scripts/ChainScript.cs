using UnityEngine;
using System.Collections;
public class ChainScript : MonoBehaviour {
    public GameObject[] chainLinks;
	public GameObject[] playerStocks;
	public int numStocks = 3;
    public int maxChainLength;
    public int startChainLength;
	public int currentChainLength;
    public GameObject chainLink;
	public Vector2 input;
	public float force;
	public Sprite link;
	public Sprite head;
	public int playerNumber = 0;
	public int minChainLength = 5;
	public bool hasLost = false;
	// Use this for initialization
	void Start () {
	//construct the chain
        chainLinks = new GameObject[maxChainLength];
		currentChainLength = 0;
        for(int i = 0; i < startChainLength; i++)
        {
            //first link
			currentChainLength++;
            if(i == 0)
            {
                chainLinks[i] = (GameObject)Instantiate(chainLink, new Vector2(transform.position.x,transform.position.y), Quaternion.identity);
				chainLinks[i].GetComponent<HingeJoint2D>().enabled = true;
				chainLinks[i].GetComponent<HingeJoint2D>().connectedBody = this.GetComponent<Rigidbody2D>();
				chainLinks[i].GetComponent<HingeJoint2D>().anchor = -chainLinks[i].transform.Find ("ChainLeft").localPosition;
				chainLinks[i].GetComponent<HingeJoint2D>().connectedAnchor = chainLinks[i].transform.Find("ChainLeft").localPosition;
				chainLinks[i].GetComponent<Rigidbody2D>().isKinematic = false;
				chainLinks[i].transform.parent = this.transform;
				chainLinks[i].transform.GetComponent<ChainHeadScript>().chain = transform.GetComponent<ChainScript>();
				chainLinks[i].transform.GetComponent<ChainHeadScript>().source = transform.GetComponent<AudioSource>();

				continue;
            }

			chainLinks[i] = (GameObject)Instantiate(chainLink,chainLinks[i-1].transform.Find("ChainRight").position,Quaternion.identity);
			chainLinks[i].GetComponent<HingeJoint2D>().connectedBody = chainLinks[i-1].GetComponent<Rigidbody2D>();
			chainLinks[i].GetComponent<HingeJoint2D>().anchor = -chainLinks[i].transform.Find ("ChainLeft").localPosition;
			chainLinks[i].GetComponent<HingeJoint2D>().connectedAnchor = chainLinks[i].transform.Find("ChainLeft").localPosition;
			chainLinks[i].transform.parent = this.transform;
			chainLinks[i].transform.GetComponent<ChainHeadScript>().chain = transform.GetComponent<ChainScript>();
			chainLinks[i].transform.GetComponent<ChainHeadScript>().source = transform.GetComponent<AudioSource>();

        }
		chainLinks [currentChainLength - 1].GetComponent<SpriteRenderer> ().sprite = head;
		chainLinks[currentChainLength - 1].GetComponent<ChainHeadScript>().isHead = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (numStocks == 0)
			hasLost = true;
		//first player
		if (playerNumber == 0) {
			input = new Vector2 (Input.GetAxis ("Horizontal"), Input.GetAxis ("Vertical"));
		} else if (playerNumber == 1) {
			input = new Vector2 (Input.GetAxis ("Horizontal2"), Input.GetAxis ("Vertical2"));
		} else if (playerNumber == 2) {
			input = new Vector2 (Input.GetAxis ("Horizontal3"), Input.GetAxis ("Vertical3"));
		} else if (playerNumber == 3) {
			input = new Vector2 (Input.GetAxis ("Horizontal4"), Input.GetAxis ("Vertical4"));
		}

	}
	void FixedUpdate(){
		if (Mathf.Abs (input.x) >=.2f) {
			chainLinks [currentChainLength - 1].GetComponent<Rigidbody2D> ().velocity = new Vector2(force * input.x , chainLinks [currentChainLength - 1].GetComponent<Rigidbody2D> ().velocity.y);
		}
		if (Mathf.Abs (input.y) >=.2f) {
			chainLinks [currentChainLength - 1].GetComponent<Rigidbody2D> ().velocity = new Vector2(chainLinks [currentChainLength - 1].GetComponent<Rigidbody2D> ().velocity.x, force * input.y);
		}
		if (Mathf.Abs (input.x) < .2f && Mathf.Abs (input.y) < .2f) {
			chainLinks[currentChainLength -1].GetComponent<Rigidbody2D>().velocity = Vector2.zero;
		}
	}
	public void addChainLength()
	{
		if (currentChainLength < maxChainLength) {
			chainLinks[currentChainLength] = (GameObject)Instantiate(chainLink,chainLinks[currentChainLength-1].transform.Find("ChainRight").position,Quaternion.identity);
			chainLinks[currentChainLength].GetComponent<HingeJoint2D>().connectedBody = chainLinks[currentChainLength-1].GetComponent<Rigidbody2D>();
			chainLinks[currentChainLength].GetComponent<HingeJoint2D>().anchor = -chainLinks[currentChainLength].transform.Find ("ChainLeft").localPosition;
			chainLinks[currentChainLength].GetComponent<HingeJoint2D>().connectedAnchor = chainLinks[currentChainLength].transform.Find("ChainLeft").localPosition;
			chainLinks[currentChainLength].transform.parent = this.transform;
			chainLinks[currentChainLength].transform.GetComponent<ChainHeadScript>().chain = transform.GetComponent<ChainScript>();
			chainLinks[currentChainLength].transform.GetComponent<ChainHeadScript>().source = transform.GetComponent<AudioSource>();
			chainLinks[currentChainLength].GetComponent<SpriteRenderer>().sprite = head;
			chainLinks[currentChainLength].GetComponent<ChainHeadScript>().isHead = true;
			chainLinks[currentChainLength-1].GetComponent<SpriteRenderer>().sprite = link;
			chainLinks[currentChainLength-1].GetComponent<ChainHeadScript>().isHead = false;
			currentChainLength++;
		}
	}

	public void subChainLength()
	{
		//min chain length
		if (currentChainLength >= minChainLength) {
			Destroy(chainLinks[currentChainLength-1]);
			chainLinks[currentChainLength -2].GetComponent<SpriteRenderer>().sprite = head;
			chainLinks[currentChainLength -2].GetComponent<ChainHeadScript>().isHead = true;
			currentChainLength--;
		}
	}
	public void removeStock()
	{
		if (numStocks > 0) {
			Destroy (playerStocks [numStocks - 1]);
			numStocks--;
		} else {
			hasLost = true;
		}
	}
}
