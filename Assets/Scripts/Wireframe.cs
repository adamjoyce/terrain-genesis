using UnityEngine;
using System.Collections;

public class Wireframe : MonoBehaviour {

    private void OnPreRender() {
        GL.wireframe = true;
    }

    private void OnPostRender() {
        GL.wireframe = false;
    }
}
