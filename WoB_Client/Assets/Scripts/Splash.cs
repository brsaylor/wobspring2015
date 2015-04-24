using UnityEngine;

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using Test = System.Diagnostics.Debug;

public class Splash : MonoBehaviour {
	
	private GameObject mainObject;
	// Other
	public Texture animals;
	private Rect windowRect;
	private bool isHidden;
	
	void Awake() {
		mainObject = GameObject.Find("MainObject");
		//mainObject.GetComponent<MessageQueue>().AddCallback(Constants.SMSG_AUTH, ResponseLogin);
	}
	
	// Use this for initialization
	void Start() {
		//sleep 5 seconds
		sleepMethod ();
	}
	
	void OnGUI() {
		// Background
		GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), animals);
		
		// Client Version Label
		GUI.Label(new Rect(Screen.width - 75, Screen.height - 30, 65, 20), "v" + Constants.CLIENT_VERSION + " Test");
		
	}
	
	public void Show() {
		isHidden = false;
	}
	
	public void Hide() {
		isHidden = true;
	}
	
	// Update is called once per frame
	void Update() {
	}

	//method to make splash screen wait 5 seconds
	void sleepMethod(){
		StartCoroutine ("waitTime");
	}
	
	IEnumerator waitTime(){
		//Debug.Log ("before");
		yield return new WaitForSeconds(5);
		//Debug.Log ("after");
	}
}
