using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class PlayerElement {
	public string name;
	public string terrain;
	public bool isSelected;
}

public class CreateScrollList : MonoBehaviour {
	public GameObject sampleToggle;
	public List<PlayerElement> playerList;
	public Transform contentPanel;
	string selectedPlayer;
	bool isSelected;

	void Awake() {
	//	playerList = PopulatePlayerList ();
	}

	void Start () {
		isSelected = true;
		PopulateScrollView ();
	}

	//protocol does this
	//gets only the player name and terrain name from the valid defense table
	List<PlayerElement> PopulatePlayerList() {
		return new List<PlayerElement>();
	}

	void PopulateScrollView() {
		foreach (var playerElement in playerList) {
			GameObject newToggle = Instantiate(sampleToggle) as GameObject;
			SampleToggle toggle = newToggle.GetComponent<SampleToggle>();
			toggle.label.text = playerElement.name;
			toggle.terrain = playerElement.terrain;
			toggle.toggle.isOn = playerElement.isSelected;
			toggle.toggle.onValueChanged.AddListener(delegate {ToggleAction(playerElement.name);});
			newToggle.transform.SetParent(contentPanel);
		}
	}

	public void ToggleAction(string s) {
		Debug.Log (s);

	}
}
