using UnityEngine;
using System.Collections;

public class ButtonBuild : MonoBehaviour {

	public Texture test1;
	public GameObject obj;
	public float xMin = 30F;
	public float xMax = 70F;
	public float zMin = 40F;
	public float zMax = 60F;
	//void OnGUI(){
	//	GUI.Button(new Rect (10, 10, 50, 200),test1);
	//}
	// Use this for initialization
	public void OnMouseDown(){
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
		//Instantiate (obj, newPos, Quaternion.identity);
	}

	void Start () {

		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
