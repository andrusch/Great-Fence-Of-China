using UnityEngine;
using System;
using System.Collections;

public class Player : Piece {

	public int Health;
	public float xMargin = 1f;		// Distance in the x axis the  can move before the camera follows.
	public float yMargin = 1f;		// Distance in the y axis the focus can move before the camera follows.
	public float xSmooth = 8f;		// How smoothly the camera catches up with it's target movement in the x axis.
	public float ySmooth = 8f;		// How smoothly the camera catches up with it's target movement in the y axis.
	public Vector2 maxXAndY;		// The maximum x and y coordinates the camera can have.
	public Vector2 minXAndY;		// The minimum x and y coordinates the camera can have.
	private bool m_isHoriAxisInUse = false;
	private bool m_isVertAxisInUse = false;
    public float XMovementOffset;
    public float YMovementOffset;
	// Use this for initialization
	void Start () {
		this.Health = 5;
        this.Y = 2;
	}
	
	// Update is called once per frame
	void Update () {
	 
	}
	
	void FixedUpdate() 
	{
        if (Input.GetKeyDown("space"))
        {
            BuildFence();
        }
        else
        {
            // see 
            float h = Input.GetAxisRaw("Horizontal");
            float v = Input.GetAxisRaw("Vertical");
            float tempX = 0;
            float tempY = 0;
            if (v != 0)
            {
                if (!m_isVertAxisInUse)
                {
                    m_isVertAxisInUse = true;
                    tempY += v;
                    tempX += v;

                    if (tempY + this.Y < GameEngine.Instance.BoardHeight && tempY + this.Y > -1)
                    {
                        this.Y += (int)tempY;
                        transform.Translate((float)(tempX * XMovementOffset), (float)(tempY * YMovementOffset), 0);
                    }
                }
            }
            else
                m_isVertAxisInUse = false;
        }
	}
	public void TakeDamage(int damage)
	{
        this.Health -= damage;
	}
    public void BuildFence()
    {

    }
}
