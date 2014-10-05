using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class GameEngine : MonoBehaviour {
	private Enemy[,] _grid;
    public int TotalSheepInLevel;
	private Stack<Fence>[] _fencePieces;
	private List<Enemy> _enemies;
	public int MaxSheepOnBoardAtOnce;
	public GameObject SheepPrefab;
	private int _sheepAdded;
	public static GameEngine Instance = null;
	private int[] _enemyCountPerRow;
    public int SheepAddedAtOnce;
    public int DelayBetweenSheepAdd;
    DateTime? _whenLastSheepAdded = null;
    public Player _player;
    private int _heartCount;
	private int _sheepCount;
    private Stack<GameObject> _hearts;
    public GameObject HeartPrefab;
	public float HeartStartX;
	public float HeartStartY;
    public GameObject FencePrefab;
	public float SheepCounterStartX;
	public float SheepCounterStartY;
    public float LevelSheepIncreaseFactor;
    public float DelayBetweenSheepLevelFactor;
    private int _lastFenceTouched = -1;
	
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
		this._grid = new Enemy[this.BoardHeight, this.BoardWidth];
		this._enemyCountPerRow = new int[this.BoardHeight];
        this.TotalSheepKilledInAllLevels = 0;
        this._heartCount = 0;
        this._hearts = new Stack<GameObject>();
	}
	// Update is called once per frame
	void Update () 
	{
        if (this.IsLevelOver())
        {
            if (this.DidIWin())
            {
                GoToNextLevel();
            }
            else
            {
                DisplayGameOver();
            }
        }
        else {
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
        UpdateHearts();
	}
    void GoToNextLevel()
    {
        Level++;
        _sheepAdded = 0;
        TotalSheepInLevel = (int)Math.Round(LevelSheepIncreaseFactor * Level * TotalSheepInLevel);
        DelayBetweenSheepAdd = (int)Math.Round(DelayBetweenSheepLevelFactor * Level * DelayBetweenSheepAdd);
    }
    void DisplayGameOver()
    {

    }
    void UpdateHearts()
    {
        if (this.Player.Health != this._heartCount)
        {
            int diff = this.Player.Health - this._heartCount;
            while (diff != 0)
            {
                if (diff > 0)
                {
                    GameObject heart = GameObject.Instantiate(HeartPrefab, new Vector3(HeartStartX - ((_hearts.Count + 1) * .75f), HeartStartY, -3), Quaternion.identity) as GameObject;
                    _hearts.Push(heart);
                    diff--;
                }
                else
                {
                    GameObject heart = _hearts.Pop();
                    diff++;
                    Destroy(heart, 0.45f);
                }
            }
            this._heartCount = this.Player.Health;
        }
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
            this.Player.TakeDamage(damageLeftToDeal);
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
	public bool IsLevelOver()
	{
        return (_enemies.Count == 0 && TotalSheepInLevel == _sheepAdded) || (this.Player.Health <= 0);
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
        }
        if (_fencePieces[y].Count == 0)
        {
            GameObject eGO = GameObject.Instantiate(FencePrefab, new Vector3((float)(transX * 0.64), (float)(1.28 * transY)), Quaternion.identity) as GameObject;
            Fence e = eGO.GetComponent<Fence>();
            _fencePieces[y].Push(e);
        }
        Fence f = _fencePieces[y].Peek();
        if (f.Health == f.MaxHealth)
        {
            GameObject eGO = GameObject.Instantiate(FencePrefab, new Vector3((float)(transX * 0.64), (float)(1.28 * (transY + _fencePieces[y].Count))), Quaternion.identity) as GameObject;
            Fence e = eGO.GetComponent<Fence>();
            _fencePieces[y].Push(e);
        }
        _fencePieces[y].Peek().AddHealth(_lastFenceTouched != -1 && _lastFenceTouched == y);
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
    
    public int TotalSheepKilledInAllLevels { get; private set; }
    public int Level { get; private set; }
    public int BoardHeight { get; private set; }
    public int BoardWidth { get; private set; }

}
