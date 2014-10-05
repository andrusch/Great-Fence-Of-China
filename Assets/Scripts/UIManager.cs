using UnityEngine;
using System.Collections;

public class UIManager : MonoBehaviour {

	public GUIText UITextBox;
	public GUIText LevelTextBox;
	public GameEngine game;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		// Update the player's score
		UITextBox.text = ": " + game.TotalSheepKilledInAllLevels.ToString();

		// Update the current level
		LevelTextBox.text = "Level " + game.Level.ToString();
	}
}
