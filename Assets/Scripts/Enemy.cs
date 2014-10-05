using UnityEngine;
using System.Collections;
using System.Threading;
using System;

public class Enemy : Piece {
	public int Power;
	public double Speed;
	public bool DoesStun;
	public float RandomBleatChance;
    private DateTime? _start;
    private bool _exploded;
	// Use this for initialization
	void Start () {
		this.Power = 1;
		this.DoesStun = false;
		this.Speed = 1000;
        this._start = null;
        this._exploded = false;
        _start = DateTime.Now;
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
        if (shouldMoveEnemy && GameEngine.Instance.CanEnemyMoveToSpace(this, this.X +1, this.Y))
        {
            Move();
            _start = DateTime.Now;
        }
	}


	public void Move()
	{
        if (this.X < GameEngine.Instance.BoardWidth)
        {
            GameEngine.Instance.UpdateEnemyLocation(this, this.X + 1, this.Y);
            this.X++;
            transform.Translate((float)1.28, 0, 0);

			float BleatRoll = UnityEngine.Random.Range(0.0f,1.0f);
			// 90% chance to bleat
			if (BleatRoll <= RandomBleatChance)
			{
				audio.Play();
			}

        }
        else
        {
            Explode();
        }
	}
    public Boolean HasExploded
    {
        get { return _exploded; }
    }
	public void Explode()
	{
        if (!_exploded)
        {
            // insert sound here

            // insert animation here
            _exploded = true;
            GameEngine.Instance.RemoveEnemy(this);
            Destroy(gameObject, 1.0f);
            
        }
	}

	

}
