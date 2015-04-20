using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class ShopElement {
	public string name;
	public int id;
	public string prefab;
}

public class ShopController : MonoBehaviour {
	GameObject required_object;
	PersistentData pd;
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
	public List<ShopElement> terrainList;
	public List<ShopElement> carnivoreList;
	public List<ShopElement> herbivoreList;
	public List<ShopElement> omnivoreList;
	public List<ShopElement> plantList;

	void Awake() {
		/*	//------Protocol gets the list from server------
		terrainList = RetrieveList ();
		carnivoreList = RetrieveList ();
		herbivoreList = RetrieveList ();
		omnivoreList = RetrieveList ();
		plantList = RetrieveList ();
		*/
	}

	// Use this for initialization
	void Start () {
		required_object = GameObject.Find ("Persistent Object");
		/*
		if (required_object == null) {
			Application.LoadLevel ("ClashSplash");
		}
		pd = required_object.GetComponent<PersistentData> ();
		if (pd.GetSceneType() == "defense") {
			pd.SetDefenderName (pd.GetPlayerName ());
			pd.SetDefenderId (pd.GetPlayerId ());*/
			PopulateTerrainPanel (terrainList, terrainPanel);
		/*} else {
			terrainTab.SetActive(false);
			selectedTerrain.gameObject.setActive(false);
		}*/
		PopulateUnitPanel (carnivoreList, carnivorePanel);
		PopulateUnitPanel (herbivoreList, herbivorePanel);
		PopulateUnitPanel (omnivoreList, omnivorePanel);
		PopulateUnitPanel (plantList, plantPanel);
	}

	// Update is called once per frame
	void Update () {
	
	}

	/*
	 * Protocol implements this to get the list from server
	 * */
	List<ShopElement> RetrieveList() {
		return new List<ShopElement>();
	}

	void PopulateUnitPanel(List<ShopElement> list, Transform panel) {
		foreach (var shopElement in list) {
			GameObject element = Instantiate(shopElementPrefab) as GameObject;
			ShopElementPrefab e = element.GetComponent<ShopElementPrefab>();
			e.label.text = shopElement.name;	//species name for display
			e.name = shopElement.name;			//species name
			e.id = shopElement.id;				//species id
			e.prefab = shopElement.prefab;		//prefab name
			//e.image = ;						//set sprite
			e.preview.onClick.AddListener(() => DisplayPreview(e));
			e.add.onClick.AddListener(() => AddToSelectedUnits(e));
			element.transform.SetParent(panel);
		}
	}

	void PopulateTerrainPanel(List<ShopElement> list, Transform panel) {
		foreach (var shopElement in list) {
			GameObject element = Instantiate(shopElementPrefab) as GameObject;
			ShopElementPrefab e = element.GetComponent<ShopElementPrefab>();
			e.label.text = shopElement.name;	//terrain name for display
			e.item_name = shopElement.name;		//terrain name
			e.id = shopElement.id;				//terrain id
			e.prefab = shopElement.prefab;		//prefab name
			//e.image = ;						//set sprite
			e.add.onClick.AddListener(() => AddToSelectedTerrain(e));
			element.transform.SetParent(panel);
		}
	}

	void AddToSelectedUnits(ShopElementPrefab se) {
		if (selectedUnits.childCount <= 5) {
			GameObject selected = Instantiate (selectedUnitPrefab) as GameObject;
			SelectedUnit e = selected.GetComponent<SelectedUnit> ();
			e.label.text = se.label.text;	//species name
			e.prefab_name = se.prefab;		//prefab name
			e.id = se.id;					//species id
			//e.image =;
			e.input.characterValidation = InputField.CharacterValidation.Integer;
			//e.input.onValidateInput();
			e.remove.onClick.AddListener (() => RemoveFromSelected (selected));
			selected.transform.SetParent (selectedUnits);
		} else {
			DisplayErrorMessage("Only 5 units can be chosen");
		}
	}

	void AddToSelectedTerrain(ShopElementPrefab se) {
		if (selectedTerrain.childCount <= 1) {
			GameObject selected = Instantiate (selectedTerrainPrefab) as GameObject;
			SelectedTerrain e = selected.GetComponent<SelectedTerrain> ();
			e.label.text = se.label.text;
			e.prefab_name = se.prefab;
			e.id = se.id;
			//e.image = ;
			e.remove.onClick.AddListener (() => RemoveFromSelected (selected));
			selected.transform.SetParent (selectedTerrain);
		} else {
			DisplayErrorMessage("Only 1 terrain can be chosen");
		}
	}

	void DisplayPreview(ShopElementPrefab se) {

	}

	void DisplayErrorMessage(string s) {

	}

	void RemoveFromSelected(GameObject se) {
		Destroy (se);
	}
}
