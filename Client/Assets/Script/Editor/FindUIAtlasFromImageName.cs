using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections;
using System.Collections.Generic;

public class FindUIAtlasFromImageName : ScriptableWizard 
{
    static bool _firstSearchFromTool; // 是否是打開介面後的第一次搜尋
    string _findImageName = string.Empty;    
    bool _reloadResourceData = true;
    List<UIAtlas> _allUIAtlas = new List<UIAtlas>();
    List<UIAtlas> _searchResult = new List<UIAtlas>();

    [MenuItem("Tools/UI/Find UIAtlas From Image")]
    static void FindUIAtlasFromImage()
    {
        _firstSearchFromTool = true;
        ScriptableWizard.DisplayWizard<FindUIAtlasFromImageName>("Find UIAtlas From Image");
    }

    /// <summary>
    /// Draw the custom wizard.
    /// </summary>
    void OnGUI()
    {
            NGUIEditorTools.SetLabelWidth(80f);
            string newImageName = EditorGUILayout.TextField("Search for:", (_firstSearchFromTool && Selection.activeObject != null) ? Selection.activeObject.name : _findImageName);
            _reloadResourceData = GUILayout.Button("Reload UIAtlas Data", GUILayout.Width(200f));
            NGUIEditorTools.DrawSeparator();
            if (_reloadResourceData || _firstSearchFromTool) // 從Tool打開介面後的第一次搜尋要強制讀取UIAtlas
            {
                ReloadResourcesUIAtlas();
                _reloadResourceData = false;
                _firstSearchFromTool = false;
            }

            if (GUI.changed || newImageName != _findImageName)
            {
                _findImageName = newImageName;

                if (string.IsNullOrEmpty(_findImageName)) { _searchResult.Clear(); }
                else { FindUIAtlasByImageName(_findImageName); }
            }
            if (_searchResult.Count == 0)
            {
                if (!string.IsNullOrEmpty(_findImageName)) { GUILayout.Label("No matches found"); }
            }
            else
            {
                PrintOneResultAtlasData("UIAtlas Name", false);
                NGUIEditorTools.DrawSeparator();

                _searchResult.RemoveAll(item => item == null); // 移除所有null
                foreach (UIAtlas test in _searchResult)
                {
                    if (PrintOneResultAtlasData(test.name, true))
                    {
                        Selection.activeObject = test;
                        EditorUtility.SetDirty(Selection.activeObject);
                    }
                }
            }
    }

    /// <summary>
    /// 印出一筆結果資料
    /// </summary>
    /// <returns>是否要選擇該UIAtlas</returns>
    bool PrintOneResultAtlasData(string uiAtlasName, bool hasSelectButton)
    {
        bool retVal = false;

        GUILayout.BeginHorizontal();
        {
            GUILayout.Label(uiAtlasName);

            if (hasSelectButton)
            {
                retVal = GUILayout.Button("Select", GUILayout.Width(60f));
            }
            else
            {
                GUILayout.Space(60f);
            }
        }
        GUILayout.EndHorizontal();
        return retVal;
    }

    void FindUIAtlasByImageName(string needSearchImageName)
    {
        _searchResult.Clear();
        _allUIAtlas.RemoveAll(item => item == null); // 移除所有null
        foreach (UIAtlas oneAtlas in _allUIAtlas)
        {
            if (oneAtlas.GetSprite(needSearchImageName) != null)
            {
                _searchResult.Add(oneAtlas);
            }
        }
    }

    void ReloadResourcesUIAtlas()
    {
        DirectoryInfo aRootDir;

        string uiAtlasPath = string.Format("{0}{1}{2}{3}", CommonFunction.GetProjectPath(), GLOBALCONST.DIR_ASSETS, GLOBALCONST.DIR_RESOURCES, GLOBALCONST.DIR_RESOURCES_NGUI_ATLAS);

        CommonFunction.DebugMsgFormat("Info: Reload UIAtlas from {0}", uiAtlasPath);
        aRootDir = new DirectoryInfo(uiAtlasPath);
        if (!aRootDir.Exists)
            return;

        _allUIAtlas.Clear();
        ReloadResourcesUIAtlas(aRootDir);
    }

    void ReloadResourcesUIAtlas(DirectoryInfo dirInfo)
    {
        FileInfo[] allFileInfos = dirInfo.GetFiles("*.prefab");
        float progress = 0;

        CommonFunction.DebugMsgFormat("Info: Reload UIAtlas from {0}", dirInfo.Name);

        foreach (FileInfo fInfo in allFileInfos)
        {
            UIAtlas temp = ResourceStation.LoadUIAtlasFromResource(fInfo.Name.Substring(0, fInfo.Name.Length - 7));
            if (temp != null)
            {
                _allUIAtlas.Add(temp);
            }
            progress += 1;
            EditorUtility.DisplayProgressBar("Find UIAtlas From Image", 
                string.Format("正在取得 {0} 資料夾中的 {1} prefab內的UIAtlas", dirInfo.Name, fInfo.Name),
                progress / allFileInfos.Length);
        }
        EditorUtility.ClearProgressBar();

        //sub directory
        DirectoryInfo[] allSubDirInfos = dirInfo.GetDirectories();
        foreach (DirectoryInfo dInfo in allSubDirInfos)
        {
            ReloadResourcesUIAtlas(dInfo);
        }
    }
}
