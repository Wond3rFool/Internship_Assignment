using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SetDestinationInterrupt : Node
{
	NavMeshAgent agent;
	string destination;
	public SetDestinationInterrupt(NavMeshAgent agent, string destination)
	{
		this.agent = agent;
		this.destination = destination;
	}

	public override NodeState Evaluate()
	{
		object t = GetData(destination);
		Vector3 position = (Vector3)t;
		agent.SetDestination(position);
		if(!agent.pathPending && agent.remainingDistance < 3.0f)
		{
			return NodeState.SUCCESS;
		}
		return NodeState.RUNNING;
	}
}
