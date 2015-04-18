using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ShopButtonController : MonoBehaviour {
	GameObject required_object;
	public Button cancel;
	public Text cancelLabel;
	public Button accept;
	public Text acceptLabel;

	// Use this for initialization
	void Start () {
		required_object = GameObject.Find ("Persistent Object");
		/*	//ShopController.cs handles this; not needed here
		if (required_object == null) {
			Application.LoadLevel ("ClashSplash");
		}
		*/
	
		if (1 == 0 /*required_object.GetComponent<PersistentData> ().GetSceneType () == "defense"*/) {
			cancelLabel.text = "Return to Lobby\n(Cancel)";
			cancel.onClick.AddListener (() => BackToLobby ());
			acceptLabel.text = "Place Defense\n(Continue)";
			accept.onClick.AddListener (() => GoToClashBattle ());
		} else {
			cancelLabel.text = "Return to Main\n(Cancel)";
			cancel.onClick.AddListener (() => BackToMain ());
			acceptLabel.text = "Engage\n(Continue)";
			accept.onClick.AddListener (() => GoToClashBattle ());
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void BackToLobby() {
		//Destroy (required_object);		//destroy the persisent object
		//Application.LoadLevel ("Lobby"); //Go back to lobby
	}

	void BackToMain() {
		Application.LoadLevel ("ClashMain");
	}

	void GoToClashBattle() {
		//get all children gameObject in Canvas->SelectedUnits
		//put the data from those children gameObjects into the persistent data
		Application.LoadLevel ("ClashBattle");
	}
}
