using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SetDestinationInterrupt : Node
{
	NavMeshAgent agent;
	Transform rotation;
	Animator animator;
	string destination;
	public SetDestinationInterrupt(NavMeshAgent agent, Animator animator, Transform rotation, string destination)
	{
		this.agent = agent;
		this.animator = animator;
		this.rotation = rotation;
		this.destination = destination;
	}

	public override NodeState Evaluate()
	{
		object t = GetData(destination);
		Vector3 position = (Vector3)t;
		agent.SetDestination(position);
		animator.SetBool("isMoving", true);
		Vector2 direction = (position - agent.transform.position).normalized;
		rotation.up = direction;
		if(!agent.pathPending && agent.remainingDistance < 3.0f)
		{
			animator.SetBool("isMoving", false);
			return NodeState.SUCCESS;
		}
		return NodeState.RUNNING;
	}
}
