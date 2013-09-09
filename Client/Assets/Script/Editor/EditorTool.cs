using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
public class EditorTool
{
	[MenuItem("Tools/Mesh/Copy")]
	static void CopyMesh()
	{
		if (Selection.activeObject is Mesh)
		{
			Mesh om = Selection.activeObject as Mesh;
			Mesh m = Mesh.Instantiate(om) as Mesh;
			AssetDatabase.CreateAsset(m, "Assets/" + om.name + "Copied.Asset");
		}
	}
	[MenuItem("Tools/Toon/Ramp Texture")]
	static void RampTexture()
	{
		Texture2D t2D = AssetDatabase.LoadAssetAtPath("Assets/Resources/RampTex.Asset", typeof(Texture2D)) as Texture2D;
		if (t2D == null)
		{
			t2D = new Texture2D(128, 2);
			AssetDatabase.CreateAsset(t2D, "Assets/Resources/RampTex.Asset");
		}
		float g = 0.0f;
		for (int i = 0; i < 128; i++)
		{
			if (i > 128 / 2.5f && i < 128 / 2f)
				g += 0.05f;
			if (i > 128 / 1.5f)
				g += 0.05f;
			g = Mathf.Clamp01(g);
			t2D.SetPixel(i, 0, new Color(g, g, g, g));
			t2D.SetPixel(i, 1, new Color(g, g, g, g));
		}
		t2D.Apply();

	}
	[MenuItem("Tools/Mesh/SimpleMesh")]
	static void SimpleMesh()
	{
		Mesh sm = AssetDatabase.LoadAssetAtPath("Assets/Resources/Meshs/SimpleMesh.Asset", typeof(Mesh)) as Mesh;
		if (sm == null)
		{
			sm = new Mesh();
			AssetDatabase.CreateAsset(sm, "Assets/Resources/Meshs/SimpleMesh.Asset");
		}

		Vector3[] vers = new Vector3[] { new Vector3(-0.5f, 0, 0.5f), new Vector3(0.5f, 0, 0.5f), new Vector3(-0.5f, 0, -0.5f), new Vector3(0.5f, 0, -0.5f) };
		Vector2[] uvs = new Vector2[] { new Vector2(0, 1), new Vector2(1, 1), new Vector2(0, 0), new Vector2(1, 0) };
		Vector3[] normals = new Vector3[] { Vector3.up, Vector3.up, Vector3.up, Vector3.up };
		sm.vertices = vers;
		sm.uv = uvs;
		sm.normals = normals;
		sm.triangles = new int[] { 0, 1, 2, 1, 3, 2 };
	}

	[MenuItem("Tools/Mesh/SetBone")]
	static void SetMeshBone()
	{
		if (Selection.activeGameObject.GetComponent<SkinnedMeshRenderer>())
		{
			SkinnedMeshRenderer smr = Selection.activeGameObject.GetComponent<SkinnedMeshRenderer>();
			smr.bones = smr.rootBone.GetComponentsInChildren<Transform>();
			Mesh m = Selection.activeGameObject.GetComponent<SkinnedMeshRenderer>().sharedMesh;
			Matrix4x4[] bindposes = new Matrix4x4[smr.bones.Length];
			BoneWeight[] weights = new BoneWeight[m.vertexCount];
			for (int i = 0; i < smr.bones.Length; i++)
			{
				bindposes[i] = smr.bones[i].worldToLocalMatrix * smr.transform.localToWorldMatrix;
			}
			for (int i = 0; i < m.vertexCount; i++)
			{

				weights[i] = new BoneWeight();


				float dis = float.MaxValue;
				for (int j = 0; j < bindposes.Length; j++)
				{
					float jdis = Vector3.Distance(smr.transform.InverseTransformPoint(smr.bones[j].transform.position), m.vertices[i]);
					if (jdis < dis)
					{
						dis = jdis;
						weights[i].boneIndex0 = j;
					}
				}

				weights[i].weight0 = 1;
			}
			m.boneWeights = weights;
			m.bindposes = bindposes;

		}
	}
	[MenuItem("Tools/Combine Mesh")]
	static void CombineMesh()
	{
		if (!Application.isPlaying)
			return;
		if (Selection.gameObjects.Length > 0)
		{
			List<MeshFilter> meshFilters = new List<MeshFilter>();
			foreach (GameObject goi in Selection.gameObjects)
			{
				MeshFilter mf = goi.GetComponent<MeshFilter>();
				if (mf)
				{
					if (!meshFilters.Contains(mf))
						meshFilters.Add(mf);
				}
			}

			if (meshFilters.Count > 1)
			{
				Mesh newMesh = new Mesh();
				CombineInstance[] cis = new CombineInstance[meshFilters.Count];
				for (int i = 0; i < cis.Length; i++)
				{
					cis[i].mesh = meshFilters[i].sharedMesh;
					cis[i].transform = Matrix4x4.identity;
					//cis[i].subMeshIndex = i;
					//meshFilters[i].gameObject.SetActive(false);
				}
				newMesh.CombineMeshes(cis, true);
				GameObject go = new GameObject("NEWGO");
				go.transform.position = Vector3.left;
				go.AddComponent<MeshFilter>().sharedMesh = newMesh;
				go.AddComponent<MeshRenderer>().sharedMaterial = meshFilters[0].renderer.sharedMaterial;
				for (int i = 0; i < meshFilters.Count; i++)
				{
					meshFilters[i].sharedMesh = newMesh;

					//Material tempM = meshFilters[i].renderer.sharedMaterial;
					//meshFilters[i].renderer.sharedMaterials = new Material[cis.Length];
					//meshFilters[i].renderer.sharedMaterials[i] = tempM;
				}
			}
		}
	}
}
