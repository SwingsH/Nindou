using UnityEngine;
using UnityEditor;
using System.Collections;

/// <summary>
/// 自動在 editor 設定  Scripting Define Symbols (Edit -> Project Settings-> Other Settings )
/// </summary>
/// 
[InitializeOnLoad]
public class AutoInitializeDefines
{
    static AutoInitializeDefines()
    {
        var types = new[] { BuildTargetGroup.Standalone, BuildTargetGroup.WebPlayer, BuildTargetGroup.Android, BuildTargetGroup.iPhone };
        var toInclude = GLOBAL_DEFINE.CONFIG;

        SetupConditionalCompilation(types, toInclude);
    }

    static void SetupConditionalCompilation(BuildTargetGroup[] platformTargets, string[] symbolsToInclude)
    {
        foreach (var type in platformTargets)
        {
            var hasEntry = new bool[symbolsToInclude.Length];
            var conditionals = PlayerSettings.GetScriptingDefineSymbolsForGroup(type).Trim();
            var parts = conditionals.Split(';');

            //檢查 defineS 是否被設定過
            bool needUpdate = false;

            // 這裡檢查 define 設定是否減少了
            if (symbolsToInclude.Length != parts.Length)
                needUpdate = true;

            // 這裡檢查 define 設定是否有不存在的
            if (needUpdate == false)
            {
                for (int i = 0; i < symbolsToInclude.Length; i++)
                {
                    foreach (var part in parts)
                    {
                        if (part.Trim() == symbolsToInclude[i].Trim())
                        {
                            hasEntry[i] = true;
                            break;
                        }
                    }

                    // 有一個 define 沒被偵測到 ==> 全部刷新
                    if (hasEntry[i] == false)
                    {
                        needUpdate = true;
                        break;
                    }
                }
            }

            // 沒有任何 define 需要輸入
            if (needUpdate == false)
            {
                return;
            }

            //刷新所有 define
            string newConditionals = string.Empty;
            for (int i = 0; i < hasEntry.Length; i++)
            {
                if (!hasEntry[i])
                {
                    newConditionals += (string.IsNullOrEmpty(newConditionals) ? string.Empty : ";") + symbolsToInclude[i];
                }
            }

            PlayerSettings.SetScriptingDefineSymbolsForGroup(type, newConditionals);

            Debug.Log(string.Format(" Define 設置成功, Platform {0} : {1}", type, newConditionals));
        }
    }
}