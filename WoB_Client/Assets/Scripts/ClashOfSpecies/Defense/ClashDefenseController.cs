using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ClashDefenseController : MonoBehaviour {
	GameObject required_object;
	public Transform unit_display;
	public ToggleGroup toggleGroup = null;
	public GameObject unit_display_toggle;

	void Awake() {}

	// Use this for initialization
	void Start () {
        /*

		pd = required_object.GetComponent<ClashPersistentData> ();
		toggleGroup = unit_display.GetComponent<ToggleGroup>();

		Instantiate (pd.terrain_list[pd.defenderInfo.terrain_id], new Vector3(0,0,0), Quaternion.identity);

		PopulateUnitDisplay ();
        */
	}
	
	// Update is called once per frame
	void Update () {}

	void PopulateUnitDisplay() {
        /*
		int i = 0;
		foreach (ClashUnitData ud in pd.defenderInfo.defense) {
			GameObject element = Instantiate(unit_display_toggle) as GameObject;
			ClashDefenseToggle cdt = element.GetComponent<ClashDefenseToggle>();
			cdt.list_index = i;
			//cdt.unit_image = ;
			cdt.toggle.group = toggleGroup;
			cdt.transform.SetParent(unit_display);
			i++;
		}
        */
	}

	public void ReturnToShop() {
		Application.LoadLevel ("ClashShop");
	}

	public void ConfirmDefense() {
        /*
		bool allDeployed = true;
		foreach (ClashUnitData cud in pd.defenderInfo.defense) {
			allDeployed = (cud.isDeployed) ? allDeployed : false;
		}

		if (allDeployed) {
			SendDefenseToServer ();

			Application.LoadLevel ("ClashMain");
		}
        */
	}

	public void SendDefenseToServer() {
        /*
		Terrain t = GameObject.FindGameObjectWithTag ("Terrain").GetComponent<Terrain>();
		float terrainX = t.terrainData.size.x;
		float terrainZ = t.terrainData.size.z;
		Dictionary<int, Vector2> species_loc = new Dictionary<int, Vector2> ();

		foreach(GameObject go in GameObject.FindGameObjectsWithTag("Ally")) {
			species_loc.Add(go.GetComponent<ClashUnitAttributes>().species_id, new Vector2(go.transform.position.x/terrainX, go.transform.position.z/terrainZ));
		}

		NetworkManager.Send(ClashDefenseSetupProtocol.Prepare(pd.defenderInfo.terrain_id, species_loc), (res) => {
			var response = res as ResponseClashDefenseSetup;
			Debug.Log("Valid Defense" + response.valid);
		});
        */
	}

}
