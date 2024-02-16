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

	private Vector2 moveDirection;

	private void Start()
	{
		Destroy(gameObject, lifespan);
	}

	private void Update()
	{
		MoveProjectile();
	}

	private void MoveProjectile()
	{
		transform.Translate(moveDirection * speed * Time.deltaTime);
	}

	public void SetDirection(Vector2 direction)
	{
		moveDirection = direction.normalized;
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		Destroy(gameObject);
	}
}
