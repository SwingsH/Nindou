using UnityEngine;
using System.Collections;
[ExecuteInEditMode]
public class FakeShadowSimulate : MonoBehaviour {
	public Light simLight;
	public Mesh simMesh;
	public float _Height;
	public float _Radius;
	// Use this for initialization
	void Start () {
		simMesh = GetComponent<MeshFilter>().sharedMesh;
		GetComponent<MeshFilter>().sharedMesh = GetComponent<MeshFilter>().mesh;
	}
	public Matrix4x4 m44;
	// Update is called once per frame
	void Update () {
		Vector3 lDir = transform.InverseTransformDirection(simLight.transform.forward);
		lDir.y = transform.position.y;
		//lDir = lDir.normalized;
		Debug.DrawLine(transform.position, transform.TransformPoint(lDir), Color.yellow);
		
		m44.SetTRS(Vector3.zero,Quaternion.LookRotation(lDir),Vector3.one);
		for (int i = 0; i < 4; i++)
		{
			Vector3 v3 = simMesh.vertices[i];
			float vd = Mathf.Atan2(v3.z, v3.x);
			float vl = Mathf.Atan2(lDir.z, lDir.x);
			Debug.Log(vd);
			Debug.Log(vl);
			Vector3 nv3 = new Vector3(_Radius * Mathf.Cos(vd + vl - 0.5f * Mathf.PI), 0, _Radius * Mathf.Sin(vd + vl - 0.5f * Mathf.PI));
			if (Vector3.Dot(nv3, lDir)> 0)
			{
				nv3.x += lDir.x;
				nv3.z += lDir.z;
			}
			Debug.DrawLine(transform.position, transform.TransformPoint(Vector3.forward), Color.red);
			Debug.DrawLine(transform.position, transform.TransformPoint(nv3), Color.white);

		}
	}
}
