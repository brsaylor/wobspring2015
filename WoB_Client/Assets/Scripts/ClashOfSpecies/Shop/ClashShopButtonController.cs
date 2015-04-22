using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ClashShopButtonController : MonoBehaviour {
	GameObject required_object;
	ClashPersistentData pd;
	public Button cancel;
	public Text cancelLabel;
	public Button accept;
	public Text acceptLabel;
	public Transform selectedUnits;
	public Transform selectedTerrain;

	// Use this for initialization
	void Start () {
		required_object = GameObject.Find ("Persistent Object");
		pd = required_object.GetComponent<ClashPersistentData> ();
		/*	//ShopController.cs handles this; not needed here
		if (required_object == null) {
			Application.LoadLevel ("ClashSplash");
		}
		*/
	
		if (pd.GetSceneType () == "defense") {
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
					ClashSelectedUnit su = child.gameObject.GetComponent<ClashSelectedUnit> ();
					//pd.AddToUnitList (species_name, species_id, prefabName, hp);
					int hp;
					int.TryParse(su.input.text, out hp);
					pd.AddToUnitList (su.label.text, su.id, su.prefab_name, hp);
				}
			} 
			if (selectedTerrain.childCount > 0) {
				foreach (Transform child in selectedTerrain) {
					ClashSelectedTerrain st = child.gameObject.GetComponent<ClashSelectedTerrain> ();
					pd.SetDefenderTerrain (st.prefab_name);
				}
			}
			if(pd.GetTeamSize() > 0 && pd.GetDefenderTerrain() != "")
				Application.LoadLevel ("ClashDefense");
			else
				Debug.Log("Need at least one unit and terrain to move on");
		} else if (pd.GetSceneType () == "offense") {
			if (selectedUnits.childCount > 0) {
				foreach (Transform child in selectedUnits) {
					ClashSelectedUnit su = child.gameObject.GetComponent<ClashSelectedUnit> ();
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
