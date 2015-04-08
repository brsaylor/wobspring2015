using UnityEngine;
using System.Collections;

public class ClashList : MonoBehaviour {
	public Texture background;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI() {
		// Background
		GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), background, ScaleMode.ScaleAndCrop);
	}
}


/*
 * public Transform panel_target;//this must have Grid Layout Group
   public GameObject mybutton;
   GameObject temp;
   // Use this for initialization
   void Start () {
   
     foreach (RoomInfo room in PhotonNetwork.GetRoomList())
     {
       temp = (GameObject)Instantiate(mybutton);
         temp.transform.SetParent(panel_target_target);
     }
   }
   */
