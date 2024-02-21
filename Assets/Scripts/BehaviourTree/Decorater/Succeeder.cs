using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Succeeder: Node
{
	public Succeeder(Node childNode)
	{
		children.Add(childNode);
		children[0].parent = this;
	}

	public override NodeState Evaluate()
	{
		switch(children[0].Evaluate())
		{
			case NodeState.RUNNING:
				state = NodeState.RUNNING;
				return state;
			case NodeState.SUCCESS:
				state = NodeState.SUCCESS;
				return state;
			case NodeState.FAILED:
				state = NodeState.SUCCESS;
				return state;
			default:
				// Handle any unexpected states, if necessary
				state = NodeState.SUCCESS;
				return state;
		}
	}
}

