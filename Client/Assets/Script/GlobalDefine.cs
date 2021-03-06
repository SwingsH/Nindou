﻿/// <summary>
/// #define 用之設定
/// </summary>
public static class GLOBAL_DEFINE
{
    // define 種類
    public static readonly string[] CONFIG = new string[]
    {
        "DEVELOP_DEBUG",        // 開發除錯版
        //"RELEASE",           // 正式版
        "KOREAN_GSTAR",         // 2013 G-Star 版本
        "SKIP_TEST_NETWORK",    // 略過檢查 c 端網路能力
        "SKIP_CONNECT_CHECK",  // 略過 server 
        //"SHOW_BATTLE_ONGUI",  // 顯示戰鬥的 OnGUI資訊
         //"UI_OFFLINE_TEST",   // UI離線測試
         //"LOCAL_SERVER",        // server 為本機
    };
}
