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
    private ToggleGroup toggle;

    public HorizontalLayoutGroup unitList;
    public GameObject attackItemPrefab;
    public GameObject attackUnitPrefab;

    public List<ClashBattleUnit> enemiesList = new List<ClashBattleUnit>();
    public List<ClashBattleUnit> alliesList = new List<ClashBattleUnit>();

	void Awake() {
        manager = GameObject.Find("MainObject").GetComponent<ClashGameManager>();
    }

	void Start () {
        var terrainObject = Instantiate(Resources.Load("Prefabs/ClashOfSpecies/Terrains/" + manager.currentTarget.terrain)) as GameObject;
        terrainObject.transform.position = Vector3.zero;
        terrainObject.transform.localScale = Vector3.one;

        var terrain = terrainObject.GetComponentInChildren<Terrain>();

        foreach (var pair in manager.currentTarget.layout) {
            var species = pair.Key;
            var speciesObject = Instantiate(attackUnitPrefab) as GameObject;
            speciesObject.tag = "Enemy";

            var speciesPrefab = Instantiate(Resources.Load<GameObject>("Models/" + species.name)) as GameObject;
            speciesPrefab.transform.SetParent(speciesObject.transform);
            speciesPrefab.transform.localPosition = Vector3.zero;
            //speciesPrefab.transform.localScale = Vector3.one;

            // Place navmesh agent.
            var speciesPos = new Vector3(pair.Value.x * terrain.terrainData.size.x, 0.0f, pair.Value.y * terrain.terrainData.size.z);
            NavMeshHit placement;
            if (NavMesh.SamplePosition(speciesPos, out placement, 1000, 1)) {
                speciesObject.transform.position = placement.position;
                speciesObject.AddComponent<NavMeshAgent>();
                var unit = speciesObject.AddComponent<ClashBattleUnit>();
                enemiesList.Add(unit);
                unit.species = species;
            } else {
                Debug.LogWarning("Failed to place unit.", speciesObject);
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
                    var allyObject = Instantiate(attackUnitPrefab, placement.position, Quaternion.identity) as GameObject;
                    allyObject.AddComponent<NavMeshAgent>();
                    allyObject.tag = "Ally";

                    var speciesPrefab = Instantiate(Resources.Load<GameObject>("Models/" + selected.name)) as GameObject;
                    speciesPrefab.transform.SetParent(allyObject.transform);
                    speciesPrefab.transform.localPosition = Vector3.zero;

                    var unit = allyObject.AddComponent<ClashBattleUnit>();
                    alliesList.Add(unit);
                    unit.species = selected;

                    selected = null;
                }
            }
        }
    }
	
	void FixedUpdate() {
        int totalEnemyHealth = 0;
        foreach (var enemy in enemiesList) {
            totalEnemyHealth += enemy.currentHealth;
            if (enemy.currentHealth > 0 && !enemy.target && alliesList.Count > 0) {
                var target = alliesList.Where(u => {
                    switch (enemy.species.type) {
                        case UnitType.CARNIVORE:
                            return (u.species.type == UnitType.CARNIVORE) || (u.species.type == UnitType.HERBIVORE) ||
                                                    (u.species.type == UnitType.OMNIVORE);
                        case UnitType.HERBIVORE:
                            return (u.species.type == UnitType.PLANT);
                        case UnitType.OMNIVORE:
                            return (u.species.type == UnitType.HERBIVORE) || (u.species.type == UnitType.PLANT);
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
        }

        int totalAllyHealth = 0;
        foreach (var ally in alliesList) {
            totalAllyHealth += ally.currentHealth;
            if (ally.currentHealth > 0 && !ally.target && enemiesList.Count > 0) {
                var target = enemiesList.Where(u => {
                    switch (ally.species.type) {
                        case UnitType.CARNIVORE:
                            return (u.species.type == UnitType.CARNIVORE) || (u.species.type == UnitType.HERBIVORE) ||
                                                    (u.species.type == UnitType.OMNIVORE);
                        case UnitType.HERBIVORE:
                            return (u.species.type == UnitType.PLANT);
                        case UnitType.OMNIVORE:
                            return (u.species.type == UnitType.HERBIVORE) || (u.species.type == UnitType.PLANT);
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

        if (totalAllyHealth == 0 && alliesList.Count > 0) {
            // ENEMIES HAVE WON!
        }
    }

}
