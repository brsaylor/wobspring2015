using UnityEngine;
using System.Collections;

public class ClashBattleCamera : MonoBehaviour {

    private SphereCollider reticle = null;

    public Terrain target {
        set {
            reticle = new GameObject("Reticle", typeof(SphereCollider)).GetComponent<SphereCollider>();
            var reticlePos = value.transform.position + (value.terrainData.size * 0.5f);
            reticlePos.y = value.SampleHeight(reticle.transform.position);
            reticle.transform.position = reticlePos;
            transform.position = reticle.transform.position + ((Vector3.back + Vector3.up).normalized * zoomLevel);
            transform.LookAt(reticle.transform);
        }
    }

    public bool dragging = false;
    public Vector3 lastPosition;
    public float yawSpeed = 5.0f;
    public float pitchSpeed = 5.0f;
    public float zoomLevel = 100.0f;

	
	// Update is called once per frame
	void Update() {
        if (!reticle) return;
        Debug.Log("HELLO");

        if (Input.GetMouseButtonDown(1)) {
            dragging = true;
            lastPosition = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(1))
            dragging = false;

        if (dragging) {
            var delta = Input.mousePosition - lastPosition;
            transform.RotateAround(reticle.transform.position, reticle.transform.up, yawSpeed * delta.x);
            transform.RotateAround(reticle.transform.position, reticle.transform.right, pitchSpeed * delta.y);
            transform.LookAt(reticle.transform, Vector3.up);
            lastPosition = Input.mousePosition;
        } else {
            dragging = false;
        }
	}
}
