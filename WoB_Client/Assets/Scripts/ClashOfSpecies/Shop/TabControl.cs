using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public class TabControlEntry {
	[SerializeField]
	private GameObject panel = null;
	public GameObject Panel { get { return panel; } }
	
	[SerializeField]
	private Button tab = null;
	public Button Tab { get { return tab; } }
}

public class TabControl : MonoBehaviour {
	[SerializeField]
	private List<TabControlEntry> entries = null;

	// Use this for initialization
	void Start () {
		foreach (TabControlEntry entry in entries) {
			AddButtonListener(entry);
		}
		
		if (entries.Count > 0) {
			SelectTab(entries[0]);
		}
	}

	public void AddEntry(TabControlEntry entry) {
		entries.Add(entry);
	}
	
	private void AddButtonListener(TabControlEntry entry) {
		entry.Tab.onClick.AddListener(() => SelectTab(entry));
	}

	private void SelectTab(TabControlEntry selectedEntry) {
		foreach (TabControlEntry entry in entries) {
			bool isSelected = entry == selectedEntry;
			
			entry.Tab.interactable = !isSelected;
			entry.Panel.SetActive(isSelected);
		}
	}
}
