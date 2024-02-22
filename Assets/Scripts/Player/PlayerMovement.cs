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

	private Animator animator;
	private PlayerHealth health;

	private Vector2 moveDirection;
	private bool isSneaking;

	public static bool isMoving;

	private void Awake()
	{
		animator = GetComponentInChildren<Animator>();
		health = GetComponent<PlayerHealth>();
	}

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
		if(!health.IsDead) 
		{
			float currentSpeed = isSneaking ? moveSpeed * sneakSpeed : moveSpeed;
			Vector3 movement = new Vector3(moveDirection.x, moveDirection.y, 0) * currentSpeed * Time.deltaTime;

			if(movement.sqrMagnitude < 0.0001f)
			{
				isMoving = false;
				animator.SetBool("isMoving", false);
				animator.SetBool("isSneaking", false);
			}
			else
			{
				animator.SetBool("isMoving", true);
				isMoving = true;

				if(isSneaking)
				{
					animator.SetBool("isSneaking", true);
					isMoving = false;
				}
				else
				{
					animator.SetBool("isSneaking", false);
				}
			}
			transform.Translate(movement);
		}
	} 
}
