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

			// Linecast to check for obstacles
			RaycastHit2D[] hits = Physics2D.LinecastAll(transform.position, targetTransform.position, obstacleLayer);

			// Check if any obstacle is hit
			for(int i = 0; i < hits.Length; i++)
			{
				if(hits[i].collider.CompareTag("Obstacle") || hits[i].collider.CompareTag("Interactable"))
				{
					// Obstacle in the way
					return NodeState.FAILED;
				}
			}
			// No obstacles, player is in line of sight
			return NodeState.SUCCESS;
		}

		return NodeState.FAILED;
	}
}