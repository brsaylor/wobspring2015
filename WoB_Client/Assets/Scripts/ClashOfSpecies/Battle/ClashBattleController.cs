using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ClashBattleController : MonoBehaviour {
	GameObject required_object;
	ClashPersistentData data;
	GameObject t, go;
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

			//Application.LoadLevel ("ClashSplash");
			
		}
	}
	// Use this for initialization
	void Start () {

			data = required_object.GetComponent<ClashPersistentData>();
			SpawnTe (data.terrain_list[data.defenderInfo.terrain_id]);
			//InstantiateEnemyUnits ();
			//button2 = (Texture)Resources.Load("Images/African Elephant");

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
			if(ud.species_id == 1) 
				go = Instantiate(Resources.Load("Prefabs/3dModels/Setted models/CARNIVORE_Prefab",typeof(GameObject)), ud.location, Quaternion.identity);
			else if(ud.species_id == 2) 
				go = Instantiate(Resources.Load("Prefabs/3dModels/Setted models/HERBIVORE_Prefab",typeof(GameObject)), ud.location, Quaternion.identity);
			else if(ud.species_id == 3) 
				go = Instantiate(Resources.Load("Prefabs/3dModels/Setted models/OMNIVORE_Prefab",typeof(GameObject)), ud.location, Quaternion.identity);
			else if(ud.species_id == 0) 
				go = Instantiate(Resources.Load("Prefabs/3dModels/Setted models/PLANT_Prefab",typeof(GameObject)), ud.location, Quaternion.identity);

			go.GetComponent<Animator>().runtimeAnimatorController = Resources.Load("Animations/" + ud.species_name + "Animator") as RuntimeAnimatorController;
			go.GetComponent<Animator>().avatar = Resources.Load("Prefabs/ClashOfSpecies/3D Animal Prefabs" + ud.species_name + "Animator")as RuntimeAnimatorController;

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
		
	}

	

}
