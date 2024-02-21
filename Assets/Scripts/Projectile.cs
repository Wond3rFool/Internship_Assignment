using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Projectile: MonoBehaviour
{
	[SerializeField]
	[Range(10.0f, 2000.0f)]
	private float speed;

	[SerializeField]
	[Range(1.0f, 10.0f)]
	private float lifespan;

	[SerializeField]
	private LayerMask layerMask;

	private Vector2 moveDirection;

	private float distance = 100f;

	private void Start()
	{
		Destroy(gameObject, lifespan);
	}

	void Update()
	{
		MoveProjectile();
		RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.forward, distance, layerMask);

		// Check for hits and apply damage
		if(hit.collider != null)
		{
			BaseHealth baseHealth = hit.collider.GetComponent<BaseHealth>();
			if(baseHealth != null)
			{
				baseHealth.TakeDamage(50);
			}
			Destroy(gameObject);
		}
	}

	private void MoveProjectile()
	{
		transform.Translate(moveDirection * speed * Time.deltaTime);
	}

	public void SetDirection(Vector2 direction)
	{
		moveDirection = direction.normalized;
	}
}
