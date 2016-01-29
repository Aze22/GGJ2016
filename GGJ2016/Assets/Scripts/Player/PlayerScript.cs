using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {
	float xpos;
	float ypos;
	const float speed = 0.2f;

	// Use this for initialization
	void Start () {
		// Set up initial coordinates
		xpos = transform.position.x;
		ypos = transform.position.y;	
	}
	
	// Update is called once per frame
	void Update () {
		// Get the input from mouse keys
		float dx = Input.GetAxis("Horizontal");
		float dy = Input.GetAxis("Vertical");
		Debug.Log("dx: " + dx + ", dy: " + dy);

		xpos += speed * dx;
		ypos += speed * dy;

		transform.SetXPosition(xpos);
		transform.SetYPosition(ypos);
	}
}