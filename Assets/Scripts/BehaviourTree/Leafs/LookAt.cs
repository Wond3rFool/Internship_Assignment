using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt: Node
{
	private Transform transform;
	private Transform target;

	public LookAt(Transform transform, Transform target)
	{
		this.transform = transform;
		this.target = target;
	}

	public override NodeState Evaluate()
	{
		Vector2 direction = (target.position - transform.position).normalized;
		transform.up = direction;
		return NodeState.SUCCESS;
	}
}
