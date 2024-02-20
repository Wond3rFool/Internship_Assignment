using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class PatrolOfficer: BehaviourTree
{
	public Transform[] waypoints;

	public Transform doorPoint;
	public Transform roomPoint;

	[SerializeField]
	private LayerMask objectLayer;

	[SerializeField]
	private float speed;

	[SerializeField]
	[Range(500f, 1000f)]
	private float findPlayerRad;

	[SerializeField]
	[Range(4.0f, 22.0f)]
	private float detectRadius;

	[SerializeField]
	private Transform weaponTransform;

	[SerializeField]
	private GameObject projectilePrefab;

	[SerializeField]
	[Range(0.1f, 10.0f)]
	private float fireRate;

	private string enemyID;
	private string doorID;
	private string playerID;

	private NavMeshAgent agent;

	private bool playerDetected;

	private void Awake()
	{
		//Get an unique ID so you have a way to store the data of the player unique to this enemy.
		enemyID = gameObject.GetInstanceID().ToString();
		doorID = enemyID + "Doors";
		playerID = enemyID + "player";
		agent = GetComponent<NavMeshAgent>();
		agent.updateRotation = false;
		agent.updateUpAxis = false;
		agent.speed = speed;
	}

	protected override Node SetupTree()
	{
		Node root = new Selector(new List<Node>
		{
			new Inverter(new FindPlayer(gameObject, findPlayerRad, enemyID)),

			new Sequence(new List<Node>
			{
				new DetectPlayer(gameObject, detectRadius, enemyID),
				new SetLKPosition(this, enemyID, playerID),
				new FindInteractable(gameObject, doorID),
				new WalkToClosest(agent, doorID),
				new UseDoorOpen(agent, doorID),
				new WaitFor(1.2f),
				new SetDestination(agent, doorPoint.position),
				new WaitFor(0.5f),
				new Sequence(new List<Node>
				{
					new ParallelSequence(new List<Node>
					{
						new SetDestinationInterrupt(agent, playerID),
						new Inverter(new HasLOS(transform, enemyID, objectLayer))
					}),
					new ParallelSequence(new List<Node>
					{
						 new WaitFor(20.0f),
						 new Log("Debugging")
 					}),
					new SetDestination(agent, roomPoint.position),
					new UseDoorClose(agent, doorID),
					new Inverter(new WaitFor(0.2f))
				}),
			}),
			new Sequence(new List<Node>{
				new HasLOS(transform, enemyID, objectLayer),
				new SetLKPosition(this, enemyID, playerID),
				new ParallelSequence(new List<Node>
				{
					new SetDestinationInterrupt(agent, playerID),
					new HasLOS(transform, enemyID, objectLayer),
					new AimAt(weaponTransform, enemyID),
					new WaitFor(fireRate)
				}),
				new Shoot(transform, weaponTransform, projectilePrefab, enemyID)
			}),
			new Patrol(agent, waypoints)
		});
		return root;
	}

	private void SetPlayerDetection(bool isDetected) 
	{
		playerDetected = isDetected;
	}

	private bool PlayerDetected() 
	{
		return playerDetected;
	}
}
