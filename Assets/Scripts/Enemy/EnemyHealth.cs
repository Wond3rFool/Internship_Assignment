using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth: BaseHealth
{
	public int CurrentHealth
	{
		get { return currentHealth; }
		set { currentHealth = Mathf.Clamp(value, 0, maxHealth); }
	}
	public bool IsDead { get { return CurrentHealth <= 0; } }

	private void Start()
	{
		ResetHealth();
	}
	public override void TakeDamage(int damage)
	{
		CurrentHealth -= damage;

		if(IsDead)
		{
			Die();
		}
	}

	public void TakeDamage(int damage, Transform origin)
	{
		TakeDamage(damage);
	}

	public void ResetHealth()
	{
		CurrentHealth = maxHealth;
	}
	public override void Die()
	{
		Debug.Log("dead");
		Destroy(gameObject.transform.parent.gameObject);
	}
}