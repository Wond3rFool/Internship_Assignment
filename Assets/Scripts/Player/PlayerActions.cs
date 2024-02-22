using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerActions: MonoBehaviour
{
	[SerializeField]
	private GameObject projectilePrefab;
	[SerializeField]
	private Transform firePoint;
	[SerializeField]
	private float fireRate = 0.5f;


	private Vector3 mouseDirection;
	private Vector2 mouseInput;
	private WeaponParent weaponParent;

	private bool isShooting;
	private float shotTimer;

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
		if(isShooting && Time.time >= shotTimer)
		{
			Shoot();
			shotTimer = Time.time + 1f / fireRate;
		}
	}

	private Vector2 GetMousePosition()
	{
		mouseDirection.z = Camera.main.nearClipPlane;
		return Camera.main.ScreenToWorldPoint(mouseDirection);
	}

	private void Shoot()
	{
		if(projectilePrefab != null && firePoint != null)
		{
			GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);

			Vector2 direction = (mouseInput - (Vector2)firePoint.position).normalized;

			projectile.GetComponent<Projectile>().SetDirection(direction, gameObject);
		}
		else
		{
			Debug.LogWarning("Projectile prefab or fire point not set.");
		}
	}
}
