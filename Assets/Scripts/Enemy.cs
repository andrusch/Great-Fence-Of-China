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
    public GameObject explosionPrefab;

	AudioSource bleat1;
	AudioSource bleat2;
	AudioSource bleat3;
	AudioSource splode1;
	AudioSource splode2;
	AudioSource splode3;

	// Use this for initialization
	void Start () {
        this._start = null;
        this._exploded = false;
        _start = DateTime.Now;

		//Grab all the audio source components and identify them
		AudioSource[] audios = GetComponents<AudioSource>();
		bleat1 = audios[0];
		bleat2 = audios[1];
		bleat3 = audios[2];
		splode1 = audios[3];
		splode2 = audios[4];
		splode3 = audios[5];
	}
	
	// Update is called once per frame
    void Update()
    {
        if (!GameEngine.Instance.IsLevelOver())
        {
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
            }
        }
    }


	public void Move()
	{
        
        if (this.X + 1 < GameEngine.Instance.BoardWidth)
        {
            if (GameEngine.Instance.CanEnemyMoveToSpace(this, this.X + 1, this.Y))
            {   
                transform.Translate((float)1.28, 0, 0);
                _start = DateTime.Now;
                GameEngine.Instance.UpdateEnemyLocation(this, this.X+1, this.Y);
                this.X++;
				float BleatRoll = UnityEngine.Random.Range(0.0f,1.0f);
				// Check against the bleat chance
				if (BleatRoll <= RandomBleatChance)
				{
					//If successful bleat, roll again for random bleat sound
					BleatRoll = UnityEngine.Random.Range(0.0f,1.0f);
					if(BleatRoll <= 0.33f)
						bleat1.Play();
					else if(BleatRoll <= 0.66f)
						bleat2.Play();
					else
						bleat3.Play();
				}
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
			_exploded = true;
			
			// Play a random explosion sound here
			float SplodeRoll = UnityEngine.Random.Range(0.0f,1.0f);
			if(SplodeRoll <= 0.33f)
				splode1.Play();
			else if(SplodeRoll <= 0.66f)
				splode2.Play();
			else
				splode3.Play();

			// Instantiate the explosion animation here
			var splode = transform.position;
			GameObject.Instantiate(explosionPrefab, splode, Quaternion.identity);

			// Instantiate wool ball particle emitter here
            
			// Destroy myself
			GameEngine.Instance.RemoveEnemy(this);
            Destroy(gameObject, 0.45f);
        }
	}

	

}
