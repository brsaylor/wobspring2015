using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class UnitData {
	public string species_id;
	public string type;
	public int hp;
	public Vector3 location;

	public string ToString() {
		return "species_id: " + species_id + " Type: " + type + " Biomass: " + hp;
	}

	public bool Equals(UnitData other) {
		if (other == null)
			return false;
		return(this.species_id.Equals (other.species_id));
	}
}

[System.Serializable]
public class Defender {
	public string player_id;
	public string terrain;
	public List<UnitData> defense;
}

[System.Serializable]
public class Attacker {
	public string player_id;
	public List<UnitData> offense;
}

/***************************
	 * If type == offense, defender is the enemy and attacker is the player
	 * If type == defense, defender is the player, no attacker
	 * 
	 * Defense Set-up:
	 * SetSceneType = "defense"
	 * SetDefenderId(GetCurrentPlayer())
	 * SetDefenderTerrain(terrain) to be the selected terrain
	 * AddToUnitList(species_id, [carnivore,herbivore,plant1,plant2,etc], biomass#)
	 * RemoveFromUnitList(species_id)
	 * 
	 * Offense Set-up:
	 * SetSceneType = "offense"
	 * SetDefenderId(selectedEnemyId)
	 * SetDefenderTerrain(enemy's terrain)
	 * SetAttackerId(GetCurrentPlayer())
	 * 
	 * Go to ClashShop scene to load up units
	 * Then after the user pushes "Engage", 
	 * 	make a call to Server for Defender's units and their biomass and locations
	 **/


public class PersistentData : MonoBehaviour {
	public string current_player;
	public string type;	//is data for offense setup scene or defense setup scene
	public Defender defenderInfo;
	public Attacker attackerInfo;

	void Awake() {
		DontDestroyOnLoad (this);
		type = "defense";		//starts off as defense
	}

	void Start() {
		defenderInfo.defense = new List<UnitData> ();
		attackerInfo.offense = new List<UnitData> ();
	}

	public string GetCurrentPlayer() {
		return current_player;
	}

	public void SetCurrentPlayer(string s) {
		current_player = s;
	}

	public string GetSceneType() {
		return type;
	}
	
	public void SetSceneType(string s) {
		type = s;
	}

	public void AddToUnitList(string species_id, string type, int hp) {
		UnitData ud = new UnitData ();
		ud.species_id = species_id;
		ud.type = type;
		ud.hp = hp;

		if (type == "offense")
			this.attackerInfo.offense.Add (ud);
		else if (type == "defense")
			this.defenderInfo.defense.Add (ud);
	}

	public int GetTeamSize() {
		if (type == "offense")
			return this.attackerInfo.offense.Count;
		else if (type == "defense")
			return this.defenderInfo.defense.Count;
		else
			return -1;
	}

	public bool RemoveFromUnitList(string id) {
		return this.defenderInfo.defense.Remove (new UnitData(){species_id = id});
	}
	
	/******************************/

	public string GetDefenderId() {
		return this.defenderInfo.player_id;
	}

	public void SetDefenderId(string player_id) {
		this.defenderInfo.player_id = player_id;
	}

	public string GetDefenderTerrain() {
		return this.defenderInfo.terrain;
	}

	public void SetDefenderTerrain(string terrain) {
		this.defenderInfo.terrain = terrain;
	}

	public void ClearDefenderData() {
		this.defenderInfo.player_id = "";
		this.defenderInfo.terrain = "";
		this.defenderInfo.defense.Clear();
	}

	/*****************************/

	public string GetAttackerId() {
		return this.attackerInfo.player_id;
	}

	public void SetAttackerId(string player_id) {
		this.attackerInfo.player_id = player_id;
	}
	
	public void ClearAttackerData() {
		this.attackerInfo.player_id = "";
		this.attackerInfo.offense.Clear ();
	}

	/******************************/

	public void ClearAllData() {
		defenderInfo = null;
		attackerInfo = null;
		defenderInfo = new Defender ();
		attackerInfo = new Attacker ();
		defenderInfo.defense = defenderInfo.defense ?? new List<UnitData> ();
		attackerInfo.offense = attackerInfo.offense ?? new List<UnitData> ();
	}

}
