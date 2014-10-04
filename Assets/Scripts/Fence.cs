using UnityEngine;
using System.Collections;

public class Fence : MonoBehaviour {
	public int Health;
	public int Shielding;
	public int x;
	public int y;
	public int HealthIncrease;

	// Use this for initialization
	void Start () {
		this.Health = 10;
		this.Shielding = 0;
		this.HealthIncrease = 1;

	
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void TakeDamage(int damage)
	{
		if (damage > Shielding) {

			this.Health -= damage;
		} 

	}
	public void Explode()
	{
	}
	public void AddHealth()
	{
		this.Health += this.HealthIncrease;
	}
}
