using UnityEngine;
using System.Collections;

public class ClickTest : MonoBehaviour {
    public GameObject obj;
	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnMouseUp()
    {
        Instantiate(obj);
    }
}
