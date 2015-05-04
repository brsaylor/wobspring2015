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
    private ToggleGroup toggle;

    public HorizontalLayoutGroup unitList;
    public GameObject defenseItemPrefab;
    public GameObject defenseUnitPrefab;

	void Awake() {
        manager = GameObject.Find("MainObject").GetComponent<ClashGameManager>();
        toggle = unitList.GetComponent<ToggleGroup>();
    }
    
	// Use this for initialization
	void Start () {
        var terrainObject = Resources.Load<GameObject>("Prefabs/ClashOfSpecies/Terrains/" + manager.pendingDefenseConfig.terrain);
        terrain = (Instantiate(terrainObject, Vector3.zero, Quaternion.identity) as GameObject).GetComponent<Terrain>();
        terrain.transform.position = Vector3.zero;
        terrain.transform.localScale = Vector3.one;

        foreach (var species in manager.pendingDefenseConfig.layout.Keys) {
            var item = Instantiate(defenseItemPrefab) as GameObject;

            var texture = Resources.Load<Texture2D>("Images/" + species.name);
            item.GetComponentInChildren<Image>().sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
            item.GetComponentInChildren<Toggle>().onValueChanged.AddListener((val) => {
                if (val) {
                    selected = species;
                    item.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 0.5f);
                } else {
                    selected = null;
                    item.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
                }
            });

            item.transform.SetParent(unitList.transform);
            item.transform.position = new Vector3(item.transform.position.x, item.transform.position.y, 0.0f);
            item.transform.localScale = Vector3.one;
        }
	}

    void Update() {
        if (Input.GetButton("Fire1") && !EventSystem.current.IsPointerOverGameObject()) {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 100000, LayerMask.GetMask("Terrain"))) {
                if (selected != null) {
                    Instantiate(defenseUnitPrefab, hit.point, Quaternion.identity);
                    
                    Vector2 normPos = new Vector2(hit.point.x - terrain.transform.position.x, hit.point.z - terrain.transform.position.z);
                    normPos.x = normPos.x / terrain.terrainData.size.x;
                    normPos.y = normPos.y / terrain.terrainData.size.z;

                    manager.pendingDefenseConfig.layout[selected] = normPos;
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
            }
        });

	}
}
