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

	private GameObject parent;

	private float distance = 100f;

	private void Start()
	{
		Destroy(gameObject, lifespan);
	}

	void Update()
	{
		MoveProjectile();

		// Create a layer mask that excludes the layer of the entity firing the bullet
		int layerMaskWithoutSelf = layerMask.value & ~(1 << parent.layer);

		RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.forward, distance, layerMaskWithoutSelf);
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

	public void SetDirection(Vector2 direction, GameObject parent)
	{
		this.parent = parent;
		moveDirection = direction.normalized;
	}
}
