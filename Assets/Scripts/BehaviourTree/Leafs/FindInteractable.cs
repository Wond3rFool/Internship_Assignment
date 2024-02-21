using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FindInteractable: Node
{
	private int interactableLayer = 1 << LayerMask.NameToLayer("Interactable");

	private GameObject gameObject;
	private string detection;

	public FindInteractable(GameObject gameObject, string detection)
	{
		this.gameObject = gameObject;
		this.detection = detection;
	}

	public override NodeState Evaluate()
	{
		Collider2D[] colliders = Physics2D.OverlapCircleAll(
			gameObject.transform.position, float.MaxValue, interactableLayer);

		Transform closestInteractable = null;
		float closestDistance = float.MaxValue;

		foreach(var collider in colliders)
		{
			float distance = Vector3.Distance(gameObject.transform.position, collider.transform.position);

			if(distance < closestDistance)
			{
				closestInteractable = collider.transform;
				closestDistance = distance;
			}
		}

		if(closestInteractable != null)
		{
			parent.parent.parent.SetData(detection, closestInteractable);
			return NodeState.SUCCESS;
		}

		return NodeState.FAILED;
	}
}

