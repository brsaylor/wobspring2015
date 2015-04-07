using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {
	public Texture aTexture;
	GameObject loadedInvader;
	bool prefabLoaded;
	bool toggleTxt;

	// Use this for initialization
	void Start () {
		prefabLoaded = false;
		toggleTxt = false;
		loadedInvader = null;
	}
	
	// Update is called once per frame
	void Update () {
		if (prefabLoaded) {
			SpawnInvader ();
		} else {
			LoadPrefab();
		}
	}

	void OnGUI() {
		/*
		if(!aTexture) {
			Debug.LogError("Please assign a texture in the inspector.");
			return;
		}
		*/
		toggleTxt = GUILayout.Toggle (toggleTxt, "Herbivore");
	}

	void LoadPrefab() {
		loadedInvader = Resources.Load ("Herbivore", typeof(GameObject)) as GameObject;
		prefabLoaded = true;
	}

	private void SpawnInvader() {
		if(Input.GetButtonDown("Fire1")) {	//mouse 1 pressed
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if (Physics.Raycast(ray, out hit, 1000.0f))
			{
				print (hit.collider.gameObject.tag);
				if (hit.collider.gameObject.tag == "Terrain") {
					Instantiate (loadedInvader, hit.point, Quaternion.identity);
				}
			}
		}
	}
}
