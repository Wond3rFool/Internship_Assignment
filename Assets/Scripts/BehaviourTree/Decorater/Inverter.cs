using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inverter: Node
{
	public Inverter(Node child)
	{
		children.Add(child);
		children[0].parent = this;
	}

	public override NodeState Evaluate()
	{
		switch(children[0].Evaluate())
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
