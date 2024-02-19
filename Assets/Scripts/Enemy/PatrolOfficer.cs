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

	[SerializeField]
	[Range(1.0f, 7.0f)]
	private float attackRadius;

	[SerializeField]
	private Transform weaponTransform;

	[SerializeField]
	private GameObject projectilePrefab;

	[SerializeField]
	[Range(0.1f, 10.0f)]
	private float fireRate;



	private string enemyID;

	private void Awake()
	{
		//Get an unique ID so you have a way to store the data of the player unique to this enemy.
		enemyID = gameObject.GetInstanceID().ToString();
	}

	protected override Node SetupTree()
	{
		Node root = new Selector(new List<Node>
		{
			new Sequence(new List<Node>
			{
				new InAttackRange(gameObject, attackRadius, enemyID),
				new ParallelSequence(new List<Node>
				{
					new AimAt(weaponTransform, enemyID),
					new WaitFor(fireRate)
				}),
				new Shoot(transform, weaponTransform, projectilePrefab, enemyID)

			}),
			new Patrol(transform, speed, patrolRadius)
		});
		return root;
	}
}
