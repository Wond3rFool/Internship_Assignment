using NavMeshPlus.Components;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenDoor: MonoBehaviour
{
	[SerializeField]
	NavMeshSurface surface;

	private Animator animator;
	private BoxCollider2D boxCollider;
	private NavMeshModifier modifier;
	// Start is called before the first frame update
	void Awake()
	{
		animator = GetComponent<Animator>();
		boxCollider = GetComponentInChildren<BoxCollider2D>();
		modifier = GetComponentInChildren<NavMeshModifier>();
	}

	public void PlayDoorOpenAnimation() 
	{
		modifier.area = 0;
		animator.SetBool("OpenDoor", true);
 	}

	public void PlayDoorCloseAnimation() 
	{
		modifier.area = 1;
		animator.SetBool("OpenDoor", false);
	}

	public void TurnOffCollider() 
	{
		boxCollider.enabled = false;
		surface.UpdateNavMesh(surface.navMeshData);
	}
	public void TurnOnCollider() 
	{
		boxCollider.enabled = true;
		surface.UpdateNavMesh(surface.navMeshData);
	}
}
