using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class SurfaceGenerator : MonoBehaviour {

    private Mesh mesh;

    // Refresh the surface mesh.
    public void Refresh() {
        mesh.vertices = new Vector3[] {
            new Vector3(0f, 0f),
            new Vector3(1f, 0f),
            new Vector3(0f, 1f)
        };

        mesh.triangles = new int[] {
            0, 1, 2
        };
    }

    // Creates a mesh if one does not exist.
    private void OnEnable() {
        if (mesh == null) {
            mesh = new Mesh();
            mesh.name = "Surface Mesh";
            GetComponent<MeshFilter>().mesh = mesh;
        }

        Refresh();
    }

}
