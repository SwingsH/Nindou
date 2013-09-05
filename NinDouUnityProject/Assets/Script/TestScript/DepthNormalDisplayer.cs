using UnityEngine;
using System.Collections;
[ExecuteInEditMode]
public class DepthNormalDisplayer : MonoBehaviour {
	public Material mat;
	// Use this for initialization
	void Start () {
		camera.depthTextureMode = DepthTextureMode.DepthNormals;
		mat =new Material(Shader.Find("Custom/DepthNormalDisplayer"));
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnRenderImage(RenderTexture src, RenderTexture dest)
	{
		Graphics.Blit(src, dest, mat);
	}
}
