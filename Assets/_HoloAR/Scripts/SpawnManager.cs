using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    void Awake()
    {
        Debug.Log("shanks>>>>>Spawn Prefab");
        // 激活事件
        ScenesConfig.OnPrefabCreate();
    }
}
