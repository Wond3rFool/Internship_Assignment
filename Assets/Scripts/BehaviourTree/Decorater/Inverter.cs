using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inverter: Node
{
	private Node child;

	public Inverter(Node child)
	{
		this.child = child;
		child.parent = this;
	}

	public override NodeState Evaluate()
	{
		switch(child.Evaluate())
		{
			case NodeState.SUCCESS:
				state = NodeState.FAILED;
				return state;
			case NodeState.FAILED:
				state = NodeState.SUCCESS;
				return state;
			case NodeState.RUNNING:
				state = NodeState.RUNNING;
				return state;
			default:
				// Handle any unexpected states, if necessary
				state = NodeState.FAILED;
				return state;
		}
	}
}
