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
		});
	}
	
	// Use this for initialization
	void Start() {
		
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
}