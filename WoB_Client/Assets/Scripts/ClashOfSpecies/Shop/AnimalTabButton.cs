using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class AnimalTabButton : MonoBehaviour 
{
	public GUISkin myskin;
	
	public Rect windowRect;
	public bool paused = false, select = false;
	bool attackShop;
	
	public void Start()
	{
	
		//attackShop = GameObject.Find ("PersistentData").GetComponent ("PersistentData").isAttackShop ();

		windowRect = new Rect(62, 37, 410, 195);

	}

	
	public void test(){
		
		paused = true;
	}
	
	
	private void Update()
	{

	}
	
	public void OnGUI()
	{
		if (paused)
			windowRect = GUI.Window(0, windowRect, windowFunc, "Please select your Terrain as defense map:");
	}
	
	public void windowFunc(int id)
	{
		if (GUI.Button(new Rect(10,20,60,60),"Animal1"))
		{
			select=true;
		}
		
		if (GUI.Button(new Rect(80,20,60,60),"Animal2"))
		{
			
		}
		if (GUI.Button(new Rect(150,20,60,60),"Animal3"))
		{
			
		}
		if (GUI.Button(new Rect(220,20,60,60),"Animal4"))
		{
			
		}
		
		if (GUI.Button(new Rect(290,20,60,60),"Animal5"))
		{
			
		}
		
		if (GUI.Button(new Rect(10,90,60,60),"Animal6"))
		{
			
		}
		if (GUI.Button(new Rect(80,90,60,60),"Animal7"))
		{
			
		}
		if (GUI.Button(new Rect(150,90,60,60),"Animal8"))
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
	public void ExtraGUI()
	{

		if (select)
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
}