using UnityEngine;
using System.Collections;

public class TitleManager : MonoBehaviour {

	public string LevelScene;
	public string CreditsScene;
	public string InstructionsScene;
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
			GUILayout.BeginArea (new Rect (Screen.width*0.3f, 0, Screen.width*0.7f, Screen.height));
			GUILayout.Space (Screen.height*0.4f);

			if (GUILayout.Button ("Play", GUILayout.Height(Screen.height*0.075f), GUILayout.Width(Screen.width*0.4f))) 
			{ 
				Application.LoadLevel(LevelScene); 
				this.displayButtons = false;
			}

			GUILayout.Space (Screen.height*0.05f);

			if (GUILayout.Button ("Instructions", GUILayout.Height(Screen.height*0.075f), GUILayout.Width(Screen.width*0.4f))) 
			{ 
				Application.LoadLevel(InstructionsScene); 
				this.displayButtons = false;
			}

			GUILayout.Space (Screen.height*0.05f);
			
			if (GUILayout.Button ("Credits", GUILayout.Height(Screen.height*0.075f), GUILayout.Width(Screen.width*0.4f))) 
			{ 
				Application.LoadLevel(CreditsScene); 
				this.displayButtons = false;
			}

			GUILayout.Space (Screen.height*0.05f);
			
			if (GUILayout.Button ("Quit", GUILayout.Height(Screen.height*0.075f), GUILayout.Width(Screen.width*0.4f))) 
			{ 
				Application.Quit(); 
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
