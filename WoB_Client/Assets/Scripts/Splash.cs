using UnityEngine;

using System;
using System.Collections;
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
<<<<<<< HEAD
		NetworkManager.Send(ClashEntryProtocol.Prepare(1), (response) => {
			var x = response as ResponseClashEntry;;
			Debug.Log(x.firstTime);
		});
		//mainObject.GetComponent<MessageQueue>().AddCallback(Constants.SMSG_AUTH, ResponseLogin);
	}

	void RunClashEntry(int id) {
		NetworkManager.Send(ClashEntryProtocol.Prepare(id), (response) => {
			var x = response as ResponseClashEntry;
			Debug.Log(x.firstTime);
			RunClashSpeciesList();
		});
	}

	void RunClashSpeciesList() {
		NetworkManager.Send(ClashSpeciesListProtocol.Prepare(), (response) => {
			var res = response as ResponseClashSpeciesList;
			foreach (var x in res.speciesList) {
				Debug.Log(x);
			}
=======
		StartCoroutine("RunTests");
		//mainObject.GetComponent<MessageQueue>().AddCallback(Constants.SMSG_AUTH, ResponseLogin);
	}

	IEnumerable RunTests() {
		Execute(ClashEntryProtocol.Prepare(1));
		yield return null;

		Execute(ClashSpeciesListProtocol.Prepare());
		yield return null;
	
	}

	void Execute(NetworkRequest req) {
		NetworkManager.Send(req, (response) => {
			Debug.Log(response);
>>>>>>> 0411567832f0df6683625d3d8358565851ed0518
		});
	}
	
	// Use this for initialization
	void Start() {
<<<<<<< HEAD
		sleepMethod ();
=======
		
>>>>>>> 0411567832f0df6683625d3d8358565851ed0518
	}
	
	void OnGUI() {
		// Background
		GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), animals);
		
		// Client Version Label
		GUI.Label(new Rect(Screen.width - 75, Screen.height - 30, 65, 20), "v" + Constants.CLIENT_VERSION + " Test");
<<<<<<< HEAD


=======
>>>>>>> 0411567832f0df6683625d3d8358565851ed0518
		
	}
	
	public void Show() {
		isHidden = false;
	}
	
	public void Hide() {
		isHidden = true;
	}
	
	// Update is called once per frame
	void Update() {
<<<<<<< HEAD

		//Game.SwitchScene("ClashDefense");
	}

	void sleepMethod(){
		StartCoroutine ("waitTime");
	}

	IEnumerator waitTime(){
		Debug.Log ("before");
		yield return new WaitForSeconds(5);
		Debug.Log ("after");
=======
>>>>>>> 0411567832f0df6683625d3d8358565851ed0518
	}
}