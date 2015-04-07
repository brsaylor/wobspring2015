using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PersistentData : MonoBehaviour {
	private class UnitData {
		string name;
		string type;
		int hp;
	}

	private class Defender {
		string enemyName;
		string enemyTerrain;

	}

	private class Attacker {

	}

	string enemyName;
	string enemyTerrain;
	ArrayList enemyUnitNames;
	int[] enemyUnitBiomass;
	ArrayList unitNames;
	int[] unitBiomass;

	void Awake() {
		DontDestroyOnLoad (this);
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public bool addUnit(string name) {
		if (unitNames.Count == 5) {
			print ("Only 5 unique species allowed");
			return false;
		}
		unitNames.Add (name);
		return true;
	}

}
