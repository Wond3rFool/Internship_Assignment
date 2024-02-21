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

	private bool foundDoor;
	private bool doorIsOpen;

	private NavMeshAgent agent;

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
		foundDoor = false;
		doorIsOpen = false;
	}

	protected override Node SetupTree()
	{
		Node root = new Selector(new List<Node>
		{
			new Inverter(new FindPlayer(gameObject, findPlayerRad, enemyID)),

			new Sequence(new List<Node>
			{
				new Inverter(new HasLOS(transform, enemyID, objectLayer)),
				new DetectPlayer(gameObject, detectRadius, enemyID),
				new SetLKPosition(this, enemyID, playerID),
				new Succeeder(new Sequence(new List<Node>
				{
					new Inverter(new Condition(() => foundDoor)),
					new FindInteractable(gameObject, doorID),
					new Function(() => foundDoor = true),
				})),

				new WalkToClosest(agent, doorID),
				new Succeeder(new Sequence(new List<Node>
				{
					new Inverter(new Condition(() => doorIsOpen)),
					new UseDoorOpen(agent, doorID),
					new Function(() => doorIsOpen = true),

				})),
				new SetDestination(agent, doorPoint.position),
				new WaitFor(1.0f),
				new Sequence(new List<Node>
				{
					new SetDestinationInterrupt(agent, playerID),
					new ParallelSequence(new List<Node>
					{
						new WaitFor(2.5f),
						new Inverter(new HasLOS(transform, enemyID, objectLayer)),
						new Log("log")
					}),
					new WaitFor(1.5f),
					new ParallelSequence(new List<Node>
					{
						new SetDestination(agent, roomPoint.position),
						new Inverter(new HasLOS(transform, enemyID, objectLayer))
					}),
					new UseDoorClose(agent, doorID),
					new Function(() => doorIsOpen = false),
					new Inverter(new WaitFor(0.1f))
				}),

			}),
			new Sequence(new List<Node>{
				new HasLOS(transform, enemyID, objectLayer),
				new SetLKPosition(this, enemyID, playerID),
				new ParallelSequence(new List<Node>
				{
					new AimAt(weaponTransform, enemyID),
					new SetDestinationInterrupt(agent, playerID),
					new WaitFor(fireRate)
				}),
				new Shoot(transform, weaponTransform, projectilePrefab, enemyID)
			}),

			new Patrol(agent, waypoints)
		});
		return root;
	}
}
