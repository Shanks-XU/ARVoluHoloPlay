using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// FBX加载
/// </summary>
public class FBXLoader : MonoBehaviour
{
    public Transform RootTrans;
    private int frameIndex = -1;
    private int frameCount = 0;

    private Object assetGo = null;

    private GameObject tempGo = null;

    private AssetBundle assetbundle;

    [SerializeField]
    private Text showTitle;
    [SerializeField]
    private Text showText;

    private void Awake()
    {
#if UNITY_EDITOR // Test
        ScenesConfig.SceneType = PlaySceneType.Fanatic;
        switch (ScenesConfig.SceneType)
        {
            case PlaySceneType.Family: // 亲子
                ScenesConfig.SceneTitle = "AR亲子影像";
                ScenesConfig.AssetBundleName = "10001.assetbundle";
                break;
            case PlaySceneType.Fanatic: // 健身
                ScenesConfig.SceneTitle = "AR全息教练";
                ScenesConfig.AssetBundleName = "10002.assetbundle";
                //0.501   1.29   -0.168
                break;
            case PlaySceneType.Station: // 航天
                ScenesConfig.SceneTitle = "AR航天讲解";
                ScenesConfig.AssetBundleName = "10003.assetbundle";
                break;
            default:
                break;
        }
#endif

        switch (ScenesConfig.SceneType)
        {
            case PlaySceneType.Family: // 亲子
                transform.localPosition = new Vector3(1.144f, 1.5f, -0.1f);
                transform.localRotation = Quaternion.Euler(new Vector3(-195f, 86.8f, 1.24f));

                transform.localScale *= 100;

                RootTrans.localScale *= 0.2f;
                break;
            case PlaySceneType.Fanatic: // 健身
                transform.localPosition = new Vector3(0.37f, 1.471f, -1.331f);
                transform.localRotation = Quaternion.Euler(new Vector3(155.67f, 173.08f, -2.97f));
                transform.localScale *= 100;

                RootTrans.localScale *= 0.2f;
                break;
            case PlaySceneType.Station: // 航天
                transform.localPosition = new Vector3(0, 0, 0);
                transform.localRotation = Quaternion.Euler(new Vector3(0, 180, 0));
                transform.localScale *= 100;

                RootTrans.localScale *= 0.2f;
                break;
            default:
                break;
        }

        UnityEngine.Time.fixedDeltaTime = 0.04f;

    }

    void Start()
    {
#if !UNITY_EDITOR
        // 激活事件
        ScenesConfig.OnPrefabCreate();
#endif
        assetbundle = AssetBundle.LoadFromFile(Application.streamingAssetsPath + "/" + ScenesConfig.AssetBundleName);
        frameCount = assetbundle.GetAllAssetNames().Length;

        showTitle.text = ScenesConfig.SceneTitle;

        switch (ScenesConfig.SceneType)
        {
            case PlaySceneType.Fanatic: // 健身
                showText.text = "全息影像可以记录教练每个专业动作的全方位视角信息，学员可以按下暂停键将影像定格在某一帧，全方位的观看教练的教学，并作出相应动作";
                break;
            case PlaySceneType.Station: // 航天
                transform.GetComponent<AudioSource>().Play();
                showText.text = "AR全息影像可以应用在科技展馆、博物馆、景区介绍等场景，体验者通过手机或AR眼镜扫描二维码，在现实世界中投射AR全息影像做展品、景区的介绍，体验者可以自由视角来观看，并且可以与全息影像进行合影。";
                break;
            case PlaySceneType.Family:  // 亲子
                showText.text = "记录一段亲子的美好回忆，与普通视频录制影像不同，3D全息影像录制是三维的，可以保存任一时刻全视角的信息。父母可以通过手机，AR眼镜、VR眼镜来观看这段全息影像，将影像定格在某一时刻，全视角来观看。";
                break;
            default:
                break;
        }

    }

    void FixedUpdate()
    {
        if (tempGo != null)
        {
            Destroy(tempGo);
            tempGo = null;
            
        }

        if (assetbundle!=null)
        {
            assetbundle.Unload(true);
            assetbundle = null;
            Resources.UnloadUnusedAssets();
        }

        if (frameIndex < frameCount - 1)
        {
            frameIndex++;
        }
        else
        {
            frameIndex = 0;
        }

        string assetName = frameIndex.ToString();
        if (assetbundle==null)
        {
            assetbundle = AssetBundle.LoadFromFile(Application.streamingAssetsPath + "/" + ScenesConfig.AssetBundleName);
        }

        assetGo = assetbundle.LoadAsset(assetName);

        tempGo = Instantiate(assetGo, this.transform, false) as GameObject;
        //tempGo.transform.localScale = Vector3.one*ScenesConfig.HoloPersonScale;
        
    }

    private void OnDestroy()
    {
        tempGo = null;
        Resources.UnloadUnusedAssets();
        assetbundle.Unload(true);
    }
}
