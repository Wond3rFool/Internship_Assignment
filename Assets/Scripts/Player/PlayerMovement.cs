using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement: MonoBehaviour
{
	[SerializeField]
	private float moveSpeed;

	[SerializeField]
	[Range(0.1f, 1.0f)]
	private float sneakSpeed;

	private Vector2 moveDirection;
	private bool isSneaking;

	public static bool isMoving;

	private void OnMove(InputValue value) 
	{
		moveDirection = value.Get<Vector2>();
	}
	private void OnSneak(InputValue value) 
	{
		isSneaking = value.isPressed;
	}

	private void Update()
    {
		float currentSpeed = isSneaking ? moveSpeed * sneakSpeed : moveSpeed;
		Vector3 movement = new Vector3(moveDirection.x, moveDirection.y, 0) * currentSpeed * Time.deltaTime;

		if(movement.sqrMagnitude < 0.0001f || isSneaking) { isMoving = false; }
		else {isMoving = true;}

		transform.Translate(movement);
	} 
}
