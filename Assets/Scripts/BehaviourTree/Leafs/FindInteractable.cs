using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FindInteractable: Node
{
	private int interactableLayer = 1 << LayerMask.NameToLayer("Interactable");

	private GameObject gameObject;
	private float detectRadius;
	private string detection;

	public FindInteractable(GameObject gameObject, float detectRadius , string detection)
	{
		this.gameObject = gameObject;
		this.detectRadius = detectRadius;
		this.detection = detection;
	}

	public override NodeState Evaluate()
	{
		Collider2D[] colliders = Physics2D.OverlapCircleAll(
			gameObject.transform.position, detectRadius, interactableLayer);

		Transform closestInteractable = null;

		foreach(var collider in colliders)
		{
			if(CanReachDestination(collider.transform.position)) 
			{
				closestInteractable = collider.transform;
			}
		}

		if(closestInteractable != null)
		{
			parent.parent.SetData(detection, closestInteractable);
			return NodeState.SUCCESS;
		}

		return NodeState.FAILED;
	}
	private bool CanReachDestination(Vector3 destination)
	{
		NavMeshPath path = new NavMeshPath();

		// Calculate the path to the destination
		bool pathIsValid = gameObject.GetComponent<NavMeshAgent>().CalculatePath(destination, path);

		return pathIsValid;
	}
}

