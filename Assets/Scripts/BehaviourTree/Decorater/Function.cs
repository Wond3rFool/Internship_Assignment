using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Function: Node
{
	private Action action;

	public Function(Action action)
	{
		this.action = action;
	}

	public override NodeState Evaluate()
	{
		action();
		return NodeState.SUCCESS;
	}
}
