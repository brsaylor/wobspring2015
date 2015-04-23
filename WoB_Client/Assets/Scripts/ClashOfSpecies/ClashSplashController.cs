using UnityEngine;
using System.Collections;

public class ClashSplashController : MonoBehaviour {
	GameObject required_object;
	ClashPersistentData pd;

	void Awake() {
		Screen.SetResolution (800, 600, true);
		required_object = GameObject.Find ("Persistent Object");
		if (required_object == null) {
			//return to lobby because error
			Debug.Log("Error re-enter game from lobby");
		}
	}

	// Use this for initialization
	void Start () {
		pd = required_object.GetComponent<ClashPersistentData> ();
		pd.SetPlayerName ("Player Name");
		pd.SetPlayerId (3);

		//load the species list into the persistent data
		//

		//request server to check if defense map is in database table
		//bool b = RequestResult();
		//if(b) {
		if (1 == 0) {
			pd.type = "offense";
			Application.LoadLevel("ClashMain");
		} else {
			pd.type = "defense";
			Application.LoadLevel("ClashShop");
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
