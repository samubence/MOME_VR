using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public bool useCharacterController = true;
	public bool stayStill = true;
	public float stayStillForce = 10;
	public GameObject myHead;
	public float walkSpeed = 1;

	float stickToGroundForce = 1;
	Vector3 origPosition;

	CharacterController myCharacterController;

	void Start () 
	{
		if (useCharacterController) {
			myCharacterController = GetComponent<CharacterController> ();
		}

		origPosition = new Vector3 (transform.position.x, transform.position.y, transform.position.z);	// copy original position
	}

	void Update () 
	{
		Vector2 inputWalk = new Vector2 (Input.GetAxis ("Horizontal"), Input.GetAxis ("Vertical"));
		float inputLift = Input.GetAxis ("JoyLift");

		Vector3 walkDirection = myHead.transform.forward * inputWalk.y + myHead.transform.right * inputWalk.x + myHead.transform.up * inputLift;

		if (stayStill)		// try to go baco to original position, the force increses with distance
		{
			walkDirection += (origPosition - transform.position) * stayStillForce;
		}

		if (useCharacterController) {
			myCharacterController.SimpleMove (walkDirection * walkSpeed);
		} else {
			transform.position += walkDirection * walkSpeed * 0.01f;	// get roughly the same speed as in character controller mode
		}
	}
}
