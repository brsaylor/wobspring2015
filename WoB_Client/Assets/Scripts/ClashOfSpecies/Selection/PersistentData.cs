using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PersistentData : MonoBehaviour {
	private class UnitData {
		public string Name { get; set; }
		public string Type { get; set; }
		public int Hp { get; set; }
	}

	private class Defender {
		public string Name { get; set; }
		public string Terrain { get; set; }

	}

	private class Attacker {
		public string Name {get; set; } 
	}

	Defender defenderInfo;
	Attacker attackerInfo;

	void Awake() {
		DontDestroyOnLoad (this);
	}

	// Use this for initialization
	void Start () {
		//set the user_id or username
		//attackerInfo.Name = ;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	/*
	public bool addUnit(string name) {
		if (unitNames.Count == 5) {
			print ("Only 5 unique species allowed");
			return false;
		}
		unitNames.Add (name);
		return true;
	}*/

}
