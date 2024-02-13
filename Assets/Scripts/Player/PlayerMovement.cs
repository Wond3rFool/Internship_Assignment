using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
	[SerializeField]
	private float moveSpeed;

	private Vector2 moveDirection;
	private void OnMove(InputValue value) 
	{
		moveDirection = value.Get<Vector2>();
	}
    private void Update()
    {
		transform.Translate(new Vector3(moveDirection.x, moveDirection.y, 0) * moveSpeed * Time.deltaTime);
    }
}
