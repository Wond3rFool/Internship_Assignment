using NavMeshPlus.Components;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildNavmesh: MonoBehaviour
{
	private NavMeshSurface surface;

	void Start()
	{
		surface = GetComponent<NavMeshSurface>();	
		surface.BuildNavMeshAsync();
	}

}
