using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BehaviourTree : MonoBehaviour
{
	private Node root = null;

	protected void Start()
	{
		root = SetupTree();
	}
	protected abstract Node SetupTree();

	protected virtual void Update()
	{
		if(root != null) { root.Evaluate(); }
	}
}
