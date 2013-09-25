using UnityEditor;
using UnityEngine;
using System.IO;
using System.Collections;

public class AssetbundleBuilder
{

    [MenuItem("Assets/DataUtility/Json To AssetBundle For StandAlone")]
    static void JsonToABForStandAlone()
    {
        JsonToAB(BuildTarget.StandaloneWindows);
    }
    [MenuItem("Assets/DataUtility/Json To AssetBundle For Android")]
    static void JsonToABForAndroid()
    {
        JsonToAB(BuildTarget.Android);
    }
    [MenuItem("Assets/DataUtility/Json To AssetBundle For iOS")]
    static void JsonToABForIOS()
    {
        JsonToAB(BuildTarget.iPhone);
    }

    /// <summary>
    /// 將Json檔轉成assetbundle
    /// </summary>
    static void JsonToAB(BuildTarget buildTarget)
    {
        string jsonFilePath = EditorUtility.OpenFolderPanel("Json Folder", GLOBALCONST.DIR_DATA_JSON, ""); // 使用者選擇輸入

        if (string.IsNullOrEmpty(jsonFilePath))
        {
            EditorUtility.DisplayDialog("未選取資料夾", "請選取資料夾", "我知道了");
            return;
        }

        string[] existingJsonFiles = Directory.GetFiles(jsonFilePath, "*.json", SearchOption.AllDirectories);

        int processingIndex = 0; 
        int amountOfJsonFiles = existingJsonFiles.Length;

        foreach (string filePath in existingJsonFiles)
        {
            ++processingIndex;
            CommonFunction.DebugMsgFormat("({0}/{1}) {2} 轉換中...", processingIndex, amountOfJsonFiles, filePath);

            string fileName = Path.GetFileNameWithoutExtension(filePath).ToLower();
            string encodingStr = File.ReadAllText(filePath);
            CreateDataAssetBundle(fileName, encodingStr, buildTarget);
        }
        EditorUtility.ClearProgressBar();
    }

    /// <summary>
    /// 建立資料的Assetbundle
    /// </summary>
    /// <param name="fileName">檔案名稱</param>
    /// <param name="data">要轉成assetbundle的字串</param>
    /// <param name="buildTarget">要建成哪種環境用的assetbundle</param>
    static void CreateDataAssetBundle(string fileName, string data, BuildTarget buildTarget)
    {
        CommonFunction.DebugMsgFormat("fileName = {0}", fileName);
        string outputPath = CommonFunction.GetProjectOutputPath(GLOBALCONST.DIR_ASSETBUNDLE_JSON) + Path.GetFileNameWithoutExtension(fileName) + GLOBALCONST.EXT_ASSETBUNDLE;

        Object obj = null;
        StringHolder sh = null;
        AssetDatabase.Refresh();

        sh = ScriptableObject.CreateInstance<StringHolder>();
        sh.content = data;
        string tempPath = GLOBALCONST.DIR_ASSETS + Path.GetFileNameWithoutExtension(fileName) + GLOBALCONST.EXT_ASSET;
        AssetDatabase.CreateAsset(sh, tempPath); // 建立一暫存的asset（必須建立，否則無法做成Assetbundle）
        obj = AssetDatabase.LoadAssetAtPath(tempPath, typeof(StringHolder));

        if (obj != null)
        {
            BuildPipeline.BuildAssetBundle(obj, null, outputPath, BuildAssetBundleOptions.CollectDependencies, buildTarget);
            CommonFunction.DebugMsgFormat("File {0} written", outputPath);
        }
        else
        {
            CommonFunction.DebugMsgFormat("File {0} 取不到資料", tempPath);
        }
        AssetDatabase.DeleteAsset(tempPath);
        if (sh != null) { GameObject.DestroyImmediate(sh, true); }
        AssetDatabase.Refresh();
    }
}
