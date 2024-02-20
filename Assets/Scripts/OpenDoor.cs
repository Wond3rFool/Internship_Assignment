using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor: MonoBehaviour
{
	private Animator animator;
	private Material material;
	private BoxCollider2D boxCollider;
	// Start is called before the first frame update
	void Awake()
	{
		animator = GetComponent<Animator>();
		material = GetComponent<Material>();	
		boxCollider = GetComponent<BoxCollider2D>();
	}

	public void PlayDoorOpenAnimation() 
	{
		animator.SetBool("OpenDoor", true);
		Color materialAlpha = material.color;
		materialAlpha.a = 0f;
		material.color = materialAlpha;
 	}

	public void PlayDoorCloseAnimation() 
	{
		animator.SetBool("OpenDoor", false);
		Color materialAlpha = material.color;
		materialAlpha.a = 1f;
		material.color = materialAlpha;
	}

	public void TurnOffCollider() 
	{
		boxCollider.enabled = false;
	}
	public void TurnOnCollider() 
	{
		boxCollider.enabled = true;	
	}
}
