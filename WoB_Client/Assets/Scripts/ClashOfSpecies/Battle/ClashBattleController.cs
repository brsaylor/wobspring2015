using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ClashBattleController : MonoBehaviour {
    private ClashGameManager manager;

    private ClashSpecies selected;
    private Terrain terrain;
    private ToggleGroup toggle;

    public HorizontalLayoutGroup unitList;
    public GameObject attackItemPrefab;
    public GameObject attackUnitPrefab; 

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
                unit.species = species;
            } else {
                Debug.LogWarning("Failed to place unit.", speciesObject);
            }
        }
	}
	
	// Update is called once per frame
	void Update () {

	}

	public bool IsEnemyDefeated() {
		GameObject[] enemies = GameObject.FindGameObjectsWithTag ("Enemy");
		
		return(enemies.Length == 0);
	}

	public bool IsAllyDefeated() {
		GameObject[] allies = GameObject.FindGameObjectsWithTag ("Ally");
		
		return(allies.Length == 0);
	}
}
