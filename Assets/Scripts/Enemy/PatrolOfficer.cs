using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolOfficer: BehaviourTree
{
	public Transform[] waypoints;

	[SerializeField]
	private LayerMask objectLayer;

	[SerializeField]
	private float speed;

	[SerializeField]
	[Range(4.0f, 15.0f)]
	private float patrolRadius;

	[SerializeField]
	[Range(4.0f, 50.0f)]
	private float detectRadius;

	[SerializeField]
	private Transform weaponTransform;

	[SerializeField]
	private GameObject projectilePrefab;

	[SerializeField]
	[Range(0.1f, 10.0f)]
	private float fireRate;

	private string enemyID;

	private NavMeshAgent agent;

	private void Awake()
	{
		//Get an unique ID so you have a way to store the data of the player unique to this enemy.
		enemyID = gameObject.GetInstanceID().ToString();
		agent = GetComponent<NavMeshAgent>();
		agent.updateRotation = false;
		agent.updateUpAxis = false;
		agent.speed = speed;
	}

	protected override Node SetupTree()
	{
		Node root = new Selector(new List<Node>
		{
			new Sequence(new List<Node>
			{
				new CheckRadiusFor(gameObject, patrolRadius, enemyID),
				new FindInteractable(gameObject, detectRadius, enemyID+"Doors"),
				//Walk to door
				//open door
				new Inverter(new Log("seen player"))
			}),
			new Sequence(new List<Node>{
				new HasLOS(transform, enemyID, objectLayer),
				new ParallelSequence(new List<Node>
				{
					new AimAt(weaponTransform, enemyID),
					new WaitFor(fireRate)
				}),
				new Shoot(transform, weaponTransform, projectilePrefab, enemyID)

			}),
			new Patrol(agent, waypoints)
		});
		return root;
	}
}
