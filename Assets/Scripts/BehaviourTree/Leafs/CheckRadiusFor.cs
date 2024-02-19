using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckRadiusFor : Node
{
	private int playerLayerMask = 1 << LayerMask.NameToLayer("Player");

	private GameObject gameObject;
	private float checkRadius;
	private string decectionTarget;

	public CheckRadiusFor(GameObject gameObject, float checkRadius, string detectionTarget)
	{
		this.gameObject = gameObject;
		this.checkRadius = checkRadius;
		this.decectionTarget = detectionTarget;
	}
	public override NodeState Evaluate()
	{
		object t = parent.parent.GetData(decectionTarget);
		if(t == null)
		{
			Collider2D[] colliders = Physics2D.OverlapCircleAll(
				gameObject.transform.position, checkRadius, playerLayerMask);

			if(colliders.Length > 0)
			{
				parent.parent.SetData(decectionTarget, colliders[0].transform); // set the data in the parent so all child nodes can find it.
				return NodeState.SUCCESS;
			}
			return NodeState.FAILED;
		}
		else
		{
			Transform targetTransform = (Transform)t;
			float distance = Vector2.Distance(gameObject.transform.position, targetTransform.position);
			if(distance > checkRadius)
			{
				parent.parent.ClearData(decectionTarget);
				return NodeState.FAILED;
			}
		}
		return NodeState.SUCCESS;
	}
}
