using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolOfficer : BehaviourTree
{
	public Transform[] waypoints;

	[SerializeField]
	private float speed;

	protected override Node SetupTree()
	{
		throw new System.NotImplementedException();
	}
}
