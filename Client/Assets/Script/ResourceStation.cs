using UnityEngine;
using System.Collections;

/// <summary>
/// 資料存取統一介面, 
/// 統一使用此介面, 索取者可以不用得知資源來源 : Resources/ , unity caching, file system
/// </summary>
public class ResourceStation {

    //constructor
    public ResourceStation()
    {
    }

    // destructor
    ~ResourceStation()
    {
    }
}
