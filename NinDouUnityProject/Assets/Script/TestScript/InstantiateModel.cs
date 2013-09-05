using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class InstantiateModel : MonoBehaviour {
	public GameObject targetPrefab;
	public Renderer land;
	public List<GameObject> goList = new List<GameObject>();
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.touchCount > 3)
		{
			foreach (GameObject goi in goList)
				Destroy(goi);
			goList.Clear();
		}
		else if (Input.GetMouseButtonDown(0) || (Input.touchCount > 1 && Input.GetTouch(1).phase == TouchPhase.Moved))
		{
			Vector3 sPos;
			if (Input.GetMouseButtonDown(0))
				sPos = Input.mousePosition;
			else
				sPos = Input.GetTouch(1).position;
			RaycastHit rh;
			if (Physics.Raycast(Camera.main.ScreenPointToRay(sPos), out rh, Camera.main.far))
			{
				GameObject newGameObject = Instantiate(targetPrefab) as GameObject;
				goList.Add(newGameObject);
				Vector3 newPos = rh.point;
				//newPos.y += 2.2f;
				newGameObject.transform.position = newPos;
				//newGameObject.AddComponent<Rotater>().Speed.y = Random.Range(-30f, 30f);
				newGameObject.transform.Rotate(0, Random.Range(1, 360),0, Space.World);	
			}
		}
		if (Input.GetKeyDown(KeyCode.Escape)) { Application.Quit(); }
	}

	void OnGUI()
	{
		GUI.Label(new Rect(Screen.width - 100, 10, 100, 30),goList.Count.ToString());
	}
}
