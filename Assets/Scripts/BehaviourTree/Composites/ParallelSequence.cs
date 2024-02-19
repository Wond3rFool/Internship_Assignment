using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallelSequence: Node
{
	public ParallelSequence() : base() { }

	public ParallelSequence(List<Node> children) : base(children) { }

	public override NodeState Evaluate()
	{
		bool anyChildRunning = false;
		for(int i = 0; i < children.Count; i++) 
		{
			switch(children[i].Evaluate())
			{
				case NodeState.FAILED:
					return NodeState.FAILED;
				case NodeState.SUCCESS:
					continue;
				case NodeState.RUNNING:
					anyChildRunning = true;
					continue;
				default:
					return NodeState.SUCCESS;
			}
		}

		NodeState status = anyChildRunning ? NodeState.RUNNING : NodeState.SUCCESS;
		return status;
	}
}
