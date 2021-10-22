using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadAssetAsync : MonoBehaviour
{
    public static LoadAssetAsync instance;
    static AsyncOperation operation;

    public static AssetBundle assetbundle;

    private float targetValue=0;

    public static int LoadScenesID = 0;

    public UnityEngine.UI.Slider loadingSlider;

    public  UnityEngine.UI.Text loadingText;

    public UnityEngine.UI.Text loadingNum;
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        //targetValue = operation.progress;
        targetValue = 1;
    }
    public  void LoadScenesAsync()
    {
        operation = SceneManager.LoadSceneAsync(LoadScenesID);
        //阻止当加载完成自动切换
        operation.allowSceneActivation = false;
        loadingText.text = "场景加载中";
        loadingSlider.value = 0;
        operation.completed += delegate (AsyncOperation a)
         {
            //允许异步加载完毕后自动切换场景
            Debug.Log("shanks>>>>>" + 99999);
             loadingSlider.value = 1;
         };
    }

    
    public void LoadAllAsync(string assetBundlePath, string assetBundleGroupName)
    {
        if (!string.IsNullOrEmpty(assetBundleGroupName)&& !string.IsNullOrEmpty(assetBundlePath))
        {
            operation = AssetBundleManager.LoadAllAsync(assetBundlePath, assetBundleGroupName);
            loadingText.text = "资源加载中";
            loadingSlider.value = 0;
            operation.completed += delegate (AsyncOperation a)
            {
                loadingSlider.value = 1;
                AssetBundleManager.UnLoadResource(assetBundleGroupName);
                LoadScenesAsync();
            };
        }
        else
        {
            LoadScenesAsync();
        }
       

      
    }
    // Update is called once per frame
    void Update()
    {

        targetValue = operation.progress;
        if (operation.progress >= 0.9f)
        {
            //operation.progress的值最大为0.9
            targetValue = 1.0f;
            
        }

        if (targetValue != loadingSlider.value)
        {
            //插值运算
            loadingSlider.value = Mathf.Lerp(loadingSlider.value, targetValue, Time.deltaTime);
            
            if (Mathf.Abs(loadingSlider.value - targetValue) < 0.01f)
            {
                loadingSlider.value = targetValue;
            }
        }

        loadingNum.text = ((int)(loadingSlider.value * 100)).ToString() + "%";

        if ((int)(loadingSlider.value * 100) >= 99)
        {
            operation.allowSceneActivation = true;
        }
    }
}
