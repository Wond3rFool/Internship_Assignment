using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol: Node
{

	private float waitTime = 1f; // in seconds
	private float waitCounter = 0f;
	private bool waiting = false;

	private Transform transform;
	private Vector3 originPosition;
	private Vector3 nextWaypointPosition;

	private float moveSpeed;
	private float patrolRadius;
	public Patrol(Transform source, float moveSpeed, float patrolRadius)
	{ 
		transform = source;
		originPosition = transform.position;
		nextWaypointPosition = originPosition;
		this.moveSpeed = moveSpeed;
		this.patrolRadius = patrolRadius;
	}

	public override NodeState Evaluate()
	{
		if(waiting)
		{
			waitCounter += Time.deltaTime;
			if(waitCounter > waitTime)
			{
				waiting = false;
				waitTime = Random.Range(0.1f, waitTime + 2);
			}
		}
		else
		{
			if(Vector3.Distance(transform.position, nextWaypointPosition) < 0.3f)
			{
				transform.position = nextWaypointPosition;
				waitCounter = 0f;
				waiting = true;

				Vector2 randomPoint = Random.insideUnitCircle.normalized * patrolRadius;
				nextWaypointPosition = originPosition + new Vector3(randomPoint.x, randomPoint.y, 0);
			}
			else
			{
				Vector3 direction = nextWaypointPosition - transform.position;
				transform.position += direction.normalized * moveSpeed * Time.fixedDeltaTime;
			}
		}
		return NodeState.RUNNING;
	} 
}
