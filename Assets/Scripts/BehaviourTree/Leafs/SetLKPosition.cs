using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetLKPosition : Node
{
	string detectionTarget;
	string savePosition;
	PatrolOfficer gameObject;
	public SetLKPosition(PatrolOfficer gameObject, string detectionTarget, string savePosition) 
	{
		this.gameObject = gameObject;
		this.detectionTarget = detectionTarget;
		this.savePosition = savePosition;
	}

	public override NodeState Evaluate()
	{
		object t = GetData(detectionTarget);

		if (t != null) 
		{
			Transform transform = (Transform)t;
			parent.parent.SetData(savePosition, transform.position);
			return NodeState.SUCCESS;
		}
		return NodeState.FAILED;
	}
}
