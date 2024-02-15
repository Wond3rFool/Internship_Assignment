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
	
	[SerializeField]
	[Range(10.0f, 1000.0f)]
	private float detectRadius;

	private string enemyID;

	private void Awake()
	{
		enemyID = gameObject.GetInstanceID().ToString();
	}

	protected override Node SetupTree()
	{
		Node root = new Sequence(new List<Node>
		{
			new CheckForPlayer(gameObject, detectRadius, enemyID),
			new Selector(new List<Node>
			{
				
			}),
			new Patrol(transform, speed, patrolRadius)
		});
		return root;
	}
}
