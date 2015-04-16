using UnityEngine;
using System.Collections;

public class TerrianButton : MonoBehaviour 
{
	public GUISkin myskin;
	
	public Rect windowRect;
	public bool paused = false , waited = true;
	
	public void Start()
	{
	
		windowRect = new Rect(62, 37, 410, 195);
	}
	
 private void waiting()
	{
		waited = true;
	}

	public void test(){
	
		paused = true;
	}


	private void Update()
	{
				/*	if (waited) 
				if (Input.GetKey (KeyCode.T)) {
						if (!paused)
					//paused = false;
			
								paused = true;
			
						waited = false;
						Invoke ("waiting", 0.3f);
				}
		}
		*/
		}

	public void OnGUI()
	{
		if (paused)
			windowRect = GUI.Window(0, windowRect, windowFunc, "Please select your Terrain as defense map:");
	}
	
	public void windowFunc(int id)
	{
		if (GUI.Button(new Rect(10,20,60,60),"Terrian1"))
		{

		}
		
		if (GUI.Button(new Rect(80,20,60,60),"Terrian2"))
		{
			
		}
		if (GUI.Button(new Rect(150,20,60,60),"Terrian3"))
		{
			
		}
		if (GUI.Button(new Rect(220,20,60,60),"Terrian4"))
		{
			
		}

		if (GUI.Button(new Rect(290,20,60,60),"Terrian5"))
		{
			
		}

		if (GUI.Button(new Rect(10,90,60,60),"Terrian6"))
		{
			
		}
		if (GUI.Button(new Rect(80,90,60,60),"Terrian7"))
		{
			
		}
		if (GUI.Button(new Rect(150,90,60,60),"Terrian8"))
		{
			
		}
		if (GUI.Button(new Rect(30,160,60,20),"Cancel"))
		{
			paused=false;
		}
		if (GUI.Button(new Rect(250,160,60,20),"OK"))
		{
			paused=false;
		}
	}
}
