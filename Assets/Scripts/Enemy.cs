using UnityEngine;
using System.Collections;
using System.Threading;
using System;

public class Enemy : Piece {
	public int Power;
	public double Speed;
	public bool DoesStun;
	public float RandomBleatChance;
	public GameObject explosionPrefab;
    private DateTime? _start;
    private bool _exploded;
	// Use this for initialization
	void Start () {
		this.Power = 1;
		this.DoesStun = false;
		this.Speed = 1000;
        this._start = null;
        this._exploded = false;
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

	public void Explode()
	{
        if (!_exploded)
        {
            // insert sound here

            // insert animation here
			var splode = transform.position;
			GameObject.Instantiate(explosionPrefab,splode,Quaternion.identity);

			Destroy(gameObject, 0.1f);
            GameEngine.Instance.RemoveEnemy(this);
            _exploded = true;
        }
	}

	

}
