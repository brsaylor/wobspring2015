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
		Debug.Log("Setting player name to " + GameState.player.name);
		pd.SetPlayerName (GameState.player.name);
		Debug.Log("Setting player id to " + GameState.player.GetID());
		pd.SetPlayerId (GameState.player.GetID());

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
		NetworkManager.Send (ClashEntryProtocol.Prepare (), (res) => {
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

	void GetSpeciesList() {
		NetworkManager.Send(ClashSpeciesListProtocol.Prepare(), (res) => {
			var response = res as ResponseClashSpeciesList;
			if(response.speciesList != null && response.speciesList.Count == 12) {
				foreach(ClashSpeciesData spd in response.speciesList) {
					ClashUnitData cud = new ClashUnitData();
					cud.species_name = spd.species_name;
					cud.species_id = spd.species_id;
					cud.hp = spd.hit_points;
					cud.cost = spd.species_price;
					cud.prefab_id = (int)spd.species_type;
					cud.attack = spd.attack_points;
					cud.attack_speed = spd.attack_speed;
					cud.movement_speed = spd.movement_speed;
					cud.description = spd.description;
					pd.species_list.Add(cud);
				}
			}
		});
	}
}
