using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Log: Node
{
	private string message;

	public Log(string message)
	{
		this.message = message;
	}

	public override NodeState Evaluate()
	{
		Debug.Log(message);
		return NodeState.SUCCESS;
	}
}
