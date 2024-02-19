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



		return NodeState.SUCCESS;
	}
}
