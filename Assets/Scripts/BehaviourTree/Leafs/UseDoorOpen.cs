using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UseDoorOpen : Node
{
	NavMeshAgent agent;
	string detectionTarget;

	public UseDoorOpen(NavMeshAgent agent, string detectionTarget) 
	{
		this.agent = agent;
		this.detectionTarget = detectionTarget;
	}

	public override NodeState Evaluate()
	{
		object t = GetData(detectionTarget);
		if (t != null) 
		{
			Transform interactable = (Transform)t;

			// Assuming OpenDoor is a MonoBehaviour attached to the interactable GameObject
			OpenDoor openDoorComponent = interactable.gameObject.GetComponent<OpenDoor>();

			if(openDoorComponent != null && agent.remainingDistance < 0.3f)
			{
				// Call the method from the OpenDoor component
				openDoorComponent.PlayDoorOpenAnimation(); 
				return NodeState.SUCCESS;
			}
		}
		return NodeState.FAILED;
	}
}
