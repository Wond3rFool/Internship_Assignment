using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selector : Node
{
	public Selector() : base() { }
	public Selector(List<Node> children) : base(children) { }

	public override NodeState Evaluate()
	{
		for(int i = 0; i < children.Count; i++)
		{
			Node node = children[i];
			switch(node.Evaluate())
			{
				case NodeState.FAILED:
					continue;
				case NodeState.SUCCESS:
					state = NodeState.SUCCESS;
					return state;
				case NodeState.RUNNING:
					state = NodeState.RUNNING;
					return state;
				default:
					continue;
			}
		}

		state = NodeState.FAILED;
		return state;
	}
}
