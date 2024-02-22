using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SetDestination : Node
{
	NavMeshAgent agent;
	Animator animator;
	Transform rotation;
	Vector3 destination;
	public SetDestination(NavMeshAgent agent,Animator animator, Transform rotation, Vector3 destination) 
	{
		this.agent = agent;
		this.animator = animator;
		this.rotation = rotation;
		this.destination = destination;
	}

	public override NodeState Evaluate()
	{
		agent.SetDestination(destination);
		animator.SetBool("isMoving", true);
		Vector2 direction = (destination - agent.transform.position).normalized;
		rotation.up = direction;
		if(!agent.pathPending && agent.remainingDistance < 0.1f)
		{
			animator.SetBool("isMoving", false);
			return NodeState.SUCCESS;
		}
		return NodeState.RUNNING;
	}
}
