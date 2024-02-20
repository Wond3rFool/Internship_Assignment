using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WalkToClosest : Node
{
	private NavMeshAgent agent;
	private string detectionTarget;
	public WalkToClosest(NavMeshAgent agent, string detectionTarget) 
	{
		this.agent = agent;
		this.detectionTarget = detectionTarget;
	}

	public override NodeState Evaluate()
	{
		object t = GetData(detectionTarget);
		if(t != null) 
		{
			Transform transform = (Transform)t;

			agent.SetDestination(transform.position);
			if(!agent.pathPending && agent.remainingDistance < 0.1f)
			{
				return NodeState.SUCCESS;
			}
			return NodeState.RUNNING;
		}
		return NodeState.FAILED;
	}
}
