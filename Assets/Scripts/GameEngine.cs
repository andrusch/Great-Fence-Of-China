﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class GameEngine : MonoBehaviour {
	private Enemy[,] _grid;
	
	private Stack<Fence>[] _fencePieces;
	private List<Enemy> _enemies;
	public int TotalSheepInLevel;
	public int TotalSheepKilledInAllLevels;
	public int MaxSheepOnBoardAtOnce;
	public GameObject SheepPrefab;
	private int _sheepAdded;
	public int Level;
	public int BoardHeight;
	public int BoardWidth;
	public static GameEngine Instance = null;
	private int[] _enemyCountPerRow;
    public int SheepAddedAtOnce;
    public int DelayBetweenSheepAdd;
    DateTime? _whenLastSheepAdded = null;
    private int HeartCount;
    private List<GameObject> _hearts;
	void Awake()
	{
		if (Instance != null) 
		{
			Debug.LogError("A second GameState has been created!");
		}
		Instance = this;
	}
	// Use this for initialization
	void Start () 
	{
		this.Level = 1;
		this.BoardHeight = 5;
		this.BoardWidth = 9;
		this._fencePieces = new Stack<Fence>[this.BoardHeight];
		this._enemies = new List<Enemy>();
		this._sheepAdded = 0;
		this.TotalSheepInLevel = 10 * this.Level;
		this.MaxSheepOnBoardAtOnce = 3;
		this._grid = new Enemy[this.BoardHeight, this.BoardWidth];
		this._enemyCountPerRow = new int[this.BoardHeight];
        this.SheepAddedAtOnce = 1;
        this.DelayBetweenSheepAdd = 3000;
        this.TotalSheepKilledInAllLevels = 0;
        this.HeartCount = 0;
        this._hearts = new List<GameObject>();
	}
	// Update is called once per frame
	void Update () 
	{
        Boolean shouldAddSheep = false;
        if (_whenLastSheepAdded == null)
            shouldAddSheep = true;
        else
        {
            double ms = (DateTime.Now - _whenLastSheepAdded.Value).TotalMilliseconds;
            if (ms >= this.DelayBetweenSheepAdd)
                shouldAddSheep = true;
        }
        if (shouldAddSheep)
        {
            for (int i = 0; i < this.SheepAddedAtOnce; i++)
            {
                AddEnemy();
            }
            _whenLastSheepAdded = DateTime.Now;
        }
	}
	void AddEnemy() 
	{
		if (_enemies.Count < MaxSheepOnBoardAtOnce && _sheepAdded < TotalSheepInLevel)
		{
            int y = GenerateYForEnemy();
            float transY = y - 3;
            float transX = -9 + y;
            GameObject eGO = GameObject.Instantiate(SheepPrefab, new Vector3((float)(transX*1.28), (float)(1.28*transY)), Quaternion.identity) as GameObject;
			Enemy e = eGO.GetComponent<Enemy>();
			e.X = 0;
			e.Y = y;
			_enemies.Add(e);
			_enemyCountPerRow[e.Y]++;
            _grid[e.Y, e.X] = e;
			_sheepAdded++;
		}
	}
	int GenerateYForEnemy()
	{
		int row = 0;
		int minCount = _enemyCountPerRow[0];
		if (minCount == 0)
			return minCount;
		for (int i =1; i<this.BoardHeight; i++)
		{
			if (_enemyCountPerRow[i] == 0)
			{
				row = i;
				minCount = _enemyCountPerRow[i];
				break;
			}				
			if (minCount > _enemyCountPerRow[i])		
			{
				row = i;
				minCount = _enemyCountPerRow[i];				
			}
		}
		return row;
	}
	
	public void RemoveEnemy(Enemy e)
	{
        _enemyCountPerRow[e.Y]--;
        _enemies.Remove(e);
        _grid[e.Y, e.X] = null;
        if (e.HasExploded)
        {
            CauseDamageToFenceOrPlayer(e);
            this.TotalSheepKilledInAllLevels++;
        }
	}
    public void CauseDamageToFenceOrPlayer(Enemy e)
    {
        int y = e.Y;
        bool shouldDamagePlayer = false;
        int damageLeftToDeal = e.Power;
        if (_fencePieces[y] == null)
        {
            _fencePieces[y] = new Stack<Fence>();
            shouldDamagePlayer = true;
        }
        else
        {
            while (damageLeftToDeal != 0 && !shouldDamagePlayer)
            {
                if (_fencePieces[y].Count == 0)
                    shouldDamagePlayer = true;
                else
                {
                    Fence f = _fencePieces[y].Peek();
                    damageLeftToDeal = f.TakeDamage(damageLeftToDeal);
                    if (f.Health <= 0)
                        _fencePieces[y].Pop();
                }
            }
        }
        if (shouldDamagePlayer)
        {
            
        }
    }
    public void UpdateEnemyLocation(Enemy e, int newX, int newY)
    {
        _grid[e.Y, e.X] = null;
        _grid[newY, newX] = e;
    }
    public bool CanEnemyMoveToSpace(Enemy e, int newX, int newY)
    {
        return _grid[newY, newX] == null;  
    }
	bool IsLevelOver()
	{
		return _enemies.Count == 0 && TotalSheepInLevel == _sheepAdded;
	}
	bool DidIWin()
	{
		//return IsLevelOver() && this._player.Health > 0;
        return IsLevelOver();
	}
}
