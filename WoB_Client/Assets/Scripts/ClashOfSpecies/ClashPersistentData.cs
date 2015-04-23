﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class ClashUnitData {
	public string species_name;
	public int species_id;
	public int prefab_id; //carnivore, herbi, omni, plant1, plant2, etc (type)
	public int hp, attack, attackSpeed, movementSpeed;
	public Vector3 location;
	public bool isDeployed;

	public override string ToString() {
		return "species_id: " + species_id + " Prefab: " + prefab_id + " Biomass: " + hp;
	}

	public bool Equals(ClashUnitData other) {
		if (other == null)
			return false;
		return(this.species_id.Equals (other.species_id));
	}
}

[System.Serializable]
public class ClashDefender {
	public string player_name;
	public int player_id;
	public int terrain_id;
	public List<ClashUnitData> defense;
}

[System.Serializable]
public class ClashAttacker {
	public string player_name;
	public int player_id;
	public List<ClashUnitData> offense;
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


public class ClashPersistentData : MonoBehaviour {
	[SerializeField]
	private string player_name;	//player username

	[SerializeField]
	private int player_id;

	public ClashDefender defenderInfo;
	public ClashAttacker attackerInfo;
	public List<ClashUnitData> species_list;
	public List<GameObject> terrain_list;

	public string type;	//is data for offense setup scene or defense setup scene


	void Awake() {
		DontDestroyOnLoad (this);
		type = "defense";		//starts off as defense
	}

	void Start() {
		defenderInfo.defense = new List<ClashUnitData> ();
		attackerInfo.offense = new List<ClashUnitData> ();
	}

	public string GetPlayerName() {
		return this.player_name;
	}

	public void SetPlayerName(string s) {
		this.player_name = s;
	}

	public int GetPlayerId() {
		return player_id;
	}
	
	public void SetPlayerId(int i) {
		player_id = i;
	}

	public List<ClashUnitData> GetList() {
		if (type == "offense")
			return this.attackerInfo.offense;
		else if (type == "defense")
			return this.defenderInfo.defense;
		else
			return null;
	}

	public void AddToUnitList(string species_name, int species_id, int prefab_id) {
		ClashUnitData ud = new ClashUnitData ();
		ud.species_name = species_name;
		ud.species_id = species_id;
		ud.prefab_id = prefab_id;
		ud.isDeployed = false;

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

	/******************************/

	public string GetDefenderName() {
		return this.defenderInfo.player_name;
	}

	public void SetDefenderName(string s) {
		this.defenderInfo.player_name = s;
	}

	public int GetDefenderId() {
		return this.defenderInfo.player_id;
	}

	public void SetDefenderId(int player_id) {
		this.defenderInfo.player_id = player_id;
	}

	public GameObject GetDefenderTerrain(int terrain_id) {
		return terrain_list[terrain_id];
	}

	public void SetDefenderTerrain(int terrain_id) {
		this.defenderInfo.terrain_id = terrain_id;
	}

	public List<ClashUnitData> GetDefenseTeam(){
		return this.defenderInfo.defense;
	}

	public void ClearDefenderData() {
		this.defenderInfo.player_id = 0;
		this.defenderInfo.player_name = "";
		this.defenderInfo.terrain_id = -1;
		this.defenderInfo.defense.Clear();
	}

	/*****************************/

	public string GetAttackerName() {
		return this.attackerInfo.player_name;
	}
	
	public void SetAttackerName(string s) {
		this.attackerInfo.player_name = s;
	}

	public int GetAttackerId() {
		return this.attackerInfo.player_id;
	}

	public void SetAttackerId(int player_id) {
		this.attackerInfo.player_id = player_id;
	}
	public List<ClashUnitData> GetAttackTeam(){
		return this.attackerInfo.offense;
	}
	public void ClearAttackerData() {
		this.attackerInfo.player_id = 0;
		this.attackerInfo.player_name = "";
		this.attackerInfo.offense.Clear ();
	}

	/******************************/

	public void ClearAllData() {
		defenderInfo = null;
		attackerInfo = null;
		defenderInfo = new ClashDefender ();
		attackerInfo = new ClashAttacker ();
		defenderInfo.defense = defenderInfo.defense ?? new List<ClashUnitData> ();
		attackerInfo.offense = attackerInfo.offense ?? new List<ClashUnitData> ();
	}

}
