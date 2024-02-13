using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerActions : MonoBehaviour
{
	private bool isSneaking;
	private void OnShoot(InputValue value) 
	{
		
	}

	private void OnSneak(InputValue value) 
	{
		isSneaking = value.isPressed;
	}
	private void Awake()
	{
		isSneaking = false;
	}
	private void Update()
    {
        
    }
}
