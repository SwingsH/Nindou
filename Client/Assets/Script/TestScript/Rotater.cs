using UnityEngine;
using System.Collections;

public class Rotater : MonoBehaviour {

	public Vector3 Speed = Vector3.zero;
	// Update is called once per frame
	void Update () {
		transform.Rotate(Speed,Space.World);
	}
}
