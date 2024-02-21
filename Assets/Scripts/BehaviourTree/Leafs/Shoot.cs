using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : Node
{
	private Transform transform;
	private Transform firePoint;
	private GameObject projectilePrefab;
	private string target;

	public Shoot(Transform transform, Transform firePoint, GameObject projectilePrefab, string target) 
	{
		this.transform = transform;
		this.firePoint = firePoint;
		this.projectilePrefab = projectilePrefab;
		this.target = target;
	}

	public override NodeState Evaluate()
	{
		object t = GetData(target);
		if(t != null)
		{
			Transform targetTransform = (Transform)t;
			Vector2 targetDirection = (targetTransform.position - transform.position).normalized;

			if(projectilePrefab != null && firePoint != null)
			{
				GameObject projectile = Object.Instantiate(projectilePrefab, firePoint.GetChild(0).position, Quaternion.identity);

				projectile.GetComponent<Projectile>().SetDirection(targetDirection);
			}
			else
			{
				Debug.LogWarning("Projectile prefab or fire point not set.");
			}
			return NodeState.SUCCESS;
		}
		return NodeState.FAILED;
	}
}
