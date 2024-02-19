using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HasLOS: Node
{
	private Transform transform;
	private string enemyID;
	private LayerMask obstacleLayer; // Set this in the inspector to include obstacles

	public HasLOS(Transform transform, string enemyID, LayerMask obstacleLayer)
	{
		this.transform = transform;
		this.enemyID = enemyID;
		this.obstacleLayer = obstacleLayer;
	}

	public override NodeState Evaluate()
	{
		object t = GetData(enemyID);
		Transform targetTransform = (Transform)t;

		if(t != null)
		{
			// Calculate direction to the player
			Vector2 direction = targetTransform.position - transform.position;

			// Cast a ray from the enemy towards the player
			RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, Mathf.Infinity, obstacleLayer);

			// Check if the player is in line of sight (no obstacles hit)
			if(hit.collider != null && hit.collider.CompareTag("Player"))
			{
				// Player is in line of sight
				return NodeState.SUCCESS;
			}
			else
			{
				return NodeState.FAILED;
			}
		}
		return NodeState.FAILED;
	}
}