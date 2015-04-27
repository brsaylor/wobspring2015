using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class ClashPlayerElement {
	public int id;
	public string name;
	public bool isSelected;
}

public class ClashMainController : MonoBehaviour {
	public GameObject playerToggle;
	public List<ClashPlayerElement> playerList;
	public Transform contentPanel;
	string selectedPlayer = "";
	int defendingTerrain = -1;
    ToggleGroup toggleGroup = null;
	ClashPreviewController pctrl;
	GameObject required_object;
	ClashPersistentData pd;


	void Awake() {
		required_object = GameObject.Find ("Persistent Object");
		if (required_object == null) {
			Application.LoadLevel ("ClashSplash");
		}
	}

	void Start () {
		pd = required_object.GetComponent<ClashPersistentData> ();
        toggleGroup = contentPanel.GetComponent<ToggleGroup>();
		pctrl = gameObject.GetComponent<ClashPreviewController> ();
		//PopulateScrollView ();
		playerList = new List<ClashPlayerElement>();
		RetrievePlayerList ();
	}

	void Update() {

	}

	//protocol does this
	//gets only the player name and terrain name from the valid defense table
	void RetrievePlayerList() {
		NetworkManager.Send (ClashPlayerListProtocol.Prepare (), (res) => {
			var response = res as ResponseClashPlayerList;
			foreach (var pair in response.players) {
				ClashPlayerElement element = new ClashPlayerElement();
				element.id = pair.Key;
				element.name = pair.Value;
				element.isSelected = false;
				playerList.Add (element);
			}
			PopulateScrollView();
		});
	}

	void PopulateScrollView() {
		foreach (var playerElement in playerList) {
			GameObject newToggle = Instantiate(playerToggle) as GameObject;
			ClashPlayerToggle toggle = newToggle.GetComponent<ClashPlayerToggle>();
			toggle.label.text = playerElement.name;
			toggle.player_name = playerElement.name;
			toggle.player_id = playerElement.id;
			toggle.toggle.isOn = playerElement.isSelected;
			toggle.toggle.onValueChanged.AddListener((value) => ToggleAction(toggle, value));
			toggle.toggle.group = toggleGroup;
			newToggle.transform.SetParent(contentPanel);
		}
	}

	public void ToggleAction(ClashPlayerToggle toggle, bool state) {
		selectedPlayer = state ? toggle.name : "";
		defendingTerrain = state ? toggle.terrain_id : -1;
		pctrl.text.enabled = !state;
		//pctrl.display = state ? Resources.Load("", typeof(Sprite)) : null;
		//Debug.Log (selectedPlayer);
		//Debug.Log (defendingTerrain);
	}

	//load the defense shop scene
	public void EditDefense() {
		pd.type = "defense";

		Application.LoadLevel ("ClashShop");
	}

	public void AttackPlayer() {
		if (selectedPlayer != "") {
			pd.type = "offense";
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
