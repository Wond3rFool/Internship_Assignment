using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Patrol: Node
{
	private float waitTime = 1f; // in seconds
	private float waitCounter = 0f;
	private bool waiting = false;

	private NavMeshAgent agent;
	private List<Transform> waypoints = new List<Transform>();
	private int currentWaypointIndex = 0;

	public Patrol(NavMeshAgent agent, Transform[] patrolWaypoints)
	{
		this.agent = agent;

		if(agent == null)
		{
			Debug.LogWarning("NavMeshAgent not provided for patrol.");
		}

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
		if(agent == null)
		{
			Debug.LogWarning("NavMeshAgent not provided for patrol.");
			return NodeState.FAILED;
		}

		if(waypoints.Count == 0)
		{
			Debug.LogWarning("No waypoints available for patrol.");
			return NodeState.FAILED;
		}
		Transform currentWaypoint = waypoints[currentWaypointIndex];
		agent.SetDestination(currentWaypoint.position);
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
			
			if(!agent.pathPending && agent.remainingDistance < 0.3f)
			{
				waiting = true;
				waitTime = Random.Range(1f, waitTime + 3);

				currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Count;

				return NodeState.SUCCESS;
			}
			else
			{
				// Set the destination for the NavMeshAgent
				agent.SetDestination(currentWaypoint.position);
				return NodeState.SUCCESS;
			}
		}
	}
}

