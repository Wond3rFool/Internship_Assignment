using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SetCurrentPosition: Node
{
	NavMeshAgent agent;
	GameObject gameObject;
	public SetCurrentPosition(NavMeshAgent agent, GameObject gameObject)
	{
		this.agent = agent;
		this.gameObject = gameObject;
	}

	public override NodeState Evaluate()
	{
		Vector3 position = gameObject.transform.position;
		agent.SetDestination(position);
		return NodeState.SUCCESS;
	}
}
