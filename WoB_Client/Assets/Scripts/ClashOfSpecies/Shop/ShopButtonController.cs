using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ShopButtonController : MonoBehaviour {
	GameObject required_object;
	PersistentData pd;
	public Button cancel;
	public Text cancelLabel;
	public Button accept;
	public Text acceptLabel;
	public Transform selectedUnits;
	public Transform selectedTerrain;

	// Use this for initialization
	void Start () {
		required_object = GameObject.Find ("Persistent Object");
		pd = required_object.GetComponent<PersistentData> ();
		/*	//ShopController.cs handles this; not needed here
		if (required_object == null) {
			Application.LoadLevel ("ClashSplash");
		}
		*/
	
		if (1 == 0 /*pd.GetSceneType () == "defense"*/) {
			cancelLabel.text = "Return to Lobby\n(Cancel)";
			cancel.onClick.AddListener (() => BackToLobby ());
			acceptLabel.text = "Place Defense\n(Continue)";
			accept.onClick.AddListener (() => GoToClashBattle ());
		} else {
			cancelLabel.text = "Return to Main\n(Cancel)";
			cancel.onClick.AddListener (() => BackToMain ());
			acceptLabel.text = "Engage\n(Continue)";
			accept.onClick.AddListener (() => GoToClashBattle ());
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void BackToLobby() {
		//Destroy (required_object);		//destroy the persisent object
		//Application.LoadLevel ("Lobby"); //Go back to lobby
	}

	void BackToMain() {
		Application.LoadLevel ("ClashMain");
	}

	void GoToClashBattle() {
		//get all children gameObject in Canvas->SelectedUnits
		//put the data from those children gameObjects into the persistent data
		if (pd.GetSceneType () == "defense") {
			if (selectedUnits.childCount > 0) {
				foreach (Transform child in selectedUnits) {
					SelectedUnit su = child.gameObject.GetComponent<SelectedUnit> ();
					//pd.AddToUnitList (species_name, species_id, prefabName, hp);
					int hp;
					int.TryParse(su.input.text, out hp);
					pd.AddToUnitList (su.label.text, su.id, su.prefab_name, hp);
				}
			} 
			if (selectedTerrain.childCount > 0) {
				foreach (Transform child in selectedTerrain) {
					SelectedTerrain st = child.gameObject.GetComponent<SelectedTerrain> ();
					pd.SetDefenderTerrain (st.prefab_name);
				}
			}
			if(pd.GetTeamSize() > 0 && pd.GetDefenderTerrain() != "")
				Application.LoadLevel ("ClashDefense");
		} else if (pd.GetSceneType () == "offense") {
			if (selectedUnits.childCount > 0) {
				foreach (Transform child in selectedUnits) {
					SelectedUnit su = child.gameObject.GetComponent<SelectedUnit> ();
					int hp;
					int.TryParse(su.input.text, out hp);
					//pd.AddToUnitList (species_name, species_id, prefabName, hp);
					pd.AddToUnitList (su.label.text, su.id, su.prefab_name, hp);
				}
			}
			if(pd.GetTeamSize() > 0 && pd.GetDefenderTerrain() != "")
				Application.LoadLevel ("ClashBattle");
		}
	}
}
