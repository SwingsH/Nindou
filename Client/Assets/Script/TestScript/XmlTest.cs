using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Text;
using System.IO;
public class XmlTest : MonoBehaviour {

	// Use this for initialization
	void Start () {

		XmlSerializer xml = new XmlSerializer(typeof(List<StageInfo>));
		List<StageInfo> silist = new List<StageInfo>();
		silist.Add(new StageInfo());
		silist.Add(new StageInfo());
		silist.Add(new StageInfo());

		StringWriter sw = new StringWriter();
		xml.Serialize(sw, silist);
		Debug.Log(sw);

		FileStream fs = new FileStream(Application.dataPath + "/Test/Resources/Data/StageInfo.xml", FileMode.Create);
		xml.Serialize(fs, silist);
		fs.Close();
	}
	
	
}
