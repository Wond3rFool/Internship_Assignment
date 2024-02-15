using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerActions: MonoBehaviour
{
	private Vector3 mouseDirection; 
	private Vector2 mouseInput;
	private WeaponParent weaponParent;

	private bool isShooting; 
	private void OnShoot(InputValue value)
	{
		isShooting = value.isPressed;
	}

	private void OnPointerPosition(InputValue value)
	{
		mouseDirection = value.Get<Vector2>();
	}
	private void Awake()
	{ 
		weaponParent = GetComponentInChildren<WeaponParent>();
	}
	private void Update()
	{
		mouseInput = GetMousePosition();
		weaponParent.PointerPostion = mouseInput;
	}

	private Vector2 GetMousePosition()
	{
		mouseDirection.z = Camera.main.nearClipPlane;
		return Camera.main.ScreenToWorldPoint(mouseDirection);
	}
}
