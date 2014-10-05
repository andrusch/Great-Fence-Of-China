using UnityEngine;
using System.Collections;

public class TitleManager : MonoBehaviour {

	public string LevelScene;
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
			GUILayout.BeginArea (new Rect (Screen.width*0.4f, 0, Screen.width*0.6f, Screen.height));
			GUILayout.Space (Screen.height*0.75f);
			//GUI.BeginGroup (new Rect (0, 0, 300, 198));
			//GUI.Box (new Rect (0, 0, 300, 198), backgroundSplash);
			//GUILayout.Label ("Load a scene and fade in a looped Audio Clip");
			//if (GUILayout.Button ("Menu Scene", GUILayout.Height (50))) { Application.LoadLevel 
			//    (1); }
			if (GUILayout.Button ("Play", GUILayout.Height(Screen.height*0.075f), GUILayout.Width(Screen.width*0.2f))) 
			{ 
				Application.LoadLevel(LevelScene); 
				this.displayButtons = false;
			}
			//if (GUILayout.Button ("Title", GUILayout.Height (50))) {Application.LoadLevel 
			//	(0); }
			//if (GUILayout.Button ("End Scene", GUILayout.Height (60))) { Application.LoadLevel 
			//	(4); }
			GUILayout.Space (Screen.height/20);
			// title
			//GUILayout.Label ("Play a single sound");
			GUILayout.BeginHorizontal ();
			/*
			// Demonstrates playing a single sound clip
			if (GUILayout.Button ("Play SFX 1")) { AudioHelper.CreatePlayAudioObject 
				(sfx1); }
			if (GUILayout.Button ("Play SFX 2")) { AudioHelper.CreatePlayAudioObject 
				(sfx2); }
			if (GUILayout.Button ("Play SFX 3")) { AudioHelper.CreatePlayAudioObject 
				(sfx3); }
			*/
			GUILayout.EndHorizontal ();
			GUILayout.EndArea ();
		}
	}

}
