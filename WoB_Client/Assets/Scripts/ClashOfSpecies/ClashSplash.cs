using UnityEngine;

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using Test = System.Diagnostics.Debug;

public class ClashSplash : MonoBehaviour {

	GameObject required_object;
	ClashPersistentData pd;
	private GameObject mainObject;
	// Other
	public Texture animals;
	private Rect windowRect;
	private bool isHidden;
	
	void Awake() {
		mainObject = GameObject.Find("MainObject");
		required_object = GameObject.Find ("Persistent Object");
		if (required_object == null) {
			//return to lobby because error
			Debug.Log("Error re-enter game from lobby");
		}
	}
	
	// Use this for initialization
	IEnumerator Start() {
		//sleep 5 seconds
		yield return new WaitForSeconds(5);
		//sleepMethod ();

		pd = required_object.GetComponent<ClashPersistentData> ();
		pd.SetPlayerName ("Player Name");
		pd.SetPlayerId (2);

		defenseRequest ();

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
	IEnumerator waitTime(){
		//Debug.Log ("before");
		yield return new WaitForSeconds(5);
		//Debug.Log ("after");
	}
	

	void defenseRequest(){
		//request server to check if defense map is in database table
		//bool b = RequestResult();
		//if(b) {
		NetworkManager.Send (ClashEntryProtocol.Prepare (pd.GetPlayerId()), (res) => {
			Debug.Log ("got clash entry response from server");
			var response = res as ResponseClashEntry;

			if(response.firstTime){
				pd.type = "defense";
				Application.LoadLevel("ClashShop");
			}else{
				pd.type = "offense";
				Application.LoadLevel("ClashMain");
			}
		});

	}
}
