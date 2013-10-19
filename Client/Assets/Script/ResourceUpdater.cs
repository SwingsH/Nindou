using UnityEngine;
using System.Collections;

/// <summary>
/// 遊戲資源更新處理者, download file from hosting or re-packing files 
/// </summary>
public class ResourceUpdater
{
    private bool _isUpdating = false;
 
    //constructor
    public ResourceUpdater()
    {
    }

    // destructor
    ~ResourceUpdater()
    {
    }

    public bool IsUpdating
    {
        get { return _isUpdating; }
        set { _isUpdating = value; }
    }
}
