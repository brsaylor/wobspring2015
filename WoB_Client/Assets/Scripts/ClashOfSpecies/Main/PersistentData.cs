using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class UnitData {
	public string name;
	public string type;
	public int hp;
}

[System.Serializable]
public class Defender {
	public string name;
	public string terrain;
	
}

[System.Serializable]
public class Attacker {
	public string name;
}

public class PersistentData : MonoBehaviour {

	public Defender defenderInfo;
	public Attacker attackerInfo;

	void Awake() {
		DontDestroyOnLoad (this);
	}
	
	void Start () {
		//set the user_id or username
		//attackerInfo.Name = ;
	}

	void Update () {
	
	}

	public string getDefenderName() {
		return this.defenderInfo.name;
	}

	public string getDefenderTerrain() {
		return this.defenderInfo.terrain;
	}

	public void SetDefenderData(string name, string terrain) {
		this.defenderInfo.name = name;
		this.defenderInfo.terrain = terrain;
	}

}
