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
		StartCoroutine("RunTests");
		//mainObject.GetComponent<MessageQueue>().AddCallback(Constants.SMSG_AUTH, ResponseLogin);
	}

	IEnumerator RunTests() {
		Execute(ClashEntryProtocol.Prepare(1));
		yield return null;

		Execute(ClashSpeciesListProtocol.Prepare());
		yield return null;
	
		// Failing defense setup.
		Execute(ClashDefenseSetupProtocol.Prepare(2, new Dictionary<int, Vector2>()	{
			{ 15, new Vector2(0.5f, 0.5f) },
			{ 16, new Vector2(0.25f, 0.25f) },
			{ 21, new Vector2(0.3f, 0.3f) }
		}));
		yield return null;

		// Successful defense setup.
		// TODO: Make sure the integers refer to valid species ids that are in our subset.
		Execute(ClashDefenseSetupProtocol.Prepare(2, new Dictionary<int, Vector2>()	{
			{ 15, new Vector2(0.5f, 0.5f) },
			{ 16, new Vector2(0.25f, 0.25f) },
			{ 21, new Vector2(0.3f, 0.3f) },
			{ 23, new Vector2(0.75f, 0.75f) },
			{ 26, new Vector2(0.0f, 0.0f) }
		}));
		yield return null;

		Execute(ClashPlayerListProtocol.Prepare());
		yield return null;

		// Expects: 
		// 	Valid player with id 3.
		// 	Player with id 3 to have a valid defense configuration.
		Execute(ClashPlayerViewProtocol.Prepare(3));
		yield return null;

		// Failing initiate call.
		// TODO: Make sure that integers refer to  species ids that are in NOT our subset, since this should fail.
		Execute(ClashInitiateBattleProtocol.Prepare(3, new List<int>() { 6, 10, 2, 3, 4 }));
		yield return null; 

		// Valid initiate call.
		Execute(ClashInitiateBattleProtocol.Prepare(3, new List<int>() { 6, 10, 2, 3, 4 }));
		yield return null;

		Execute(ClashEndBattleProtocol.Prepare(true));
		yield return null;
	}

	void Execute(NetworkRequest req) {
		NetworkManager.Send(req, (response) => {
			Debug.Log(response.ToString());
		});
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
