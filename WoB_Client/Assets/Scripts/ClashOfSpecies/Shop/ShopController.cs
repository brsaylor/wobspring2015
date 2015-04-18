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
	public Transform terrainPanel;
	public Transform carnivorePanel;
	public Transform herbivorePanel;
	public Transform omnivorePanel;
	public Transform plantPanel;
	public Transform selectedUnits;
	public GameObject shopElementPrefab;
	public GameObject selectedElementPrefab;
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
		/*required_object = GameObject.Find ("Persistent Object");
		if (required_object == null) {
			Application.LoadLevel ("ClashSplash");
		}*/
		PopulatePanel (terrainList, terrainPanel);
		PopulatePanel (carnivoreList, carnivorePanel);
		PopulatePanel (herbivoreList, herbivorePanel);
		PopulatePanel (omnivoreList, omnivorePanel);
		PopulatePanel (plantList, plantPanel);
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

	void PopulatePanel(List<ShopElement> list, Transform panel) {
		foreach (var shopElement in list) {
			GameObject element = Instantiate(shopElementPrefab) as GameObject;
			ShopElementPrefab e = element.GetComponent<ShopElementPrefab>();
			e.label.text = shopElement.name;	//species name for display
			e.name = shopElement.name;			//species name
			e.id = shopElement.id;				//species id
			e.prefab = shopElement.prefab;
			//e.image = ;						//set sprite
			e.add.onClick.AddListener(() => AddToSelected(e));
			element.transform.SetParent(panel);
		}
	}

	void AddToSelected(ShopElementPrefab se) {
		GameObject selected = Instantiate (selectedElementPrefab) as GameObject;
		SelectedElement e = selected.GetComponent<SelectedElement> ();
		e.label.text = se.label.text;
		e.name = se.name;
		e.id = se.id;
		e.remove.onClick.AddListener (() => RemoveFromSelected (selected));
		selected.transform.SetParent (selectedUnits);
	}

	void RemoveFromSelected(GameObject se) {
		Destroy (se);
	}
}
