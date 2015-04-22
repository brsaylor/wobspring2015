using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class ClashPlayerElement {
	public int id;
	public string name;
	public string terrain;
	public bool isSelected;
}

public class ClashMainController : MonoBehaviour {
	public GameObject playerToggle;
	public List<ClashPlayerElement> playerList;
	public Transform contentPanel;
	string selectedPlayer = "";
	string defendingTerrain = "";
    ToggleGroup toggleGroup = null;
	ClashPreviewController pctrl;
	GameObject required_object;
	ClashPersistentData pd;


	void Awake() {
		required_object = GameObject.Find ("Persistent Object");
		if (required_object == null) {
			Application.LoadLevel ("ClashSplash");
		}

		//	playerList = RetrievePlayerList ();
	}

	void Start () {
		pd = required_object.GetComponent<ClashPersistentData> () as ClashPersistentData;
        toggleGroup = contentPanel.GetComponent<ToggleGroup>();
		pctrl = gameObject.GetComponent<ClashPreviewController> ();
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
	List<ClashPlayerElement> RetrievePlayerList() {
		return new List<ClashPlayerElement>();
	}

	void PopulateScrollView() {
		foreach (var playerElement in playerList) {
			GameObject newToggle = Instantiate(playerToggle) as GameObject;
			ClashPlayerToggle toggle = newToggle.GetComponent<ClashPlayerToggle>();
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

	public void ToggleAction(ClashPlayerToggle toggle, bool state) {
		selectedPlayer = state ? toggle.name : "";
		defendingTerrain = state ? toggle.terrain : "";
		pctrl.text.enabled = !state;
		//pctrl.display = state ? Resources.Load("", typeof(Sprite)) : null;
		//Debug.Log (selectedPlayer);
		//Debug.Log (defendingTerrain);
	}

	//load the defense shop scene
	public void EditDefense() {
		pd.SetSceneType ("defense");
		pd.SetDefenderId (pd.GetPlayerId ());

		Application.LoadLevel ("ClashShop");
	}

	public void AttackPlayer() {
		if (selectedPlayer != "") {
			pd.SetSceneType("offense");
			pd.SetDefenderName(selectedPlayer);
			pd.SetDefenderTerrain(defendingTerrain);
			//Debug.Log(atkData.getDefenderName());
			//Debug.Log(atkData.getDefenderTerrain());

			pd.SetAttackerName(pd.GetPlayerName());
			pd.SetAttackerId(pd.GetPlayerId());

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
