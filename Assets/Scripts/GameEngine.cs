using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class GameEngine : MonoBehaviour {
	private Piece[,] _grid;
	
	private List<Fence> _fencePieces;
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
		this._fencePieces = new List<Fence>();
		this._enemies = new List<Enemy>();
		this._sheepAdded = 0;
		this.TotalSheepInLevel = 10 * this.Level;
		this.MaxSheepOnBoardAtOnce = 3;
		this._grid = new Piece[this.BoardHeight, this.BoardWidth];
		this._enemyCountPerRow = new int[this.BoardHeight];
        this.SheepAddedAtOnce = 1;
        this.DelayBetweenSheepAdd = 3000;
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
            float transX = -8 + y;
            GameObject eGO = GameObject.Instantiate(SheepPrefab, new Vector3((float)(transX*1.28), (float)(1.28*transY)), Quaternion.identity) as GameObject;
			Enemy e = eGO.GetComponent<Enemy>();
			e.X = 0;
			e.Y = y;
			_enemies.Add(e);
			_enemyCountPerRow[e.Y]++;
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
