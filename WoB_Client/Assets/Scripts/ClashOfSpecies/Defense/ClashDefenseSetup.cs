using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ClashDefenseSetup : MonoBehaviour {
    private ClashGameManager manager;
    private ClashSpecies selected;
    private Terrain terrain;

    public HorizontalLayoutGroup unitList;
    public GameObject defenseItemPrefab;
    public GameObject defenseUnitPrefab;

	void Awake() {
        manager = GameObject.Find("Global Object").GetComponent<ClashGameManager>();
    }
    
	// Use this for initialization
	void Start () {
        terrain = (Instantiate(Resources.Load<Terrain>("Terrains/" + manager.pendingDefenseConfig.terrain)) as GameObject).GetComponent<Terrain>();
        foreach (var species in manager.pendingDefenseConfig.layout.Keys) {
            var item = Instantiate(defenseUnitPrefab) as GameObject;
            item.GetComponentInChildren<Image>().sprite = Resources.Load<Sprite>("Images/" + species.name);
            item.GetComponentInChildren<Toggle>().onValueChanged.AddListener((val) => {
                if (val) {
                    selected = species;
                    item.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 0.5f);
                } else {
                    selected = null;
                    item.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
                }
            });
        }
	}

    void OnMouseDown() {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (terrain.GetComponent<TerrainCollider>().Raycast(ray, out hit, 100000)) {
            if (selected != null) {
                Instantiate(defenseUnitPrefab, hit.point, Quaternion.identity);
            }
        }
    }

	// Update is called once per frame
	void Update () {}

	public void ReturnToShop() {
		Application.LoadLevel("ClashShop");
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
