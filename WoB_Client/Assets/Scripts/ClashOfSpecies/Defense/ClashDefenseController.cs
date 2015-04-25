using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ClashDefenseController : MonoBehaviour {
	GameObject required_object;
	ClashPersistentData pd;
	string terrain_prefab;
	public Transform unit_display;
	public ToggleGroup toggleGroup = null;
	public GameObject unit_display_toggle;
	ClashDefenseToggle selected;

	void Awake() {
		required_object = GameObject.Find ("Persistent Object");
		
		if (required_object == null) {
			Application.LoadLevel ("ClashSplash");
		}
	}

	// Use this for initialization
	void Start () {

		pd = required_object.GetComponent<ClashPersistentData> ();
		toggleGroup = unit_display.GetComponent<ToggleGroup>();

		Instantiate (pd.terrain_list[pd.defenderInfo.terrain_id], new Vector3(0,0,0), Quaternion.identity);

		PopulateUnitDisplay ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void PopulateUnitDisplay() {
		int i = 0;
		foreach (ClashUnitData ud in pd.defenderInfo.defense) {
			GameObject element = Instantiate(unit_display_toggle) as GameObject;
			ClashDefenseToggle cdt = element.GetComponent<ClashDefenseToggle>();
			cdt.list_index = i;
			//cdt.unit_image = ;
			//cdt.toggle.onValueChanged.AddListener((value) => SelectUnit(cdt));
			cdt.toggle.group = toggleGroup;
			cdt.transform.SetParent(unit_display);
			i++;
		}
	}

	void SelectUnit(ClashDefenseToggle cdt) {
		selected = cdt;
	}

	public void ReturnToShop() {
		Application.LoadLevel ("ClashShop");
	}

	public void ConfirmDefense() {
		bool allDeployed = true;
		foreach (ClashUnitData cud in pd.defenderInfo.defense) {
			allDeployed = (cud.isDeployed) ? allDeployed : false;
		}
		if (allDeployed) {
			SendDefenseToServer ();
			Application.LoadLevel ("ClashMain");
		}
	}

	public void SendDefenseToServer() {

	}

}
