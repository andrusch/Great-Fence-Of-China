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

	}
	
	// Update is called once per frame
	void Update () {
	 
	}
	
	void FixedUpdate() 
	{
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        float tempX = 0;
        float tempY = 0;
        if (v != 0)
        {
            tempY += v;
            tempX += v;
        }
        if (tempX != this.X || tempY != this.Y)
        {
            if (this.TryMove((int)tempX, (int)tempY))
            {
                transform.Translate((float)(tempX * XMovementOffset), (float)(tempY * YMovementOffset), 0);
            }
        }

		
	}
	
	public bool TryMove(int newX, int newY)
	{
		this.X = newX;
		this.Y = newY;
			return true;
	}
	
	public void Move(int newStartX, int newStartY)
	{
		//GameEngine.Instance.MoveObject (this, newStartX, newStartY);
	}
	public void TakeDamage()
	{

	}

}
