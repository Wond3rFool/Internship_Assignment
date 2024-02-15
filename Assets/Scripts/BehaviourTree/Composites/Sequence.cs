using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sequence: Node
{
	SequenceState storedState;
	public Sequence(List<Node> children) : base(children) { }

	public override NodeState Evaluate()
	{
		// If there is no stored state, start from the beginning
		if(storedState == null)
		{
			int currentChildIndex = 0;
			storedState = new SequenceState(currentChildIndex);
		}

		// Continue evaluating child nodes from the stored state
		while(storedState.currentChildIndex < children.Count)
		{
			NodeState childStatus = children[storedState.currentChildIndex].Evaluate();

			// If the child is still running, return running and store the current state
			if(childStatus == NodeState.RUNNING)
			{
				return NodeState.RUNNING;
			}
			// If the child fails, return failure and reset the stored state
			else if(childStatus == NodeState.FAILED)
			{
				storedState.Reset();
				return NodeState.FAILED;
			}

			// Move to the next child
			storedState.currentChildIndex++;
		}

		storedState.Reset();
		return NodeState.SUCCESS;
	}
}
public class SequenceState
{
	public int currentChildIndex;

	public SequenceState(int currentChildIndex)
	{
		this.currentChildIndex = currentChildIndex;
	}

	public void Reset()
	{
		currentChildIndex = 0;
	}
}

