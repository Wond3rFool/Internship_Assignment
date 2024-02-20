using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class DetectPlayer : Node
{
	private int playerLayerMask = 1 << LayerMask.NameToLayer("Player");

	private GameObject gameObject;
	private float detectRadius;
	private string detectionTarget;

	public DetectPlayer(GameObject gameObject, float detectRadius, string detectionTarget) 
	{
		this.gameObject = gameObject;
		this.detectRadius = detectRadius;
		this.detectionTarget = detectionTarget;
	}

	public override NodeState Evaluate()
	{
		object t = GetData(detectionTarget);
		if(t != null) 
		{
			if(PlayerMovement.isMoving) 
			{
				Collider2D[] colliders = Physics2D.OverlapCircleAll(
				gameObject.transform.position, detectRadius, playerLayerMask);

				if(colliders.Length > 0)
				{
					return NodeState.SUCCESS;
				}
				return NodeState.FAILED;
			}
			return NodeState.FAILED;
		}
		return NodeState.FAILED;
	}
}
