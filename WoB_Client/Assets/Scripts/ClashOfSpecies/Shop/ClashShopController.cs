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
	List<ClashShopElement> carnivoreList;

	void Awake() {
		Screen.SetResolution (800, 600, true);

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
			PopulateTerrainPanel (pd.terrain_list, terrainPanel);
		} else {
			terrainTab.SetActive(false);
			selectedTerrain.gameObject.SetActive(false);
		}

		carnivoreList = new List<ClashShopElement> ();
		carnivoreList.Add (new ClashShopElement ("Unit1", 0, 0));
		PopulateUnitPanel (carnivoreList, carnivorePanel);
		/*
		PopulateUnitPanel (herbivoreList, herbivorePanel);
		PopulateUnitPanel (omnivoreList, omnivorePanel);
		PopulateUnitPanel (plantList, plantPanel);
		*/
	}

	// Update is called once per frame
	void Update () {
	
	}

	/*
	 * Protocol implements this to get the list from server
	 * */
	List<ClashShopElement> RetrieveList() {
		return new List<ClashShopElement>();
	}

	void PopulateUnitPanel(List<ClashShopElement> list, Transform panel) {
		foreach (var shopElement in list) {
			GameObject element = Instantiate(shopElementPrefab) as GameObject;
			ClashShopElementPrefab e = element.GetComponent<ClashShopElementPrefab>();
			e.label.text = shopElement.name;	//species name for display
			e.name = shopElement.name;			//species name
			e.id = shopElement.id;				//species id
			e.prefab_id = shopElement.prefab_id;		//prefab id
			//e.image = ;						//set sprite
			e.preview.onClick.AddListener(() => DisplayPreview(e));
			e.add.onClick.AddListener(() => AddToSelectedUnits(e));
			element.transform.SetParent(panel);
		}
	}

	void PopulateTerrainPanel(List<GameObject> list, Transform panel) {
		int id = 0;
		foreach (var go in list) {
			GameObject element = Instantiate(shopElementPrefab) as GameObject;
			ClashShopElementPrefab e = element.GetComponent<ClashShopElementPrefab>();
			e.label.text = go.name;	//terrain name for display
			e.item_name = go.name;		//terrain name
			e.prefab_id = id;		//prefab id
			//e.image = ;						//set sprite
			e.add.onClick.AddListener(() => AddToSelectedTerrain(e));
			element.transform.SetParent(panel);
			id++;
		}
	}

	void AddToSelectedUnits(ClashShopElementPrefab se) {
		GameObject selected = Instantiate (selectedUnitPrefab) as GameObject;
		ClashSelectedUnit e = selected.GetComponent<ClashSelectedUnit> ();

		if (selectedUnits.childCount < 5 && !CheckIfUnitExists(se.id)) {
			e.label.text = se.label.text;	//species name
			e.prefab_id = se.prefab_id;		//prefab name
			e.species_id = se.id;					//species id
			//e.image =;
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
			//e.image = ;
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

	}

	void DisplayErrorMessage(string s) {

	}

	void RemoveFromSelected(GameObject se) {
		Destroy (se);
	}
}
