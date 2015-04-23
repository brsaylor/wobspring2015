using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;


public class ClashDefenseSpawner : MonoBehaviour {
	public ClashDefenseController cdc;
	GameObject required_object;
	ClashPersistentUserData pd;

	void Awake() {
		required_object = GameObject.Find ("Persistent Object");
		
		if (required_object == null) {
			Application.LoadLevel ("ClashSplash");
		}
	}
	
	// Use this for initialization
	void Start () {
		pd = required_object.GetComponent<ClashPersistentUserData> () as ClashPersistentUserData;
		//Debug.Log (EventSystem.current.IsPointerOverGameObject ());
	}

	
	// Update is called once per frame
	void Update () {
		SpawnInvader ();
	}

    private void SpawnInvader() {
		if (Input.GetButtonDown("Fire1") && !EventSystem.current.IsPointerOverGameObject()) {	//mouse 1 pressed
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if (Physics.Raycast(ray, out hit, 1000.0f) && hit.collider.gameObject.tag == "Terrain") {
				int list_index = cdc.toggleGroup.GetActiveToggle().GetComponent<ClashDefenseToggle>().list_index;
				bool isDeployed = pd.defenderInfo.defense[list_index].isDeployed;
				if(!isDeployed) { 
					if(pd.defenderInfo.defense[list_index].prefab_id == 0) {
						Instantiate(Resources.Load ("Prefabs/ClashOfSpecies/Unit/unit1", typeof(GameObject)), hit.point, Quaternion.identity);
						pd.defenderInfo.defense[list_index].isDeployed = true;
						cdc.toggleGroup.GetActiveToggle().GetComponent<ClashDefenseToggle>().toggle.interactable = false;
					}
				}
            }
        }
    }
}