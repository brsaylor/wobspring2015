using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ClashPlayerToggle : MonoBehaviour {
	public Toggle toggle;
	public Text label;
	public int id;

    void Awake() {
        toggle = GetComponent<Toggle>();
        label = GetComponent<Text>();
    }
}
