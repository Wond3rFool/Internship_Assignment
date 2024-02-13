using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sequence : Node
{
	public Sequence() : base() { }
	public Sequence(List<Node> children) : base(children) { }

	public override NodeState Evaluate()
	{
		bool anyChildIsRunning = false;

		for(int i = 0; i < children.Count; i++)
		{
			Node node = children[i];
			switch(node.Evaluate())
			{
				case NodeState.FAILED:
					state = NodeState.FAILED;
					return state;
				case NodeState.SUCCESS:
					continue;
				case NodeState.RUNNING:
					anyChildIsRunning = true;
					continue;
				default:
					state = NodeState.FAILED;
					return state;
			}
		}

		state = anyChildIsRunning ? NodeState.RUNNING : NodeState.SUCCESS;
		return state;
	}
}
