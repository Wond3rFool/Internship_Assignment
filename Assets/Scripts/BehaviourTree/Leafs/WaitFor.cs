using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitFor : Node
{
	private float timer;
	private float timeToWait;
	public WaitFor(float timeToWait) 
	{
		this.timeToWait = timeToWait;
	}


	public override NodeState Evaluate()
	{
		if(Time.time > timer)
		{
			timer = Time.time + timeToWait;
			return NodeState.SUCCESS;
		}

		return NodeState.RUNNING;
	}

}
