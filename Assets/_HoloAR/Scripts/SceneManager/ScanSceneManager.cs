using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScanSceneManager :MonoBehaviour
{
    // 单独打包用
    public PlaySceneType holoPlayType;

    /// <summary>
    /// Canvas
    /// </summary>
    public GameObject Canvas_Family;
    public GameObject Canvas_Fanatic;
    public GameObject Canvas_Station;
    /// <summary>
    /// 
    /// </summary>
    public Toggle keepTrackingToggle;

    private ScreenOrientation nowOrientation;

    private void Awake()
    {
        ScenesConfig.SceneType = holoPlayType;

        switch (ScenesConfig.SceneType)
        {
            case PlaySceneType.Fanatic:
                Canvas_Family.SetActive(false);
                Canvas_Fanatic.SetActive(true);
                Canvas_Station.SetActive(false);
                break;
            case PlaySceneType.Station:
                Canvas_Family.SetActive(false);
                Canvas_Fanatic.SetActive(false);
                Canvas_Station.SetActive(true);
                break;
            case PlaySceneType.Family:
                Canvas_Family.SetActive(true);
                Canvas_Fanatic.SetActive(false);
                Canvas_Station.SetActive(false);
                break;
            default:
                break;
        }
    }

    private void Start()
    {
        // 默认关闭
        ScenesConfig.IsKeepTracking = false;

        nowOrientation = Screen.orientation;

#if UNITY_IPHONE
        SetIOSFirstAdapt();
#endif
    }

    public void OnGoButtonClicked()
    {
        //switch (holoPlayType)
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

        SceneManager.LoadScene(ScenesConfig.PlaySceneId, LoadSceneMode.Single);
    }


    private void Update()
    {
        if (Screen.orientation!=nowOrientation)//屏幕旋转了
        {
            nowOrientation = Screen.orientation;
#if UNITY_IPHONE
            SetIOSFirstAdapt();
#endif

        }
    }


    ///// <summary>
    ///// 主页logo图
    ///// </summary>
    //public Transform logo;

    /// <summary>
    /// 
    /// </summary>
    private void SetIOSFirstAdapt()
    {
        //if (SystemInfo.deviceModel.ToLower().Trim().Substring(0, 3) == "ipa") //如果是iPad
        //{
        //    //变大图标

        //    logo.GetComponent<RectTransform>().sizeDelta *= 2;
        //}
        //else
        //{
        //    logo.GetComponent<RectTransform>().sizeDelta = new Vector2(512, 256);
        //}
        //if (nowOrientation == ScreenOrientation.LandscapeLeft || nowOrientation == ScreenOrientation.LandscapeRight)
        //{
        //    logo.localPosition = new Vector3(0, 150, 0);
        //    logo.GetChild(0).GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -100);
        //}
        //else
        //{
        //    logo.localPosition = new Vector3(0, 100, 0);
        //    logo.GetChild(0).GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -250);
        //}
    }


    public void OnKeepTrackingToggle(bool isOn)
    {
        ScenesConfig.IsKeepTracking = isOn;
        Debug.Log("shanks>>>>>MainScene KeepTracking =  " + ScenesConfig.IsKeepTracking);
    }

    public void OnBackToFirstScene()
    {
        SceneManager.LoadScene(ScenesConfig.FristSceneId, LoadSceneMode.Single);
    }
}
