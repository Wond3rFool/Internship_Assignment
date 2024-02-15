using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selector : Node
{
	public Selector() : base() { }
	public Selector(List<Node> children) : base(children) { }

	private int currentChildIndex = 0;
	public override NodeState Evaluate()
	{
		while(currentChildIndex < children.Count)
		{
			Node child = children[currentChildIndex];
			NodeState childState = child.Evaluate();

			switch(childState)
			{
				case NodeState.SUCCESS:
					state = NodeState.SUCCESS;
					currentChildIndex = 0;  // Reset to the first child for the next iteration
					return state;
				case NodeState.FAILED:
					currentChildIndex++;  // Move on to the next child
					continue;
				case NodeState.RUNNING:
					state = NodeState.RUNNING;
					return state;
				default:
					// Handle any unexpected states, if necessary
					continue;
			}
		}

		// If all children have been attempted and none succeeded, return FAILURE
		state = NodeState.FAILED;
		currentChildIndex = 0;  // Reset to the first child for the next iteration
		return state;
	}
}
