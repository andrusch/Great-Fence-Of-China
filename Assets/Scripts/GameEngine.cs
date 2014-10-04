using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameEngine : MonoBehaviour {
	private Object[,] _grid;
	
	private List<Fence> _fencePieces;
	private List<Enemy> _enemies;
	private Player _player;
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
		this.BoardWidth = 13;
		this._fencePieces = new List<Fence>();
		this._enemies = new List<Enemy>();
		this._player = new Player();
		this._sheepAdded = 0;
		this.TotalSheepInLevel = 10 * this.Level;
		this.MaxSheepOnBoardAtOnce = 1;
		this._grid = new Object[this.BoardHeight, this.BoardWidth];
		this._enemyCountPerRow = new int[this.BoardHeight];
	}
	// Update is called once per frame
	void Update () 
	{	
		AddEnemy();
		System.Threading.Thread.Sleep(1000);
		foreach (Enemy e in _enemies)
		{
			e.Move();
		}
	}
	
	void OnGUI()
	{		
		if (this == null)
			Debug.LogError("CANNOT FIND MY PARENT");
		else
		{
			Vector2 targetPos;
			Rect cameraRect = Camera.main.pixelRect;
			
			if (this.IsLevelOver())
			{
				if (this.DidIWin())
					GUI.Box(new Rect(Screen.width /2 - 100, 10, 170, 20),  "You Win! " + this.TotalSheepKilledInAllLevels.ToString());
				else 
					GUI.Box(new Rect(Screen.width /2 - 100, 10, 170, 20),  "You Lose! " + this.TotalSheepKilledInAllLevels.ToString());
			}
			else
			{
				GUI.Box(new Rect(Screen.width /2 - 100, 10, 170, 20),  "Your Score Is: " + this.TotalSheepKilledInAllLevels.ToString());
			}
		}
	}
	void AddEnemy() 
	{
		if (_enemies.Count < MaxSheepOnBoardAtOnce && _sheepAdded < TotalSheepInLevel)
		{
			Enemy e = new Enemy();
			e.x = 0;
			e.y = GenerateYForEnemy();
			_enemies.Add(e);
			_enemyCountPerRow[e.y]++;
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
	
	void RemoveEnemy()
	{
		
	
	}
	bool IsLevelOver()
	{
		return _enemies.Count == 0 && TotalSheepInLevel == _sheepAdded;
	}
	bool DidIWin()
	{
		return IsLevelOver() && this._player.Health > 0;
	}
	bool TryMove(Object piece, int x, int y)
	{
		
	}
}
