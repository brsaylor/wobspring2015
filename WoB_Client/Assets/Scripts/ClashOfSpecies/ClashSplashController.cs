using UnityEngine;
using System.Collections;

public class ClashSplashController : MonoBehaviour {
	GameObject required_object;

	void Awake() {
		Screen.SetResolution (800, 600, true);
	}

	// Use this for initialization
	void Start () {
		required_object = GameObject.Find ("Persistent Object");
		if (required_object == null) {
			//return to lobby because error
			Debug.Log("Error re-enter game from lobby");
		}
		required_object.GetComponent<ClashPersistentData> ().SetPlayerName ("Player Name");
		required_object.GetComponent<ClashPersistentData> ().SetPlayerId (3);

		//request server to check if defense map is in database table
		//bool b = RequestResult();
		//if(b) {
		if (1 == 0) {
			required_object.GetComponent<ClashPersistentData> ().SetSceneType("offense");
			Application.LoadLevel("ClashMain");
		} else {
			required_object.GetComponent<ClashPersistentData> ().SetSceneType("defense");
			Application.LoadLevel("ClashShop");
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
