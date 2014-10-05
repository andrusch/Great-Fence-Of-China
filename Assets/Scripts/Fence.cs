using UnityEngine;
using System.Collections;

public class Fence : Piece {
	public int Health;
	public int Shielding;
	public int HealthIncrease;
	public int ClickBonus;
	public int OverlayCount;
	public int Chunk;
	public int MaxHealth;
	// Use this for initialization
	void Start () {
		this.Health = 0;
		this.Shielding = 0;
		this.HealthIncrease = 1;
		this.ClickBonus = 1;
		this.OverlayCount = 3;
		this.Chunk = this.Health / (OverlayCount + 1);
		this.MaxHealth = 100;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public int TakeDamage(int damage)
	{
        damage -= Shielding;
        int damageLeft = 0;
        if (damage > 0)
        {
            this.Health -= damage;
            if (this.Health < 0)
            {
                damageLeft = this.Health * -1;
            }
        }
        return damageLeft;
	}
	public void UpdateImage()
	{
		if (Health <= MaxHealth * 1)
		{
			//set base fence image
		}
		else if (Health <= MaxHealth * 2)
		{
			//set broken1 fence image
		}
		else if (Health <= MaxHealth * 3)
		{
			//set broken2 fence image
		}
		else if (Health <= MaxHealth * 4)
		{
			//set broken3 fence image
		}
	}
	public void Explode()
	{
		// add animation for the explosion
	}
	public void AddHealth(bool addBonus)
	{
				this.Health += this.HealthIncrease;
				if (addBonus){
						this.Health += this.ClickBonus;

		}
		UpdateImage ();
	}

}
