using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal.Internal;
using UnityEngine.UIElements;

public class InputControllerPrototype : MonoBehaviour //A Modular Script to track inputs and communicate with 'acting scripts'
{
    public static bool forwardMove = false; //Static booleans to interact with PlayerController Script
	public static bool backwardMove = false;
	public static bool leftMove = false;
	public static bool rightMove = false;

	[SerializeField] private KeyCode forwardKey; //Reassignable Keycodes for computer controls
	[SerializeField] private KeyCode backwardKey;
	[SerializeField] private KeyCode leftKey;
	[SerializeField] private KeyCode rightKey;

	public static float xRotation = 0f; //Rotation Variables for mouse input on computer
	public static float yRotation = 0f;

	void Awake() //Awake is used to give priority
    {
#if UNITY_STANDALONE_WIN || UNITY_STANDALONE_OSX// Windows Controls

		forwardKey = KeyCode.W; //Keycode assignments
		backwardKey = KeyCode.S;
		leftKey = KeyCode.A;
		rightKey = KeyCode.D;

		UnityEngine.Cursor.lockState = CursorLockMode.Locked; //Hides Cursor

#elif UNITY_ANDROID //Android Controls, to be decided on later

#endif
	}

	void Update() //Update() is used as controls should not be subject to 'Time.timeScale()' to allow use during pausing.
    {
#if UNITY_STANDALONE_WIN || UNITY_STANDALONE_OSX// Windows Controls

		//Mouse Axis Inputs
		xRotation += Input.GetAxis("Mouse X");
		yRotation += Input.GetAxis("Mouse Y");

		//Key Press Controls
		//if (!Input.GetKey(forwardKey) && !Input.GetKey(backwardKey) && !Input.GetKey(leftKey) && !Input.GetKey(rightKey)) //Movement Cancel
		//{
		//	NoMovement();
		//}

		if (!Input.GetKey(forwardKey) && !Input.GetKey(backwardKey)) //Movement Cancel
		{ 
			NoUpDownMovement();
		}
		else if (Input.GetKey(forwardKey))
		{
			ForwardMovement();
		}
		else if (Input.GetKey(backwardKey))
		{
			BackwardMovement();
		}

		if (!Input.GetKey(leftKey) && !Input.GetKey(rightKey)) //Movement Cancel
		{
			NoLRMovement();
		}
		else if (Input.GetKey(leftKey))
		{
			LeftMovement();
		}
		else if (Input.GetKey(rightKey))
		{
			RightMovement();
		}
#elif UNITY_ANDROID //Android Controls, to be decided on later

#endif
	}

	//The following functions implement the booleans associated with movement
	//It turns off the unused booleans in that command to prevent any bugs from occuring from

    public void ForwardMovement()
    { 
		forwardMove = true; //Forward Input
		backwardMove = false;
	}

	public void BackwardMovement()
	{
		forwardMove = false;
		backwardMove = true; //Backward Input
	}

	public void LeftMovement()
	{
		leftMove = true; //Left Input
		rightMove = false;
	}

	public void RightMovement()
	{
		leftMove = false;
		rightMove = true; //Right Input
	}

	public void NoMovement() //No Input
	{
		forwardMove = false;
		backwardMove = false;
		leftMove = false;
		rightMove = false;
	}

	public void NoUpDownMovement() //No Input
	{
		forwardMove = false;
		backwardMove = false;
	}

	public void NoLRMovement() //No Input
	{
		leftMove = false;
		rightMove = false;
	}
} 
