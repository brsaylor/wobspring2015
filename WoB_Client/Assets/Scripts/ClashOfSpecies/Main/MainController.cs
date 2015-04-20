using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class PlayerElement {
	public int id;
	public string name;
	public string terrain;
	public bool isSelected;
}

public class MainController : MonoBehaviour {
	public GameObject playerToggle;
	public List<PlayerElement> playerList;
	public Transform contentPanel;
	string selectedPlayer = "";
	string defendingTerrain = "";
    ToggleGroup toggleGroup = null;
	PreviewController pctrl;
	GameObject required_object;


	void Awake() {
		//	playerList = RetrievePlayerList ();
	}

	void Start () {
		required_object = GameObject.Find ("Persistent Object");
/*		if (required_object == null) {
			Application.LoadLevel ("ClashSplash");
		}*/
        toggleGroup = GameObject.FindGameObjectWithTag("ContentPanel").GetComponent<ToggleGroup>();
		pctrl = gameObject.GetComponent<PreviewController> ();
		PopulateScrollView ();
	}

	void Update() {
/*
		if (defendingTerrain != "") {
			//pctrl.display = Resources.Load ("Images\\Acacia" + defendingTerrain, typeof(Image));

			pctrl.text.enabled = false;
		} else {
			pctrl.display = null;
			pctrl.text.enabled = true;
		}
	*/
	}

	//protocol does this
	//gets only the player name and terrain name from the valid defense table
	List<PlayerElement> RetrievePlayerList() {
		return new List<PlayerElement>();
	}

	void PopulateScrollView() {
		foreach (var playerElement in playerList) {
			GameObject newToggle = Instantiate(playerToggle) as GameObject;
			PlayerToggle toggle = newToggle.GetComponent<PlayerToggle>();
			toggle.label.text = playerElement.name;
			toggle.player_name = playerElement.name;
			toggle.player_id = playerElement.id;
			toggle.terrain = playerElement.terrain;
			toggle.toggle.isOn = playerElement.isSelected;
			toggle.toggle.onValueChanged.AddListener((value) => ToggleAction(toggle, value));
			toggle.toggle.group = toggleGroup;
			newToggle.transform.SetParent(contentPanel);
		}
	}

	public void ToggleAction(PlayerToggle toggle, bool state) {
		selectedPlayer = state ? toggle.name : "";
		defendingTerrain = state ? toggle.terrain : "";
		pctrl.text.enabled = !state;
		//pctrl.display = state ? Resources.Load("", typeof(Sprite)) : null;
		//Debug.Log (selectedPlayer);
		//Debug.Log (defendingTerrain);
	}

	//load the defense shop scene
	public void EditDefense() {
		PersistentData persistentData = GameObject.Find("Persistent Data").GetComponent<PersistentData>();
		persistentData.SetSceneType ("defense");
		persistentData.SetDefenderId (persistentData.GetPlayerId ());

		Application.LoadLevel ("ClashShop");
	}

	public void AttackPlayer() {
		if (selectedPlayer != "") {
			PersistentData persistentData = GameObject.Find("Persistent Data").GetComponent<PersistentData>();
			persistentData.SetSceneType("offense");
			persistentData.SetDefenderName(selectedPlayer);
			persistentData.SetDefenderTerrain(defendingTerrain);
			//Debug.Log(atkData.getDefenderName());
			//Debug.Log(atkData.getDefenderTerrain());

			persistentData.SetAttackerName(persistentData.GetPlayerName());
			persistentData.SetAttackerId(persistentData.GetPlayerId());

			Application.LoadLevel ("ClashShop");
		}
	}

	public void ReturnToLobby() {
		GameObject go = GameObject.Find ("Attack Data");

		if (go != null) {
			Destroy(go);
		}

		//Application.LoadScene ("Lobby");	//lobby scene
	}
}
