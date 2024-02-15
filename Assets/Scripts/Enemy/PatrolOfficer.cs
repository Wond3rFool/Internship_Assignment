using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolOfficer: BehaviourTree
{
	public Transform[] waypoints;

	[SerializeField]
	private float speed;

	[SerializeField]
	[Range(1.0f, 15.0f)]
	private float patrolRadius;

	protected override Node SetupTree()
	{
		Node root = new Selector(new List<Node>
		{
			new Patrol(transform, speed, patrolRadius)
	
		});


		return root;
	}
}
