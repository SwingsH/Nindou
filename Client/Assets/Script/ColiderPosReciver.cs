using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ColiderPosReciver : MonoBehaviour {

	HashSet<MonoBehaviour> reg = new HashSet<MonoBehaviour>();
	public void Regiester(MonoBehaviour mono)
	{
		reg.Add(mono);
	}
	public void UnRegiester(MonoBehaviour mono)
	{
		reg.Remove(mono);
	}
	void OnMouseDown()
	{
		RaycastHit rh;
		if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out rh))
		{
			foreach (MonoBehaviour mono in reg)
				mono.SendMessage("ColliderReciver",rh.point);
		}
	}
	void OnMouseOver()
	{
		if (Input.GetMouseButton(1))
		{
			RaycastHit rh;
			if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out rh))
			{
				foreach (MonoBehaviour mono in reg)
					mono.SendMessage("ColliderReciver", rh.point);
			}
		}
	}
}
