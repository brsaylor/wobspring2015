using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class ClashDefenseSetup : MonoBehaviour {

    private ClashGameManager manager;
    private ClashSpecies selected;
    private Terrain terrain;
    private ToggleGroup toggleGroup;

    public HorizontalLayoutGroup unitList;
    public GameObject defenseItemPrefab;
    public GameObject defenseUnitPrefab;

	public GameObject errorCanvas;
	public Text errorMessage;

	void Awake() {
        manager = GameObject.Find("MainObject").GetComponent<ClashGameManager>();
		toggleGroup = unitList.GetComponent<ToggleGroup>();
    }
    
	// Use this for initialization
	void Start () {
        var terrainObject = Resources.Load<GameObject>("Prefabs/ClashOfSpecies/Terrains/" + manager.pendingDefenseConfig.terrain);
        terrain = (Instantiate(terrainObject, Vector3.zero, Quaternion.identity) as GameObject).GetComponent<Terrain>();
        terrain.transform.position = Vector3.zero;
        terrain.transform.localScale = Vector3.one;

        foreach (var species in manager.pendingDefenseConfig.layout.Keys) {
			var currentSpecies = species;
            var item = Instantiate(defenseItemPrefab) as GameObject;

            var texture = Resources.Load<Texture2D>("Images/" + currentSpecies.name);
            item.GetComponentInChildren<Image>().sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
            item.GetComponentInChildren<Toggle>().onValueChanged.AddListener((val) => {
                if (val) {
                    selected = currentSpecies;
                    item.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 0.5f);
                } else {
                    selected = null;
                    item.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
                }
            });

			item.GetComponentInChildren<Toggle>().group = toggleGroup;
            item.transform.SetParent(unitList.transform);
            item.transform.position = new Vector3(item.transform.position.x, item.transform.position.y, 0.0f);
            item.transform.localScale = Vector3.one;
        }
	}

    void Update() {

		if (selected == null) return;
		
		if (Input.GetButton("Fire1") && !EventSystem.current.IsPointerOverGameObject()) {
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if (Physics.Raycast(ray, out hit, 100000, LayerMask.GetMask("Terrain"))) {
				NavMeshHit placement;
				if (NavMesh.SamplePosition(hit.point, out placement, 1000, 1)) {
					//Added by Omar
					var allyResource = Resources.Load<GameObject>("Prefabs/ClashOfSpecies/Units/" + selected.name);
					var allyObject = Instantiate(allyResource, placement.position, Quaternion.identity) as GameObject;
					/*
                    var allyObject = Instantiate(Resources.Load<GameObject>("Prefabs/ClashOfSpecies/Units/" + selected.name)) as GameObject;
                    allyObject.transform.position = placement.position;
                    allyObject.transform.rotation = Quaternion.identity;
                    */
					Vector2 normPos = new Vector2(placement.position.x - terrain.transform.position.x,
					                              placement.position.z - terrain.transform.position.z);
					normPos.x = normPos.x / terrain.terrainData.size.x;
					normPos.y = normPos.y / terrain.terrainData.size.z;
					manager.pendingDefenseConfig.layout[selected] = normPos;

					var toggle = toggleGroup.ActiveToggles ().FirstOrDefault();
					toggle.enabled = false;
					toggle.interactable = false;

					selected = null;
				}
			}
		}
    }

	public void ReturnToShop() {
		Game.LoadScene("ClashDefenseShop");
	}

	public void ConfirmDefense() {
        var pending = manager.pendingDefenseConfig;
        var request = ClashDefenseSetupProtocol.Prepare(pending.terrain, pending.layout.Select(p => new { p.Key.id, p.Value })
            .ToDictionary(p => p.id, p => p.Value));

        NetworkManager.Send(request, (res) => {
            var response = res as ResponseClashDefenseSetup;
            if (response.valid) {
                manager.defenseConfig = manager.pendingDefenseConfig;
                manager.pendingDefenseConfig = null;
                Game.LoadScene("ClashMain");
            } else {
				errorCanvas.SetActive(true);
				errorMessage.text = "Place all your units down before confirming";
			}
        });
	}

	public void ConfirmError() {
		errorMessage.text = ""; 
		errorCanvas.SetActive (false);
	}
}
