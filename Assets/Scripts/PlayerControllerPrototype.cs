using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerProtototype : MonoBehaviour //Controller script for player movement and camera functionality
{
    [SerializeField] private Rigidbody playerRb; //Main player body
	[SerializeField] private GameObject playerCamera; //Player camera body

    private float walkSpeed = 15f; //Movement Variables

	private float cameraSensitivity = 5f; //Camera Variables
	private float cameraX = 0f;
	private float cameraY = 0f;

    void Start()
    {
        playerRb = GetComponent<Rigidbody>(); //Auto-assign main player body (Rigidbody)
		playerCamera = GameObject.Find("Main Camera"); //Auto-assign player camera body
	}

    void FixedUpdate() //FixedUpdate() is used as controls should be subject to 'Time.timeScale()' to allow for pausing
	{
		//Rotation, should move 'main player body' left and right (in the XZ plane/Y axis) and the 'player camera body' up and down, (in the YZ Plane/X axis)
		cameraX = InputControllerPrototype.xRotation * cameraSensitivity;
		cameraY = InputControllerPrototype.yRotation * cameraSensitivity;

		transform.localRotation = Quaternion.Euler(transform.localRotation.x, cameraX, transform.localRotation.z); //Turns main player body left and right
		playerCamera.transform.localRotation = Quaternion.Euler(-cameraY, playerCamera.transform.localRotation.y, playerCamera.transform.localRotation.z); //Turns player camera body up and down

		//Position, should move according to local space, via use of the transform.TransformDirection() function. 
		//transform.TransformDirection() is a function that converts global oriented vectors to local oriented vectors
		//e.g. [Vector3.forward == Vector3(0,0,1)] would move in the global Z axis but with transform.TransformDirection() it changes to forward relative to the script's GameObject.

		if (!InputControllerPrototype.forwardMove && !InputControllerPrototype.backwardMove && !InputControllerPrototype.leftMove && !InputControllerPrototype.rightMove)
        {
			playerRb.velocity = Vector3.zero;
		}

		//Forward Inputs
		else if (InputControllerPrototype.forwardMove && !InputControllerPrototype.leftMove && !InputControllerPrototype.rightMove)
		{
			playerRb.velocity = transform.TransformDirection(Vector3.forward) * walkSpeed; //This is a local version of [... = new Vector3(X, Y, Z) * float]
        }
		else if (InputControllerPrototype.forwardMove && InputControllerPrototype.leftMove && !InputControllerPrototype.rightMove)
		{
			playerRb.velocity = transform.TransformDirection(Vector3.forward + Vector3.left) * walkSpeed; 
		}
		else if (InputControllerPrototype.forwardMove && !InputControllerPrototype.leftMove && InputControllerPrototype.rightMove)
		{
			playerRb.velocity = transform.TransformDirection(Vector3.forward + Vector3.right) * walkSpeed; 
		}

		//Backward Inputs
		else if (InputControllerPrototype.backwardMove && !InputControllerPrototype.leftMove && !InputControllerPrototype.rightMove)
		{
			playerRb.velocity = transform.TransformDirection(Vector3.back) * walkSpeed;
		}
		else if (InputControllerPrototype.backwardMove && InputControllerPrototype.leftMove && !InputControllerPrototype.rightMove)
		{
			playerRb.velocity = transform.TransformDirection(Vector3.back + Vector3.left) * walkSpeed;
		}
		else if (InputControllerPrototype.backwardMove && !InputControllerPrototype.leftMove && InputControllerPrototype.rightMove)
		{
			playerRb.velocity = transform.TransformDirection(Vector3.back + Vector3.right) * walkSpeed;
		}

		//LR Inputs
		else if (InputControllerPrototype.leftMove && !InputControllerPrototype.forwardMove && !InputControllerPrototype.backwardMove)
		{
			playerRb.velocity = transform.TransformDirection(Vector3.left) * walkSpeed;
		}
		else if (InputControllerPrototype.rightMove && !InputControllerPrototype.forwardMove && !InputControllerPrototype.backwardMove)
		{
			playerRb.velocity = transform.TransformDirection(Vector3.right) * walkSpeed;
		}
	}
}
