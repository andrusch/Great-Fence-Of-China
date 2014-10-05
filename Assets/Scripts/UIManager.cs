using UnityEngine;
using System.Collections;

public class UIManager : MonoBehaviour {

	public GUIText UITextBox;
	public GameEngine game;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		UITextBox.text = ": " + game.TotalSheepKilledInAllLevels.ToString();
	}
}
