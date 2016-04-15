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
        mesh.Clear();

        int gridLength = currentResolution + 1;
        float quadSize = 1f / currentResolution;

        Vector3[] vertices = new Vector3[gridLength * gridLength];
        Vector2[] uvs = new Vector2[vertices.Length];
        Vector3[] normals = new Vector3[vertices.Length];

        // Build the mesh grid vertex array.
        for (int i = 0, y = 0; y <= currentResolution; y++) {
            for (int x = 0; x <= currentResolution; x++, i++) {
                vertices[i] = new Vector3(x * quadSize - 0.5f, y * quadSize - 0.5f);
                uvs[i] = new Vector3(x * quadSize, y * quadSize);
                normals[i] = new Vector3(0, 0, -1);
            }
        }
        mesh.vertices = vertices;
        mesh.uv = uvs;
        mesh.normals = normals;

        // Build the mesh grid triangle array.
        int[] triangles = new int[currentResolution * currentResolution * 6];
        for (int ti = 0, vi = 0, y = 0; y < currentResolution; vi++, y++) {
            for (int x = 0; x < currentResolution; x++, ti += 6, vi++) {
                triangles[ti] = vi;
                triangles[ti + 1] = vi + currentResolution + 1;
                triangles[ti + 2] = vi + 1;
                triangles[ti + 3] = vi + 1;
                triangles[ti + 4] = vi + currentResolution + 1;
                triangles[ti + 5] = vi + currentResolution + 2;
            }
        }
        mesh.triangles = triangles;
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
