using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
		object t = GetData(detection);
		if(t == null)
		{
			Collider2D[] colliders = Physics2D.OverlapCircleAll(
				gameObject.transform.position, detectRadius, interactableLayer);

			if(colliders.Length > 0)
			{
				SetData(detection, colliders[0].transform);
				return NodeState.SUCCESS;
			}
			return NodeState.FAILED;
		}
		return NodeState.SUCCESS;
	}
}
