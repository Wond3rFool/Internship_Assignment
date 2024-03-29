using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UseDoorClose: Node
{
	NavMeshAgent agent;
	string detectionTarget;

	public UseDoorClose(NavMeshAgent agent, string detectionTarget)
	{
		this.agent = agent;
		this.detectionTarget = detectionTarget;
	}

	public override NodeState Evaluate()
	{
		object t = GetData(detectionTarget);
		if(t != null)
		{
			Transform interactable = (Transform)t;

			// Assuming OpenDoor is a MonoBehaviour attached to the interactable GameObject
			OpenDoor openDoorComponent = interactable.gameObject.GetComponentInParent<OpenDoor>();

			if(openDoorComponent != null && agent.remainingDistance < 0.1f)
			{
				// Call the method from the OpenDoor component
				openDoorComponent.PlayDoorCloseAnimation();
				return NodeState.SUCCESS;
			}
		}
		return NodeState.FAILED;
	}
}
