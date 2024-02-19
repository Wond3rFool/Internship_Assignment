using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallelSequence: Node
{
	public ParallelSequence(List<Node> children) : base(children) { }

	public override NodeState Evaluate()
	{



		return NodeState.SUCCESS;
	}
}
