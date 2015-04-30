using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ButtonListener : MonoBehaviour {
	public Button button; //Drag a button transform on to here, and see how to toggle it when Space is pressed
	GameObject required_object;
	ClashPersistentData data;
	public GameObject obj;
	public float xMin = 30F;
	public float xMax = 70F;
	public float zMin = 40F;
	public float zMax = 60F;

	void Awake() {
		required_object = GameObject.Find ("Persistent Object");
		if (required_object == null) {
			
			//Application.LoadLevel ("ClashSplash");
			
		}
	}
	void Start () {
		data = required_object.GetComponent ("AttackingData") as ClashPersistentData;
	
		
		button.onClick.AddListener (() => Function ());
		
	}
	void Update(){

	}


	public void Function() {

	/*
		foreach (ClashUnitData ud in data.attackerInfo.offense) {
			if(ud.species_id == 1) {
				Vector3 newPos = new Vector3(Random.Range(xMin, xMax), 1, Random.Range(zMin, zMax));
				Instantiate(Resources.Load("Prefabs/3dModels/Setted models/CARNIVORE_Prefab",typeof(GameObject)), newPos, Quaternion.identity);
			}
				
		}
		*/
		button.interactable = false; 

		//void OnGUI(){
		//	GUI.Button(new Rect (10, 10, 50, 200),test1);
		//}
		// Use this for initialization

			//if(GUIUtility.hotControl == 0) {	//mouse 1 pressed
			Vector3 newPos = new Vector3(Random.Range(xMin, xMax), 1, Random.Range(zMin, zMax));
			/*RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if (Physics.Raycast(ray, out hit, 1000.0f))
			{
				print (hit.collider.gameObject.tag);
				//if (hit.collider.gameObject.tag == "Terrain") {
			Instantiate (obj, newPos, Quaternion.identity);
				//}
			}
		//}*/
			Instantiate (obj, newPos, Quaternion.identity);
	
	
}
}