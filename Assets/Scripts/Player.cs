using UnityEngine;
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
		float tempX = this.X;
		float tempY = this.Y;
		if( h != 0.0f)
		{
			if(m_isHoriAxisInUse == false)
			{
				if  (((tempX+h) < maxXAndY.x) && ((tempX+h) > minXAndY.x))
					tempX += h;
				m_isHoriAxisInUse = true;
			}
		}
		if( h == 0.0f)
		{
			m_isHoriAxisInUse = false;
		} 
		if( v != 0.0f)
		{
			if(m_isVertAxisInUse == false)
			{
				if  (((tempY+v) < maxXAndY.y) && ((tempY+v) > minXAndY.y))
					tempY += v;
				m_isVertAxisInUse = true;
			}
		}
		if( v == 0.0f)
		{
			m_isVertAxisInUse = false;
		} 
		Vector2 tempVector = new Vector2(tempX,tempY);
		//Debug.LogError("testing...");
		
		if (tempX != this.X || tempY != this.Y)
		{
			if (this.TryMove((int)tempX, (int)tempY))
			{
				gameObject.transform.position = tempVector;
			}
		}
	}
	
	public bool TryMove(int newX, int newY)
	{
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
