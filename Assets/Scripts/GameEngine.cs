using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameEngine : MonoBehaviour {
	private Object[,] _grid;
	private List<Fence> _fencePieces;
	private List<Enemy> _enemies;
	private Player _player;
	public int TotalSheep;
	public int MaxSheepOnBoardAtOnce;
	private int _sheepAdded;

	public static GameEngine Instance = null;

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
		this._fencePieces = new List<Fence>();
		this._enemies = new List<Enemy>();
		this._player = new Player();
		this._sheepAdded = 0;
		this.TotalSheep = 10;
		this.MaxSheepOnBoardAtOnce = 1;
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
	void AddEnemy() 
	{
		if (_enemies.Count < MaxSheepOnBoardAtOnce && _sheepAdded < TotalSheep)
		{
			_enemies.Add(new Enemy());
			_sheepAdded++;
		}
	}
	void RemoveEnemy()
	{
		
	}
}
