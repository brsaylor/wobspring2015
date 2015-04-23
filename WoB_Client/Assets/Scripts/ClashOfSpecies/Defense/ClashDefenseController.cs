using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ClashDefenseController : MonoBehaviour {
	GameObject required_object;
	ClashPersistentData pd;
	string terrain_prefab;
	List<ClashUnitData> units;
	public Transform unit_display;
	public GameObject unit_display_button;

	void Awake() {
		required_object = GameObject.Find ("Persistent Object");
		
		if (required_object == null) {
			Application.LoadLevel ("ClashSplash");
		}
	}

	// Use this for initialization
	void Start () {
		pd = required_object.GetComponent<ClashPersistentData> () as ClashPersistentData;
		terrain_prefab = pd.defenderInfo.terrain;
		units = pd.defenderInfo.defense;

		Instantiate (Resources.Load ("Prefabs\\ClashOfSpecies\\Terrain\\" + terrain_prefab, typeof(GameObject)));

		PopulateUnitDisplay ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void PopulateUnitDisplay() {
		foreach (ClashUnitData ud in units) {
			GameObject element = Instantiate(unit_display_button) as GameObject;
			ClashUnitButton cub = element.GetComponent<ClashUnitButton>();
			//cub.unit_image = ;
			cub.self.onClick.AddListener(() => SelectUnit(cub));
			cub.transform.SetParent(unit_display);
		}
	}

	void SelectUnit(ClashUnitButton cub) {
		cub.self.interactable = false;
	}
}
