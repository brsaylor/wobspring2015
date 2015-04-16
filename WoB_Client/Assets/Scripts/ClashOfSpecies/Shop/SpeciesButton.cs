using UnityEngine;
using System.Collections;

public class SpeciesButton : MonoBehaviour 
{
	public GUISkin myskin;
	
	public Rect windowRect;
	public bool paused = false , waited = true;
	
	public void Start()
	{
		
		windowRect = new Rect(62, 230, 410, 70);
	}
	
	private void waiting()
	{
		waited = true;
	}
	
	public void test1(){
		
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
			windowRect = GUI.Window(0, windowRect, windowFunc, "");
	}
	
	public void windowFunc(int id)
	{

		if (GUI.Button(new Rect(30,40,60,20),"Cancel"))
		{
			paused=false;
		}
		if (GUI.Button(new Rect(270,40,60,20),"OK"))
		{
			paused=false;
		}
	}
}