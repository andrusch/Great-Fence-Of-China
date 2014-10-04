using UnityEngine;
using System.Collections;
using System.Threading;
using System;

public class Enemy : Piece {
	public int Power;
	public int Speed;
	public bool DoesStun;
    private DateTime? _start;
	// Use this for initialization
	void Start () {
		this.Power = 1;
		this.DoesStun = false;
		this.Speed = 10000;
        this._start = null;
	}
	
	// Update is called once per frame
	void Update () {
        Boolean shouldMoveEnemy = false;
        if (_start == null)
            shouldMoveEnemy = true;
        else if ((DateTime.Now - _start.Value).TotalMilliseconds >= this.Speed)
            shouldMoveEnemy = true;
        if (shouldMoveEnemy)
        {
            Move();
            _start = DateTime.Now;
        }
	}


	public void Move()
	{
		this.X++;
        transform.Translate((float)1.28, 0, 0);
	}

	public void Explode()
	{

	}

	

}
