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

	Renderer overlay1;
	Renderer overlay2;
	Renderer overlay3;

	// Use this for initialization
	void Start () {
		this.Health = 0;
		this.Shielding = 0;
		this.HealthIncrease = 1;
		this.ClickBonus = 1;
		this.OverlayCount = 3;
		this.Chunk = this.Health / (OverlayCount + 1);
		this.MaxHealth = 5;

		// Grab all the renderer components of my children
		SpriteRenderer[] overlays = GetComponentsInChildren<SpriteRenderer>();
		overlay1 = overlays[1];
		overlay2 = overlays[2];
		overlay3 = overlays[3];
        overlay1.enabled = false;
        overlay2.enabled = false;
        overlay3.enabled = false;
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
		// Most damaged state
		if (Health <= Chunk)
		{
			overlay1.enabled = false;
			overlay2.enabled = false;
			overlay3.enabled = true;
		}
		// Not quite as damaged
		else if (Health <= Chunk * 2)
		{
			overlay1.enabled = false;
			overlay2.enabled = true;
			overlay3.enabled = false;		
		}
		// Lightly scratched
		else if (Health <= Chunk * 3)
		{
			overlay1.enabled = true;
			overlay2.enabled = false;
			overlay3.enabled = false;		
		}
		// Undamaged
		else
		{
			overlay1.enabled = false;
			overlay2.enabled = false;
			overlay3.enabled = false;		
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
