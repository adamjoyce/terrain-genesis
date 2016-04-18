using UnityEngine;
using System.Collections;

public class CameraControls : MonoBehaviour {

    public Camera wireframe;
    public Transform terrain;

    private float speed = 1000f;
    private bool wireframeActive = false;

    // Use this for initilisation.
    void Start() {
        gameObject.transform.LookAt(terrain);
    }

	// Update is called once per frame.
	void Update () {
	    if (Input.GetMouseButton(1)) {
            transform.LookAt(terrain);
            transform.RotateAround(terrain.position, Vector3.up, Input.GetAxis("Mouse X") * speed * Time.deltaTime);
        }
	}

    // Toggles the wireframe camera for viewing the scene in wireframe mode.
    public void toggleWireframe() {
        wireframeActive = !wireframeActive;
        if (wireframeActive) {
            wireframe.enabled = true;
        } else {
            wireframe.enabled = false;
        }
    }
}
