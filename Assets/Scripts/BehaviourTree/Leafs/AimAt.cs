using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AimAt : Node
{
	private Transform transform;
	private string target;

	public AimAt(Transform transform, string target) 
	{
		this.transform = transform;
		this.target = target;
	}

	public override NodeState Evaluate()
	{
		object t = GetData(target);
		if(t != null)
		{
			Transform targetTransform = (Transform)t;

			Vector2 direction = (targetTransform.position - transform.position).normalized;
			transform.up = direction;
			return NodeState.SUCCESS;
		}
		return NodeState.FAILED;
	}
}
