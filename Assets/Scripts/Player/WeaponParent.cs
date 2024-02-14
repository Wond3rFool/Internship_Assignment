using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponParent: MonoBehaviour
{
	public Vector2 PointerPostion { get; set;}

	private void Update()
	{
		Vector2 direction = (PointerPostion - (Vector2)transform.position).normalized;
		transform.right = direction;

		Vector2 scale = transform.localScale;
		if(direction.x < 0) 
		{
			scale.y = -1;
		}
		else if(direction.x > 0)
		{
			scale.y = 1;
		}
		transform.localScale = scale;
	}
}

