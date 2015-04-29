using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class ClashShopElement {
	public string name;
	public int id;
	public int prefab_id;

	public ClashShopElement(string n, int i,  int j) {
		this.name = n;
		this.id = i;
		this.prefab_id = j;
	}
}

public class ClashShopController : MonoBehaviour {
	GameObject required_object;
	ClashPersistentData pd;
	public Transform preview;
	public Transform terrainPanel;
	public Transform carnivorePanel;
	public Transform herbivorePanel;
	public Transform omnivorePanel;
	public Transform plantPanel;
	public Transform selectedUnits;
	public Transform selectedTerrain;
	public GameObject terrainTab;
	public GameObject shopElementPrefab;
	public GameObject selectedUnitPrefab;
	public GameObject selectedTerrainPrefab;

	void Awake() {

		required_object = GameObject.Find ("Persistent Object");
		
		if (required_object == null) {
			Application.LoadLevel ("ClashSplash");
		}
	}

	// Use this for initialization
	void Start () {
		pd = required_object.GetComponent<ClashPersistentData> ();
		if (pd.type == "defense") {
			pd.SetDefenderName (pd.GetPlayerName ());
			pd.SetDefenderId (pd.GetPlayerId ());
			PopulateTerrainPanel ();
		} else {
			terrainTab.SetActive(false);
			selectedTerrain.gameObject.SetActive(false);
		}

		PopulateUnitPanel ();
	}

	// Update is called once per frame
	void Update () {
	
	}

	/*
	 * Protocol implements this to get the list from server
	 * */

	void PopulateUnitPanel() {
		Transform panel;
		foreach (var species in pd.species_list) {
			GameObject element = Instantiate(shopElementPrefab) as GameObject;
			ClashShopElementPrefab e = element.GetComponent<ClashShopElementPrefab>();
			e.label.text = species.species_name;	//species name for display
			e.item_name = species.species_name;			//species name
			e.species_id = species.species_id;				//species id
			e.prefab_id = species.prefab_id;		//prefab id
			e.attack = species.attack;
			e.attack_speed = species.attack_speed;
			e.cost = species.cost;
			e.description = species.description;
			e.hp = species.hp;
			e.movement_speed = species.movement_speed;
			e.image.texture = Resources.Load("Images/" + species.species_name) as Texture;						//set sprite
			e.preview.onClick.AddListener(() => DisplayPreview(e));
			e.add.onClick.AddListener(() => AddToSelectedUnits(e));
			switch(e.prefab_id) {
			case 0:
				panel = plantPanel;
				break;
			case 1:
				panel = carnivorePanel;
				break;
			case 2:
				panel = herbivorePanel;
				break;
			case 3:
				panel = omnivorePanel;
				break;
			default:
				panel = null;
				break;
			}
			if(panel == null) Destroy(element);
			else element.transform.SetParent(panel);
		}
	}

	void PopulateTerrainPanel() {
		int id = 0;
		foreach (var go in pd.terrain_list) {
			GameObject element = Instantiate(shopElementPrefab) as GameObject;
			ClashShopElementPrefab e = element.GetComponent<ClashShopElementPrefab>();
			e.label.text = go.name;	//terrain name for display
			e.item_name = go.name;		//terrain name
			e.prefab_id = id;		//prefab id
			e.image.texture = Resources.Load("Images/ClashOfSpecies/" + go.name) as Texture;						//set sprite
			e.add.onClick.AddListener(() => AddToSelectedTerrain(e));
			e.preview.onClick.AddListener(() => DisplayPreview(e));
			element.transform.SetParent(terrainPanel);
			id++;
		}
	}

	void AddToSelectedUnits(ClashShopElementPrefab se) {
		if (selectedUnits.childCount < 5 && !CheckIfUnitExists(se.species_id)) {
			GameObject selected = Instantiate (selectedUnitPrefab) as GameObject;
			ClashSelectedUnit e = selected.GetComponent<ClashSelectedUnit> ();
			e.label.text = se.label.text;	//species name
			e.prefab_id = se.prefab_id;		//prefab name
			e.species_id = se.species_id;					//species id
			e.image.texture = se.image.texture;
			e.remove.onClick.AddListener (() => RemoveFromSelected (selected));
			selected.transform.SetParent (selectedUnits);
		} else {
			DisplayErrorMessage("Only 5 units can be chosen");
		}
	}

	void AddToSelectedTerrain(ClashShopElementPrefab se) {
		if (selectedTerrain.childCount < 1) {
			GameObject selected = Instantiate (selectedTerrainPrefab) as GameObject;
			ClashSelectedTerrain e = selected.GetComponent<ClashSelectedTerrain> ();
			e.label.text = se.label.text;
			e.terrain_id = se.prefab_id;
			e.image.texture = se.image.texture;
			e.remove.onClick.AddListener (() => RemoveFromSelected (selected));
			selected.transform.SetParent (selectedTerrain);
		} else {
			DisplayErrorMessage("Only 1 terrain can be chosen");
		}
	}

	bool CheckIfUnitExists(int unit_id) {
		foreach (Transform child in selectedUnits) {
			if(child.gameObject.GetComponent<ClashSelectedUnit>().species_id == unit_id) {
				return true;
			}
		}
		return false;
	}

	void DisplayPreview(ClashShopElementPrefab se) {
		preview.Find ("Image").GetComponent<RawImage> ().texture = se.image.texture;
		preview.Find ("Text").GetComponent<Text> ().text = se.description;
	}

	void DisplayErrorMessage(string s) {

	}

	void RemoveFromSelected(GameObject se) {
		Destroy (se);
	}
}
