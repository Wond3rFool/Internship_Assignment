using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
	// Start is called before the first frame update

	private Vector2 moveVal;

	[SerializeField]
	private float moveSpeed;

	private void OnMove(InputValue value) 
	{
		moveVal = value.Get<Vector2>();
	}
    // Update is called once per frame
    void Update()
    {
		transform.Translate(new Vector3(moveVal.x, moveVal.y, 0) * moveSpeed * Time.deltaTime);
    }
}
