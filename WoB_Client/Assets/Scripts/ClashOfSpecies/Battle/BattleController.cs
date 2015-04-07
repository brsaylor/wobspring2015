using UnityEngine;
using System.Collections;

public class BattleController : MonoBehaviour {
	PersistentData attackingData;

	// Use this for initialization
	void Start () {
		attackingData = GameObject.Find ("PersistentObject").GetComponent("AttackingData") as PersistentData;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public GameObject searchForEnemy(string unitAttribute) {
		return null;
	}
}
