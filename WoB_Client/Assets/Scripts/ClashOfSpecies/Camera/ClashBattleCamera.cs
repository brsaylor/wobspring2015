using UnityEngine;
using System.Collections;

public class ClashBattleCamera : MonoBehaviour {

    public BoxCollider bounds;
    public Vector3 target;

	// Use this for initialization
	void Start () {}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButton(0) && Input.GetMouseButton(1)) {
            // Pan and navigate.
        } else if (Input.GetMouseButton(1)) {
            // Rotate.
        }
	}
}
