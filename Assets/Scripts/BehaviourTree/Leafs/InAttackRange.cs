using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InAttackRange : Node
{
	private int playerLayerMask = 1 << LayerMask.NameToLayer("Player");

	private GameObject gameObject;
	private float attackRadius;
	private string detection;

	public InAttackRange(GameObject gameObject, float attackRadius,string detection )
	{
		this.gameObject = gameObject;
		this.attackRadius = attackRadius;
		this.detection = detection;
	}
	public override NodeState Evaluate()
	{
		object t = parent.parent.GetData(detection);
		if(t == null)
		{
			Collider2D[] colliders = Physics2D.OverlapCircleAll(
				gameObject.transform.position, attackRadius, playerLayerMask);

			if(colliders.Length > 0)
			{
				parent.parent.SetData(detection, colliders[0].transform); // set the data in the parent so all child nodes can find it.
				return NodeState.SUCCESS;
			}
			return NodeState.FAILED;
		}
		else
		{
			Transform targetTransform = (Transform)t;
			float distance = Vector2.Distance(gameObject.transform.position, targetTransform.position);
			if(distance > attackRadius)
			{
				parent.parent.ClearData(detection);
				return NodeState.FAILED;
			}
		}
		return NodeState.SUCCESS;
	}
}
