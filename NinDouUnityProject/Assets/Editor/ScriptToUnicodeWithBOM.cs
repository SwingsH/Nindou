using UnityEngine;
using System.Collections;
using UnityEditor;
using System.IO;
using System.Text;
using System;

/* Author: Mars
 * Last Modify Date: 2011/06/20
 * Introduction: Transform all script in project to UTF-8 with BOM.
 * Menu >> Tools >> Transform Script with BOM
 * 
 * BOM(Byte Order Mark) Code
 * UTF-8 >> EF BB BF >> 239 187 191
 */

public class ScriptToUnicodeWithBOM
{
	//EF BB BF
	static byte[] Const_BOMByte = new byte[3] { 0xEF, 0xBB, 0xBF };
	static ScriptToUnicodeWithBOM BOMScript = new ScriptToUnicodeWithBOM();

	[MenuItem("Tools/Transform Script with BOM %#T")]
	static void DoAddBOMtoAllUTF8File()
	{
		try
		{
			DirectoryInfo aRootDir;
			
			Debug.Log("Info: Start add BOM to UTF-8 file");
			
			aRootDir = new DirectoryInfo(Application.dataPath);
			if(!aRootDir.Exists)
				return;
			
			BOMScript.AddBOMProcessForDirectory(aRootDir);
		}
		catch
		{
			Debug.LogError("Error: ScriptToUnicodeWithBOM");
		}
		finally
		{
			Debug.Log("Info: End add BOM to UTF-8 file");
		}
	}
	[MenuItem("Tools/Transform Select Script with BOM")]
	public static void DoAddBOMtoUTF8File()
	{
		string path = Application.dataPath.Replace("Assets", AssetDatabase.GetAssetPath(Selection.activeObject));
		BOMScript.AddBOMToUTF8File(path);
	}
	// Add BOM to UTF-8 all files in Directory
	public void AddBOMProcessForDirectory(DirectoryInfo aRootDir)
	{
		FileInfo[] aFiles;
		DirectoryInfo[] aDirs;
		float progress = 0;
		
		//root's files
		aFiles = aRootDir.GetFiles();
		foreach(FileInfo fInfo in aFiles)
		{
			if(String.Compare(fInfo.Extension, ".cs", true) != 0 || fInfo.Name == "ScriptToUnicodeWithBOM.cs")
			{
				progress += 1;
				continue;
			}

			EditorUtility.DisplayProgressBar("Transform Script with BOM", "正在轉換" + fInfo.Name + "的編碼至UTF-8 with BOM...", progress / aFiles.Length);
			BOMScript.AddBOMToUTF8File(fInfo.FullName);
		}
		EditorUtility.ClearProgressBar();
		
		//sub directory
		aDirs = aRootDir.GetDirectories();
		foreach(DirectoryInfo dInfo in aDirs)
		{
			AddBOMProcessForDirectory(dInfo);
		}
	}

	// Add BOM to UTF-8 file
	public bool AddBOMToUTF8File(string aFullFileName)
	{
		FileInfo fInfo;
		StreamWriter outStream;
		StreamReader sourceStream;
		string strCode = "";
		//string aDir = "";
		
		//file not exists
		if(!File.Exists(aFullFileName))
			return false;
		
		try
		{
			fInfo = new FileInfo(aFullFileName);
			if(!fInfo.Exists)
				return false;
			
			Debug.Log("Info: Start add BOM to " + aFullFileName);
			
			//aDir = fInfo.Directory.FullName;
			//Debug.Log(aDir);			
			
			//if is UTF-8 with BOM then continue
			if(CheckUTF8BOM(aFullFileName))
			{
				Debug.Log("Info: No need add BOM to " + aFullFileName);
				return true;
			}
			
			//Do add BOM to UTF-8 file
			sourceStream = new StreamReader(aFullFileName);
			if(sourceStream == null)
			{
				Debug.LogError("Error: AddBOMToUTF8File - Create StreamReader failed.");
				return false;
			}
			
			strCode = sourceStream.ReadToEnd();			
			sourceStream.Close();
			
			outStream = new StreamWriter(File.Open(aFullFileName, FileMode.Create), Encoding.UTF8);
			outStream.Write(strCode);
			outStream.Close();
			
			Debug.Log("Info: End add BOM to " + aFullFileName);
			return true;
		}
		catch(IOException e)
		{
			Debug.LogError("Error: AddBOMToUTF8File");
			Debug.LogError(e.Message);
			return false;
		}
	}

	/* Check the UTF-8 file with or without BOM.
	 * If with BOM then return true, otherwise return false.
	 */
	public bool CheckUTF8BOM(string aFullFileName)
	{
		Stream byteStream;
		byte[] bomByte = new byte[3];
		
		//file not exists
		if(!File.Exists(aFullFileName))
			return false;
		
		try
		{
			byteStream = new FileStream(aFullFileName, FileMode.Open);
			
			bomByte.Initialize();
			byteStream.Read(bomByte, 0, bomByte.Length);
			byteStream.Close();
			
			
			if((bomByte[0] != Const_BOMByte[0]) || (bomByte[1] != Const_BOMByte[1]) || (bomByte[2] != Const_BOMByte[2]))
				return false;
			
			return true;
		}
		catch
		{
			Debug.LogError("Error: CheckUTF8BOM");
			return false;
		}
		finally
		{
		}
	}
}
