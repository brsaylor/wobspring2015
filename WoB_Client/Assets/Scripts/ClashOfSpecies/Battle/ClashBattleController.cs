using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ClashBattleController : MonoBehaviour {
	GameObject required_object;
	ClashPersistentData data;
	GameObject t, goo;
	string terrain_prefab;
	public GameObject terrain;
	public GameObject Model,Model2,Model3,Model4,Model5;
	public float xMin = 30F;
	public float xMax = 70F;
	public float zMin = 40F;
	public float zMax = 60F;
	//public  button1;
	//public Button button2;

	void Awake() {
		required_object = GameObject.Find ("Persistent Object");
		if (required_object == null) {

			Application.LoadLevel ("ClashSplash");
			
		}
	}
	// Use this for initialization
	void Start () {

			data = required_object.GetComponent<ClashPersistentData>();
			SpawnTe (data.terrain_list[data.defenderInfo.terrain_id]);
			//InstantiateEnemyUnits ();
			//button2 = (Texture)Resources.Load("Images/African Elephant");
		//SpawnTe ("The DryLands");
		//InstantiateEnemyUnits();
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

		switch (ud.species_id)
			{

			case 58: Instantiate(Resources.Load("Prefabs/3dModels/Setted models/caracal_Prefab",typeof(GameObject)), ud.location, Quaternion.identity);break;
			case 11: Instantiate(Resources.Load("Prefabs/3dModels/Setted models/ant_prefab",typeof(GameObject)), ud.location, Quaternion.identity);break;
			case 1001: Instantiate(Resources.Load("Prefabs/3dModels/Setted models/bush1_Prefab",typeof(GameObject)), ud.location, Quaternion.identity);break;
			case 1009: Instantiate(Resources.Load("Prefabs/3dModels/Setted models/bush2_prefab",typeof(GameObject)), ud.location, Quaternion.identity);break;
			case 1008: Instantiate(Resources.Load("Prefabs/3dModels/Setted models/bush3_prefab",typeof(GameObject)), ud.location, Quaternion.identity);break;
			case 55: Instantiate(Resources.Load("Prefabs/3dModels/Setted models/bushbaby_prefab",typeof(GameObject)), ud.location, Quaternion.identity);break;
			case 59: Instantiate(Resources.Load("Prefabs/3dModels/Setted models/elephant_Prefab",typeof(GameObject)), ud.location, Quaternion.identity);break;
			case 4: Instantiate(Resources.Load("Prefabs/3dModels/Setted models/fox_Prefab",typeof(GameObject)), ud.location, Quaternion.identity);break;
			case 61: Instantiate(Resources.Load("Prefabs/3dModels/Setted models/tortoise_prefab",typeof(GameObject)), ud.location, Quaternion.identity);break;
			case 7: Instantiate(Resources.Load("Prefabs/3dModels/Setted models/wildbeast_Prefab",typeof(GameObject)), ud.location, Quaternion.identity);break;
			case 75: Instantiate(Resources.Load("Prefabs/3dModels/Setted models/zebra_Prefab",typeof(GameObject)), ud.location, Quaternion.identity);break;
			case 86: Instantiate(Resources.Load("Prefabs/3dModels/Setted models/cheetah_Prefab",typeof(GameObject)), ud.location, Quaternion.identity);break;
			default: break; 
			}
		}
		//hardcode of instantiate defense team
	/*
		Vector3 newPos = new Vector3(Random.Range(xMin, xMax), 1, Random.Range(zMin, zMax));
		Vector3 newPos2 = new Vector3(Random.Range(xMin, xMax), 1, Random.Range(zMin, zMax));
		Vector3 newPos3 = new Vector3(Random.Range(xMin, xMax), 1, Random.Range(zMin, zMax));
		Vector3 newPos4 = new Vector3(Random.Range(xMin, xMax), 1, Random.Range(zMin, zMax));
		Vector3 newPos5 = new Vector3(Random.Range(xMin, xMax), 1, Random.Range(zMin, zMax));
		Instantiate (Model, newPos, Quaternion.identity);
		Instantiate (Model2, newPos2, Quaternion.identity);
		Instantiate (Model3, newPos3, Quaternion.identity);
		Instantiate (Model4, newPos4, Quaternion.identity);
		Instantiate (Model5, newPos5, Quaternion.identity);
	*/
	} 

	void SpawnTe (GameObject go)
	{
		
		Instantiate (go, new Vector3 (0, 0, 0), Quaternion.identity);
		//Instantiate(Resources.Load("Prefabs/ClashOfSpecies/Terrains/"), new Vector3 (0, 0, 0), Quaternion.identity);
		
	}

	

}
