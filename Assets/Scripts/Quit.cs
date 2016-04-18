using UnityEngine;
using System.Collections;

public class Quit : MonoBehaviour {

	// Update is called once per frame
	void Update () {
	    if (Input.GetButtonDown("Cancel")) {
            Application.Quit();
        }
	}
}
