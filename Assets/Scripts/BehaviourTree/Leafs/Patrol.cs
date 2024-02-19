using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol: Node
{
	private float waitTime = 1f; // in seconds
	private float waitCounter = 0f;
	private bool waiting = false;

	private Transform transform;
	private List<Transform> waypoints = new List<Transform>();
	private int currentWaypointIndex = 0;

	private float moveSpeed;

	public Patrol(Transform source, float moveSpeed, Transform[] patrolWaypoints)
	{
		transform = source;
		this.moveSpeed = moveSpeed;

		if(patrolWaypoints != null && patrolWaypoints.Length > 0)
		{
			waypoints.AddRange(patrolWaypoints);
		}
		else
		{
			Debug.LogWarning("No waypoints provided for patrol.");
		}
	}

	public override NodeState Evaluate()
	{
		if(waypoints.Count == 0)
		{
			Debug.LogWarning("No waypoints available for patrol.");
			return NodeState.FAILED;
		}

		if(waiting)
		{
			waitCounter += Time.deltaTime;
			if(waitCounter > waitTime)
			{
				waiting = false;
				waitCounter = 0f;
			}
			return NodeState.SUCCESS;
		}
		else
		{
			Transform currentWaypoint = waypoints[currentWaypointIndex];
			if(Vector3.Distance(transform.position, currentWaypoint.position) < 0.3f)
			{
				waiting = true;
				waitTime = Random.Range(0.1f, waitTime + 2);

				currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Count;

				return NodeState.SUCCESS;
			}
			else
			{
				Vector3 direction = currentWaypoint.position - transform.position;
				transform.position += direction.normalized * moveSpeed * Time.deltaTime;
				return NodeState.SUCCESS;
			}
		}
	}
}

