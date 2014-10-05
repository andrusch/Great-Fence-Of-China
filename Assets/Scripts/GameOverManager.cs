using UnityEngine;
using System.Collections;

public class GameOverManager : MonoBehaviour {

	public string TitleScene;
	private bool displayButtons = true;
	public GUISkin Newskin;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI()
	{
		GUI.skin = Newskin;

		if (this.displayButtons)
		{
			// display an area for buttons
			//75,75,400,400,     default 20,20,600,600
			GUILayout.BeginArea (new Rect (Screen.width*0.35f, 0, Screen.width*0.75f, Screen.height));
			GUILayout.Space (Screen.height*0.55f);

			if (GUILayout.Button ("Main Menu", GUILayout.Height(Screen.height*0.125f), GUILayout.Width(Screen.width*0.3f))) 
			{ 
				Application.LoadLevel(TitleScene); 
				this.displayButtons = false;
			}


			// I can't remember what anything below here does
			GUILayout.Space (Screen.height/20);
			GUILayout.BeginHorizontal ();
			
			GUILayout.EndHorizontal ();
			GUILayout.EndArea ();
		}
	}

}
