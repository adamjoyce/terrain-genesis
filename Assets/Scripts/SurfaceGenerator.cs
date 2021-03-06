﻿using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer), typeof(MeshCollider))]
public class SurfaceGenerator : MonoBehaviour {

    [Range(1, 200)] public int resolution = 100;

    // Noise variables.
    public float frequency = 1f;
    [Range(1, 8)]   public int octaves = 1;
    [Range(1f, 4f)] public float lacunarity = 2f;
    [Range(0f, 1f)] public float persistence = 0.5f;
    [Range(1, 3)]   public int dimensions = 3;
    [Range(0f, 1f)] public float scaleFactor = 1f;
    public NoiseMethodType type;
    public Gradient coloring;
    public Vector3 noiseOffset;
    public Vector3 rotation;
    public bool damping;
    public bool scaleWithColour;
    public bool displayNormals;
    public bool wireframe;

    private Mesh mesh;
    private MeshCollider meshCollider;
    private int currentResolution;

    private Vector3[] vertices;
    private Vector3[] normals;
    private Color[] colours;

    // Refresh the surface mesh.
    public void Refresh() {
        if (currentResolution != resolution)
            CreateMeshGrid();

        Quaternion rotate = Quaternion.Euler(rotation);

        // Four quad corners transformed from local space to world space with rotation accounted for.
        Vector3 point00 = rotate * transform.TransformPoint(new Vector3(-0.5f, -0.5f)) + noiseOffset;
        Vector3 point10 = rotate * transform.TransformPoint(new Vector3(0.5f, -0.5f)) + noiseOffset;
        Vector3 point01 = rotate * transform.TransformPoint(new Vector3(-0.5f, 0.5f)) + noiseOffset;
        Vector3 point11 = rotate * transform.TransformPoint(new Vector3(0.5f, 0.5f)) + noiseOffset;

        // Determine the dimension for the noise method.
        NoiseMethod method = Noise.methods[(int)type][dimensions - 1];
        float quadSize = 1f / currentResolution;

        // Take into account frequency damping.
        float amplitude;
        if (damping) {
            amplitude = scaleFactor / frequency;
        } else {
            amplitude = scaleFactor;
        }

        for (int i = 0, y = 0; y <= resolution; y++) {
            // Linearly interpolate between the bottom left and top left corner and the bottom right and top right corner.
            Vector3 point0 = Vector3.Lerp(point00, point01, y * quadSize);
            Vector3 point1 = Vector3.Lerp(point10, point11, y * quadSize);

            // Loop through each quad taking an appropriate noise sample.
            for (int x = 0; x <= resolution; x++, i++) {
                Vector3 point = Vector3.Lerp(point0, point1, x * quadSize);
                float noiseSample = Noise.Sum(method, point, frequency, octaves, lacunarity, persistence);
                noiseSample = type == NoiseMethodType.Value ? (noiseSample - 0.5f) : (noiseSample * 0.5f);

                // Determine if colour should be retained when scaled.
                if (scaleWithColour) {
                    colours[i] = coloring.Evaluate(noiseSample + 0.5f);
                    noiseSample *= amplitude;
                } else {
                    noiseSample *= amplitude;
                    colours[i] = coloring.Evaluate(noiseSample + 0.5f);
                }
                vertices[i].y = noiseSample;
            }
        }

        // Update mesh values.
        mesh.vertices = vertices;
        mesh.colors = colours;
        mesh.RecalculateNormals();
        normals = mesh.normals;
        meshCollider.sharedMesh = mesh;

        //if (displayNormals) {
        //    DrawNormals();
        //}
    }

    // Creates a mesh grid where the size is dependent on the resolution.
    private void CreateMeshGrid() {
        currentResolution = resolution;
        mesh.Clear();

        int gridLength = currentResolution + 1;
        float quadSize = 1f / currentResolution;
        
        vertices = new Vector3[gridLength * gridLength];
        normals = new Vector3[vertices.Length];
        colours = new Color[vertices.Length];

        Vector2[] uvs = new Vector2[vertices.Length];

        // Build the mesh grid vertex arrays.
        for (int i = 0, z = 0; z <= currentResolution; z++) {
            for (int x = 0; x <= currentResolution; x++, i++) {
                vertices[i] = new Vector3(x * quadSize - 0.5f, 0f, z * quadSize - 0.5f);
                uvs[i] = new Vector3(x * quadSize, z * quadSize);
                normals[i] = new Vector3(0, 1, 0);
                colours[i] = new Color(0, 0, 0, 1);
            }
        }
        mesh.vertices = vertices;
        mesh.uv = uvs;
        mesh.normals = normals;
        mesh.colors = colours;

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
        meshCollider = GetComponent<MeshCollider>();
        Refresh();
    }

    //Draws the normals of the mesh when it is selected.
    private void OnDrawGizmos() {
        float scale = 1f / resolution;
        Gizmos.color = Color.cyan;
        if (displayNormals && vertices != null) {
            for (int i = 0; i < vertices.Length; i++) {
                Gizmos.DrawRay(vertices[i], scale * normals[i]);
            }
        }
    }

    //// Draws a ray for each quad signifing its normal.
    //private void DrawNormals() {
    //    float scale = 1f / resolution;
    //    for (int i = 0; i < vertices.Length; i++) {
    //        Debug.DrawRay(vertices[i], scale * normals[i], Color.cyan, 100f);
    //    }
    //}
}
