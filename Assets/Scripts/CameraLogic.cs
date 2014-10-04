using UnityEngine;
using System.Collections;

public class CameraLogic : MonoBehaviour 
{
	public float xMargin = 1f;		// Distance in the x axis the  can move before the camera follows.
	public float yMargin = 1f;		// Distance in the y axis the focus can move before the camera follows.
	public float xSmooth = 8f;		// How smoothly the camera catches up with it's target movement in the x axis.
	public float ySmooth = 8f;		// How smoothly the camera catches up with it's target movement in the y axis.
	public Vector2 maxXAndY;		// The maximum x and y coordinates the camera can have.
	public Vector2 minXAndY;		// The minimum x and y coordinates the camera can have.
	private bool m_isHoriAxisInUse = false;
	private bool m_isVertAxisInUse = false;
	
	public Vector2 focus;		// Reference to the focus's transform.
	
	
	void Awake ()
	{
		// Setting up the reference.
		focus = new Vector2(0,0);
	}
	
	
	bool CheckXMargin()
	{
		// Returns true if the distance between the camera and the focus in the x axis is greater than the x margin.
		return Mathf.Abs(transform.position.x - focus.x) > xMargin;
	}
	
	
	bool CheckYMargin()
	{
		// Returns true if the distance between the camera and the focus in the y axis is greater than the y margin.
		return Mathf.Abs(transform.position.y - focus.y) > yMargin;
	}
	
	
	void FixedUpdate ()
	{
		Trackfocus();
		
		focus = GameObject.Find ("Player").transform.position;
	}
	
	
	void Trackfocus ()
	{
		// By default the target x and y coordinates of the camera are it's current x and y coordinates.
		float targetX = transform.position.x;
		float targetY = transform.position.y;
		
		// If the focus has moved beyond the x margin...
		//if(CheckXMargin())
		// ... the target x coordinate should be a Lerp between the camera's current x position and the focus's current x position.
		targetX = Mathf.Lerp(transform.position.x, focus.x, xSmooth * Time.deltaTime);
		
		// If the focus has moved beyond the y margin...
		//if(CheckYMargin())
		// ... the target y coordinate should be a Lerp between the camera's current y position and the focus's current y position.
		targetY = Mathf.Lerp(transform.position.y, focus.y, ySmooth * Time.deltaTime);
		
		// The target x and y coordinates should not be larger than the maximum or smaller than the minimum.
		targetX = Mathf.Clamp(targetX, minXAndY.x, maxXAndY.x);
		targetY = Mathf.Clamp(targetY, minXAndY.y, maxXAndY.y);
		
		// Set the camera's position to the target position with the same z component.
		transform.position = new Vector3(targetX, targetY, transform.position.z);
	}
}
