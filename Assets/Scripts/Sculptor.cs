using UnityEngine;
using System.Collections;

public class Sculptor : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButton(0)) {
            RaycastHit hit;
            if (!Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
                return;

            MeshCollider meshCollider = hit.collider as MeshCollider;
            //if (meshCollider == null || meshCollider.sharedMesh == null)
            //return;

            Mesh mesh = meshCollider.sharedMesh;
            Vector3[] vertices = mesh.vertices;
            int[] triangles = mesh.triangles;
            vertices[triangles[hit.triangleIndex * 3 + 0]] += new Vector3(0, 1, 0) * Time.deltaTime * 0.1f;
            vertices[triangles[hit.triangleIndex * 3 + 1]] += new Vector3(0, 1, 0) * Time.deltaTime * 0.1f;
            vertices[triangles[hit.triangleIndex * 3 + 2]] += new Vector3(0, 1, 0) * Time.deltaTime * 0.1f;
            mesh.vertices = vertices;
            //Vector3 p0 = vertices[triangles[hit.triangleIndex + 0]];
            //Vector3 p1 = vertices[triangles[hit.triangleIndex + 1]];
            //Vector3 p2 = vertices[triangles[hit.triangleIndex + 2]];
            //Transform hitTransform = hit.collider.transform;
            //p0 = hitTransform.TransformPoint(p0);
            //p1 = hitTransform.TransformPoint(p1);
            //p2 = hitTransform.TransformPoint(p2);
            //Debug.DrawLine(p0, p1);
            //Debug.DrawLine(p1, p2);
            //Debug.DrawLine(p2, p0);
        }
    }
}
