using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ClashBattleUnit : MonoBehaviour {

    private List<GameObject> enemies;

    public ClashSpecies species;

	void Start () {
        if (tag == "Ally") {
            enemies = new List<GameObject>(GameObject.FindGameObjectsWithTag("Enemy"));
        } else {
            enemies = new List<GameObject>(GameObject.FindGameObjectsWithTag("Ally"));
        }
	}
	
	void Update () {
	
	}
}
