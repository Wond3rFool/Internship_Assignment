using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SetDestination : Node
{
	NavMeshAgent agent;
	Vector3 destination;
	public SetDestination(NavMeshAgent agent, Vector3 destination) 
	{
		this.agent = agent;
		this.destination = destination;
	}

	public override NodeState Evaluate()
	{
		agent.SetDestination(destination);
		if(!agent.pathPending && agent.remainingDistance < 0.2f)
		{
			return NodeState.SUCCESS;
		}
		return NodeState.RUNNING;
	}
}
