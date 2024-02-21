using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Condition: Node
{
	private Func<bool> condition;

	public Condition(Func<bool> condition)
	{
		this.condition = condition;
	}

	public override NodeState Evaluate()
	{
		return condition() ? NodeState.SUCCESS : NodeState.FAILED;
	}
}
