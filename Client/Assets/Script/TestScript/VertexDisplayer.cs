using UnityEngine;
using System.Collections;
[ExecuteInEditMode]
public class VertexDisplayer : MonoBehaviour {
	public Transform[] vertex;
	public MeshFilter meshFilter;
	public Mesh mesh;
	public int flagIndex = 0;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (mesh)
		{
			for (int i = 0 ; i< mesh.vertexCount; i++)
			{
				if (i != flagIndex)
					continue;
				Vector3 wv3 = transform.TransformPoint(mesh.vertices[i]);
				Vector3 wn3 = transform.TransformDirection(mesh.normals[i]);
				Debug.DrawLine(wv3, wv3 + wn3 , Color.blue);
				
			}
		}
	}
	void Reset()
	{
		if (vertex != null)
		{
			foreach (Transform tri in vertex)
			{
				DestroyImmediate(tri);
			}
		}
		meshFilter = GetComponent<MeshFilter>();
		if (meshFilter)
		{
			mesh = meshFilter.sharedMesh;
			//vertex = new Transform[mesh.vertexCount];
		}
	}
}
