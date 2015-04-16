using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ShopController : MonoBehaviour {
	GameObject required_object;
	public Transform c;

	// Use this for initialization
	void Start () {
		required_object = GameObject.Find ("Persistent Object");
		if (required_object == null) {
			//instantiate the persistent object
			//print ("NULL");
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
