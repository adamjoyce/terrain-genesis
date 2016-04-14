using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class SurfaceGenerator : MonoBehaviour {

    [Range(1, 200)]
    public int resolution = 10;

    private Mesh mesh;
    private int currentResolution;

    // Refresh the surface mesh.
    public void Refresh() {
        if (currentResolution != resolution)
            CreateMeshGrid();
    }

    // Creates a mesh grid where the size is dependent on the resolution.
    private void CreateMeshGrid() {
        currentResolution = resolution;
        int gridLength = currentResolution + 1;
        Vector3[] vertices = new Vector3[gridLength * gridLength];
        float quadSize = 1f / currentResolution;

        // Build the mesh grid array.
        for (int i = 0, column = 0; column <= currentResolution; column++) {
            for (int row = 0; row <= currentResolution; row++, i++) {
                vertices[i] = new Vector3(row * quadSize - 0.5f, column * quadSize - 0.5f);
            }
        }

        mesh.vertices = vertices;
        mesh.triangles = new int[] {
            0, 2, 1,
            1, 2, 3
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
