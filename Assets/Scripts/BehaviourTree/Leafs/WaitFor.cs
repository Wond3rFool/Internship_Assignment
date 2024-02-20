using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitFor: Node
{
	private float elapsedTime;
	private float timeToWait;
	private bool isRunning;

	public WaitFor(float timeToWait)
	{
		this.timeToWait = timeToWait;
	}

	public override NodeState Evaluate()
	{
		elapsedTime += Time.deltaTime;

		if(elapsedTime >= timeToWait)
		{
			elapsedTime = 0f;
			isRunning = false;
			return NodeState.SUCCESS;
		}

		// Timer is still running
		return NodeState.RUNNING;
	}
}
