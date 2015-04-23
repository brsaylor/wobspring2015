using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ClashBattleController : MonoBehaviour {
	GameObject required_object;

	ClashPersistentData data;
	List<Transform> enemyList;
	List<Transform> allyList;

	GameObject t;
	string terrain_prefab;
	public GameObject terrain;

	// Use this for initialization
	void Start () {
		required_object = GameObject.Find ("Persistent Object");

		if (required_object != null) {
			data = required_object.GetComponent ("AttackingData") as ClashPersistentData;


		}
		//data = required_object.GetComponent<PersistentData> ();
		//enemyList = data.GetDefenseTeam ();
		//allyList = data.GetAttackTeam ();
		SpawnTe ("Te The DryLands");//data.GetDefenderTerrain ());
	}
	
	// Update is called once per frame
	void Update () {
		//if (InvokeRepeating (IsGameOver (), 2, 5)) {

		//}
	}

	GameObject CreateUnit() {

		GameObject go = null ;
		return null;
	}

	public bool IsEnemyDefeated() {
		List<GameObject> enemies = new List<GameObject> ();

		GameObject[] animals = GameObject.FindGameObjectsWithTag ("EnemyAnimal");
		foreach (GameObject enemy in animals)
			enemies.Add (enemy);

		GameObject[] plants = GameObject.FindGameObjectsWithTag ("EnemyPlant");
		foreach (GameObject enemy in plants)
			enemies.Add (enemy);

		return(enemies.Count == 0);

	}

	public bool IsAllUnitsDeployed() {
		foreach (ClashUnitData ud in data.attackerInfo.offense) {
			if (!ud.isDeployed) {
				return false;
			}
		}
		return true;
	}

	public bool IsAllyDefeated() {
		List<GameObject> allies = new List<GameObject> ();
		
		GameObject[] animals = GameObject.FindGameObjectsWithTag ("AllyAnimal");
		foreach (GameObject enemy in animals)
			allies.Add (enemy);
		
		GameObject[] plants = GameObject.FindGameObjectsWithTag ("AllyPlant");
		foreach (GameObject enemy in plants)
			allies.Add (enemy);
		
		return(allies.Count == 0);
	}

	public bool IsGameOver() {
		return((IsAllUnitsDeployed () && IsAllyDefeated()) || IsEnemyDefeated ());
	}

	public void AddAllAllies() {

	}

	public void InstantiateEnemyUnits() {

		foreach (ClashUnitData ud in data.defenderInfo.defense) {

			Instantiate(Resources.Load("Prefabs/ClashOfSpecies/Unit/"+ud.prefabName,typeof(GameObject)), ud.location, Quaternion.identity);

		}
	}
	void SpawnTe ( string p)
	{
		
		Instantiate (Resources.Load ("Prefabs/ClashOfSpecies/Terrains/" + p, typeof(GameObject)), new Vector3 (0, 0, 0), Quaternion.identity);
		
	}



}
