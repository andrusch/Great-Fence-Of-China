using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameEngine : MonoBehaviour {
	private Object[,] _grid;
	private List<Fence> _fencePieces;
	private List<Enemy> _enemies;
	private Player _player;

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
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
	void AddSheep() 
	{
		
	}
	void RemoveSheep()
	{
	
	}
}
