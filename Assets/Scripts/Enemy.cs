using UnityEngine;
using System.Collections;
using System.Threading;

public class Enemy : Piece {
	public int Power;
	public int Speed;
	public bool DoesStun;
	// Use this for initialization
	void Start () {
		this.Power = 1;
		this.DoesStun = false;
		this.Speed = 1000;
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	public void Move()
	{
		this.X++;
		this.Move (X,Y);
	}

	public void Explode()
	{

	}

	

}
