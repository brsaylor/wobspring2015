using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ClashBattleController : MonoBehaviour {
	GameObject required_object, unit;
	ClashPersistentData pd;
	public Transform unit_display;
	//public ToggleGroup toggleGroup = null;
	public GameObject unit_display_toggle;
	Vector3 enemy_loc;

	void Awake() {
		required_object = GameObject.Find ("Persistent Object");
		
		if (required_object == null) {
			Application.LoadLevel ("ClashSplash");
		}
	}

	// Use this for initialization
	void Start () {
		pd = required_object.GetComponent<ClashPersistentData> ();
		//toggleGroup = unit_display.GetComponent<ToggleGroup>();

		Instantiate (pd.terrain_list[pd.defenderInfo.terrain_id], new Vector3(0,0,0), Quaternion.identity);

		PopulateUnitDisplay ();
		SpawnEnemies ();
	}
	
	// Update is called once per frame
	void Update () {

	}

	void PopulateUnitDisplay() {
		int i = 0;
		foreach (ClashUnitData ud in pd.attackerInfo.offense) {
			GameObject element = Instantiate(unit_display_toggle) as GameObject;
			ClashBattleToggle cbt = element.GetComponent<ClashBattleToggle>();
			cbt.list_index = i;
			cbt.unit_image = Resources.Load("Images/" + ud.species_name) as Texture;
			//cbt.toggle.group = toggleGroup;
			cbt.transform.SetParent(unit_display);
			i++;
		}
	}
	
	public bool IsAllUnitsDeployed() {
		foreach (ClashUnitData ud in pd.attackerInfo.offense) {
			if (!ud.isDeployed) {
				return false;
			}
		}
		return true;
	}

	public bool IsEnemyDefeated() {
		GameObject[] enemies = GameObject.FindGameObjectsWithTag ("Enemy");
		
		return(enemies.Length == 0);
	}

	public bool IsAllyDefeated() {
		GameObject[] allies = GameObject.FindGameObjectsWithTag ("Ally");
		
		return(allies.Length == 0);
	}

	public bool IsGameOver() {
		return((IsAllUnitsDeployed () && IsAllyDefeated()) || IsEnemyDefeated ());
	}

	public void SpawnEnemies() {
		foreach (ClashUnitData ud in pd.defenderInfo.defense) {
			//loc = new Vector3(ud.location.x, something, ud.location.y);
			switch(ud.prefab_id) {
			case 0:
				unit = Instantiate(Resources.Load ("Prefabs/ClashOfSpecies/Unit/Plant", typeof(GameObject)), enemy_loc, Quaternion.identity) as GameObject;
				break;
			case 1:
				unit = Instantiate(Resources.Load ("Prefabs/ClashOfSpecies/Unit/Carnivore", typeof(GameObject)), enemy_loc, Quaternion.identity) as GameObject;
				break;
			case 2:
				unit = Instantiate(Resources.Load ("Prefabs/ClashOfSpecies/Unit/Herbivore", typeof(GameObject)), enemy_loc, Quaternion.identity) as GameObject;
				break;
			case 3:
				unit = Instantiate(Resources.Load ("Prefabs/ClashOfSpecies/Unit/Omnivore", typeof(GameObject)), enemy_loc, Quaternion.identity) as GameObject;
				break;
			}
			unit.tag = "Enemy";
			unit.GetComponent<ClashUnitAttributes>().species_id = ud.species_id;
			unit.GetComponent<ClashUnitAttributes>().species_name = ud.species_name;
			unit.GetComponent<ClashUnitAttributes>().prefab_id = ud.prefab_id;
			unit.GetComponent<ClashUnitAttributes>().hp = ud.hp;
			unit.GetComponent<ClashUnitAttributes>().attack = ud.attack;
			unit.GetComponent<ClashUnitAttributes>().attack_speed = ud.attack_speed;
			unit.GetComponent<ClashUnitAttributes>().movement_speed = ud.movement_speed;
			ud.isDeployed = true;

		}
	}
}
