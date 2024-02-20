using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindPlayer : Node
{
	private int playerLayerMask = 1 << LayerMask.NameToLayer("Player");

	private GameObject gameObject;
	private float checkRadius;
	private string detectionTarget;

	public FindPlayer(GameObject gameObject, float checkRadius, string detectionTarget)
	{
		this.gameObject = gameObject;
		this.checkRadius = checkRadius;
		this.detectionTarget = detectionTarget;
	}
	public override NodeState Evaluate()
	{
		object t = GetData(detectionTarget);
		if(t == null)
		{
			Collider2D[] colliders = Physics2D.OverlapCircleAll(
				gameObject.transform.position, checkRadius, playerLayerMask);

			if(colliders.Length > 0)
			{
				parent.parent.SetData(detectionTarget, colliders[0].transform); // set the data in the parent so all child nodes can find it.
				return NodeState.SUCCESS;
			}
			return NodeState.FAILED;
		}
		return NodeState.SUCCESS;
	}
}
