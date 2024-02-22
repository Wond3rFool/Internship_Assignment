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
	private Animator animator;
	private Transform rotation;
	private List<Transform> waypoints = new List<Transform>();
	private int currentWaypointIndex = 0;

	public Patrol(NavMeshAgent agent, Animator animator, Transform rotation, Transform[] patrolWaypoints)
	{
		this.agent = agent;
		this.animator = animator;
		this.rotation = rotation;

		if(patrolWaypoints != null && patrolWaypoints.Length > 0)
		{
			waypoints.AddRange(patrolWaypoints);
		}
	}

	public override NodeState Evaluate()
	{
		Transform currentWaypoint = waypoints[currentWaypointIndex];
		agent.SetDestination(currentWaypoint.position);
		animator.SetBool("isMoving", true);
		Vector2 direction = (currentWaypoint.position - agent.transform.position).normalized;
		rotation.up = direction;
		if(waiting)
		{
			animator.SetBool("isMoving", false);
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
				animator.SetBool("isMoving", true);
				direction = (currentWaypoint.position - agent.transform.position).normalized;
				rotation.up = direction;
				return NodeState.SUCCESS;
			}
		}
	}
}

