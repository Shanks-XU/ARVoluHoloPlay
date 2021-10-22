using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

public class HoloPlayerDetectPlane : MonoBehaviour
{
    public GameObject ScanNode;

    public Text titleText;

    public ARSession ARSession;

    // Start is called before the first frame update
    void Awake()
    {
        ARSession.Reset();
        titleText.text = ScenesConfig.SceneTitle;

        ScenesConfig.OnPrefabCreate = delegate
        {
            // 隐藏扫描
            ScanNode.SetActive(false);
        };
    }

    public void OnBackToScanScene()
    {
        SceneManager.LoadScene(ScenesConfig.MainSceneId, LoadSceneMode.Single);
    }
}
