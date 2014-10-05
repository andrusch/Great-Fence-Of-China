using UnityEngine;
using System.Collections;

public class Fence : Piece {
	public int Health;
	public int Shielding;
	public int HealthIncrease;
	public int ClickBonus;
	public int OverlayCount;
	private int Chunk;
	public int MaxHealth;

    Renderer overlay0;
	Renderer overlay1;
	Renderer overlay2;
	Renderer overlay3;

	// Use this for initialization
	void Start () {
		this.Chunk = this.MaxHealth / (OverlayCount + 1);

		// Grab all the renderer components of my children
		SpriteRenderer[] overlays = GetComponentsInChildren<SpriteRenderer>();
        overlay0 = overlays[0];
		overlay1 = overlays[1];
		overlay2 = overlays[2];
		overlay3 = overlays[3];
        overlay1.enabled = false;
        overlay2.enabled = false;
        overlay3.enabled = true;
	}
	
	// Update is called once per frame
	void Update () {
        UpdateImage();
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
            overlay0.enabled = true;
			overlay1.enabled = false;
			overlay2.enabled = false;
			overlay3.enabled = true;
		}
		// Not quite as damaged
		else if (Health <= Chunk * 2)
		{
            overlay0.enabled = true;
			overlay1.enabled = false;
			overlay2.enabled = true;
			overlay3.enabled = false;		
		}
		// Lightly scratched
		else if (Health <= Chunk * 3)
		{
            overlay0.enabled = true;
			overlay1.enabled = true;
			overlay2.enabled = false;
			overlay3.enabled = false;		
		}
		// Undamaged
		else
		{
            overlay0.enabled = true;
			overlay1.enabled = false;
			overlay2.enabled = false;
			overlay3.enabled = false;		
		}
	}
	public void Explode()
	{
		// add animation for the explosion
	}
    public int AddHealth(bool addBonus)
    {
        this.Health += this.HealthIncrease;
        if (addBonus)
        {
            this.Health += this.ClickBonus;
        }
        return this.Health - this.MaxHealth;
    }
}
