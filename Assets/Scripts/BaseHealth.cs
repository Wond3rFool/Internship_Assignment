using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseHealth: MonoBehaviour
{
	[SerializeField] public int maxHealth;
	public int currentHealth;
	public bool isDead;
	public abstract void TakeDamage(int damage);
	public abstract void Die();
}
