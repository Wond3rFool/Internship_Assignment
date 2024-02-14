using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : Node
{
	private int currentWayPointIndex = 0;

	private float waitTime = 1f; // in seconds
	private float waitCounter = 0f;
	private bool waiting = false;

	public override NodeState Evaluate()
	{
		if(waiting)
		{
			waitCounter += Time.deltaTime;
			if(waitCounter >= waitTime)
			{
				waiting = false;
			}
			return NodeState.RUNNING;
		}
		return NodeState.FAILED;                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                
	}

}
