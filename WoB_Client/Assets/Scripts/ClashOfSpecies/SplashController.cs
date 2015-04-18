using UnityEngine;
using System.Collections;

public class SplashController : MonoBehaviour {
	GameObject required_object;
	
	// Use this for initialization
	void Start () {
		required_object = GameObject.Find ("Persistent Object");
		if (required_object == null) {
			//instantiate the persistent object
			//print ("NULL");
		}
		//required_object.GetComponent<PersistentData> ().SetCurrentPlayer ("Player Name");
		//required_object.GetComponent<PersistentData> ().SetPlayerId (id);

		//request server to check if defense map is in database table
		//bool b = RequestResult();
		//if(b) {
		//required_object.GetComponent<PersistentData> ().SetSceneType("offense");
		//Application.LoadLevel("ClashMain");
		//} else {
		//required_object.GetComponent<PersistentData> ().SetSceneType("defense");
		//Application.LoadLevel("ClashShop");
		//}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
