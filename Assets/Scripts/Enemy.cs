using UnityEngine;
using System.Collections;
using System.Threading;
using System;

public class Enemy : Piece {
	public int Power;
	public double Speed;
	public bool DoesStun;
    private DateTime? _start;
	// Use this for initialization
	void Start () {
		this.Power = 1;
		this.DoesStun = false;
		this.Speed = 1000;
        this._start = null;
	}
	
	// Update is called once per frame
	void Update () {
        Boolean shouldMoveEnemy = false;
        if (_start == null)
            shouldMoveEnemy = true;
        else
        {
            double ms = (DateTime.Now - _start.Value).TotalMilliseconds;
            if (ms >= this.Speed)
                shouldMoveEnemy = true;
        }
        if (shouldMoveEnemy)
        {
            Move();
            _start = DateTime.Now;
            
        }
	}


	public void Move()
	{
        if (this.X < GameEngine.Instance.BoardWidth)
        {
            this.X++;
            transform.Translate((float)1.28, 0, 0);
        }
	}

	public void Explode()
	{

	}

	

}
