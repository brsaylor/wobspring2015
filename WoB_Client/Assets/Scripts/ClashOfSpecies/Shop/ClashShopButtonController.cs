using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ClashShopButtonController : MonoBehaviour {
	GameObject required_object;
	ClashPersistentUserData pd;
	public Button cancel;
	public Text cancelLabel;
	public Button accept;
	public Text acceptLabel;
	public Transform selectedUnits;
	public Transform selectedTerrain;

	// Use this for initialization
	void Start () {
		required_object = GameObject.Find ("Persistent Object");
		/*	//ShopController.cs handles this; not needed here
		if (required_object == null) {
			Application.LoadLevel ("ClashSplash");
		}
		*/
		pd = required_object.GetComponent<ClashPersistentUserData> ();

		if (pd.type == "defense") {
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
		if (pd.type == "defense") {
			if (selectedUnits.childCount > 0 && selectedTerrain.childCount > 0) {
				foreach (Transform child in selectedUnits) {
					ClashSelectedUnit su = child.gameObject.GetComponent<ClashSelectedUnit> ();
					//pd.AddToUnitList (species_name, species_id, prefabName);
					pd.AddToUnitList (su.label.text, su.species_id, su.prefab_id);
				}
				foreach (Transform child in selectedTerrain) {
					ClashSelectedTerrain st = child.gameObject.GetComponent<ClashSelectedTerrain> ();
					pd.SetDefenderTerrain (st.terrain_id);
				}
				Application.LoadLevel ("ClashDefense");
			} else
				Debug.Log("Need at least one unit and terrain to move on");
		} else if (pd.type == "offense") {
			if (selectedUnits.childCount > 0) {
				foreach (Transform child in selectedUnits) {
					ClashSelectedUnit su = child.gameObject.GetComponent<ClashSelectedUnit> ();
					pd.AddToUnitList (su.label.text, su.species_id, su.prefab_id);
				}
				Application.LoadLevel ("ClashBattle");
			}
		}
	}
}
