using UnityEngine;
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
    public Player _player;
    private int HeartCount;
	private int SheepCount;
    private List<GameObject> _hearts;
    public GameObject HeartPrefab;
	public float HeartStartX;
	public float HeartStartY;
    public GameObject FencePrefab;
	public GameObject SheepIconPrefab;
	public float SheepCounterStartX;
	public float SheepCounterStartY;
	
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
		this.BoardWidth = 10;
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
		this.SheepCount = 0;
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
        UpdateHearts();
		UpdateScore();
	}
    void UpdateHearts()
    {
		if (this.Player.Health != this.HeartCount)
		{
			Debug.Log("Draw Heart");
			for (int i=0; i< this.Player.Health; i++)
			{
				GameObject heart = GameObject.Instantiate(HeartPrefab, new Vector3(HeartStartX - (i * .75f), HeartStartY, -3), Quaternion.identity) as GameObject;
			}
			this.HeartCount = this.Player.Health;
		}
	}

	// I have absolutely no idea what I'm doing here, and have currently copy-pasta-ed the code
	// from last Game Jam into this function that doesn't really do anything.
	void UpdateScore()
	{
		//if (this.SheepCount != this.TotalSheepKilledInAllLevels)
		//{
		//	GUI.Box(new Rect(Screen.width /2 - 100, 10, 170, 20),  "Sheep: " + this.SheepCount.ToString());
		//}
		//this.SheepCount = this.TotalSheepKilledInAllLevels;
	}

	void AddEnemy() 
	{
		if (_enemies.Count < MaxSheepOnBoardAtOnce && _sheepAdded < TotalSheepInLevel)
		{
            int y = GenerateYForEnemy();
            float transY = y - 3;
            float transX = -16 + y;
            GameObject eGO = GameObject.Instantiate(SheepPrefab, new Vector3((float)(transX*0.64), (float)(1.28*transY)), Quaternion.identity) as GameObject;
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
        return IsLevelOver() && this.Player.Health > 0;
    }
    public void BuildFence(int y)
    {
        float transY = y - 3;
        float transX = y + 4;
        if (_fencePieces[y] == null)
        {
            _fencePieces[y] = new Stack<Fence>();
            GameObject eGO = GameObject.Instantiate(FencePrefab, new Vector3((float)(transX * 0.64), (float)(1.28 * transY)), Quaternion.identity) as GameObject;
            Fence e = eGO.GetComponent<Fence>();
            _fencePieces[y].Push(e);
        }
        Fence f = _fencePieces[y].Peek();
        if (f.Health == f.MaxHealth)
        {
            GameObject eGO = GameObject.Instantiate(FencePrefab, new Vector3((float)(transX * 0.64), (float)(1.28 * transY)), Quaternion.identity) as GameObject;
            Fence e = eGO.GetComponent<Fence>();
            _fencePieces[y].Push(e);
        }
        _fencePieces[y].Peek().Health++;
    }
	public Player Player
	{
		get
		{
			if (this._player == null)
				this._player = GameObject.Find ("Player").GetComponent<Player>();
			return this._player;
		}

	}
}
