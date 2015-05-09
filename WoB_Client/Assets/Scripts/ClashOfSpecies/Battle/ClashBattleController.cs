using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnitType = ClashSpecies.SpeciesType;

public class ClashBattleController : MonoBehaviour {
    private ClashGameManager manager;

    private ClashSpecies selected;
    private Terrain terrain;
    private ToggleGroup toggleGroup;

    public HorizontalLayoutGroup unitList;
    public GameObject attackItemPrefab;

    public List<ClashBattleUnit> enemiesList = new List<ClashBattleUnit>();
    public List<ClashBattleUnit> alliesList = new List<ClashBattleUnit>();

	public GameObject messageCanvas;
	public Text messageText;

	void Awake() {
        manager = GameObject.Find("MainObject").GetComponent<ClashGameManager>();
		toggleGroup = unitList.gameObject.GetComponent<ToggleGroup> ();
    }

	void Start () {
        var terrainResource = Resources.Load("Prefabs/ClashOfSpecies/Terrains/" + manager.currentTarget.terrain);
        var terrainObject = Instantiate(terrainResource, Vector3.zero, Quaternion.identity) as GameObject;

        var terrain = terrainObject.GetComponentInChildren<Terrain>();
        Camera.main.GetComponent<ClashBattleCamera>().target = terrain;

        foreach (var pair in manager.currentTarget.layout) {
            var species = pair.Key;
            
            // Place navmesh agent.
            var speciesPos = new Vector3(pair.Value.x * terrain.terrainData.size.x, 0.0f, pair.Value.y * terrain.terrainData.size.z);
            NavMeshHit placement;
            if (NavMesh.SamplePosition(speciesPos, out placement, 1000, 1)) {
                var speciesResource = Resources.Load<GameObject>("Prefabs/ClashOfSpecies/Units/" + species.name);
                var speciesObject = Instantiate(speciesResource, placement.position, Quaternion.identity) as GameObject;
                speciesObject.tag = "Enemy";

                var unit = speciesObject.AddComponent<ClashBattleUnit>();
                enemiesList.Add(unit);
                unit.species = species;
            } else {
                Debug.LogWarning("Failed to place unit: " + species.name);
            }
        }

        // Populate user's selected unit panel.
        foreach (var species in manager.attackConfig.layout) {
            var item = Instantiate(attackItemPrefab) as GameObject;
            var current = species;

            var texture = Resources.Load<Texture2D>("Images/" + species.name);
            item.GetComponentInChildren<Image>().sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
            item.GetComponentInChildren<Toggle>().onValueChanged.AddListener((val) => {
                if (val) {
                    selected = current;
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

                    allyObject.tag = "Ally";

                    var unit = allyObject.AddComponent<ClashBattleUnit>();
                    alliesList.Add(unit);
                    unit.species = selected;

					var toggle = toggleGroup.ActiveToggles ().FirstOrDefault();
					toggle.enabled = false;
					toggle.interactable = false;

                    selected = null;
                }
            }
        }
    }
	
	void FixedUpdate() {
        int totalEnemyHealth = 0;

        foreach (var enemy in enemiesList) {

            totalEnemyHealth += enemy.currentHealth;
			Debug.Log(totalEnemyHealth);
			if (enemy.currentHealth > 0 && !enemy.target && alliesList.Count > 0) {
				Debug.Log ("Finding Enemy Target", gameObject);
                var target = alliesList.Where(u => {
					if(u.currentHealth<=0) 
						return false;
					switch (enemy.species.type) {
					case UnitType.CARNIVORE:
						return (u.species.type == UnitType.CARNIVORE) || (u.species.type == UnitType.HERBIVORE) ||
							(u.species.type == UnitType.OMNIVORE);
					case UnitType.HERBIVORE:
						return (u.species.type == UnitType.PLANT);
					case UnitType.OMNIVORE:
						return (u.species.type == UnitType.HERBIVORE) || (u.species.type == UnitType.PLANT)||
							(u.species.type == UnitType.CARNIVORE)||
								(u.species.type == UnitType.OMNIVORE);
					case UnitType.PLANT:
						return false;
					default: return false;
					}
					return false;
				}).OrderBy(u => {
					return (enemy.transform.position - u.transform.position).sqrMagnitude;
				}).FirstOrDefault();
				enemy.target = target;
			}
        }

        if (totalEnemyHealth == 0 && enemiesList.Count > 0) {
            // ALLIES HAVE WON!

			//messageCanvas.SetActive(true);
			//messageText.text = "You Won!\n\nKeep on fighting!";

			//TODO: Tell server you won
        }

        int totalAllyHealth = 0;
        foreach (var ally in alliesList) {
            totalAllyHealth += ally.currentHealth;
            if (ally.currentHealth > 0 && !ally.target && enemiesList.Count > 0) {
				Debug.Log ("Finding Ally Target", gameObject);
                var target = enemiesList.Where(u => {
					if(u.currentHealth<=0) 
						return false;

                    switch (ally.species.type) {
                        case UnitType.CARNIVORE:
                            return (u.species.type == UnitType.CARNIVORE) || (u.species.type == UnitType.HERBIVORE) ||
                                                    (u.species.type == UnitType.OMNIVORE);
                        case UnitType.HERBIVORE:
                            return (u.species.type == UnitType.PLANT);
                        case UnitType.OMNIVORE:
						return (u.species.type == UnitType.HERBIVORE) || (u.species.type == UnitType.PLANT)||
													(u.species.type == UnitType.CARNIVORE)||
													(u.species.type == UnitType.OMNIVORE);
					case UnitType.PLANT:
						return false;
					default: return false;
                    }
                    return false;
                }).OrderBy(u => {
                    return (ally.transform.position - u.transform.position).sqrMagnitude;
                }).FirstOrDefault();
                ally.target = target;
            }
        }

        if (totalAllyHealth == 0 && alliesList.Count == 5) {
            // ENEMIES HAVE WON!

			messageCanvas.SetActive(true);
			messageText.text = "You Lost!\n\nTry again next time!";

			//TODO: Tell server you lost
        }
    }

	public void ConfirmResult() {
		Game.LoadScene ("ClashMain");
	}
}
