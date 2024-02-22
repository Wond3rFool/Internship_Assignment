using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateToMouse : MonoBehaviour
{
	public Vector2 PointerPostion { get; set; }

	private void Update()
	{
		Vector2 direction = (PointerPostion - (Vector2)transform.position).normalized;
		transform.up = direction;
	}
}
