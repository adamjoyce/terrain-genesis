using UnityEngine;
using System.Collections;

public class MenuGUI : MonoBehaviour {

	private void OnGUI() { 
        // Background box.
        GUI.Box(new Rect(10f, 10f, 200f, 500f), "Menu");
        
    }
}
