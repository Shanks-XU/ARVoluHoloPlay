using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

/// <summary>
/// Image Tracking
/// </summary>
public class HoloPlayerManager : MonoBehaviour
{
    public Text titleText;

    public ARSessionOrigin ARSessionOrigin;

    public ARSession ARSession;

    public GameObject ScanNode;

    public GameObject SpawnPrefab;

    private ARTrackedImageManager ImgTrackedManager;

    // Start is called before the first frame update
    private void Awake()
    {
        ImgTrackedManager = ARSessionOrigin.GetComponent<ARTrackedImageManager>();
        if (ImgTrackedManager == null)
        {
            Debug.LogError("shanks>>>>>not found ARTrackedImageManager!");
        }


        ARSession.Reset();

        titleText.text = ScenesConfig.SceneTitle;

        // 注册事件
        ScenesConfig.OnPrefabCreate = delegate
        {
            // 隐藏扫描
            ScanNode.SetActive(false);

            if (!ScenesConfig.IsKeepTracking)
            {
                // 识别后强制关闭追踪
                if (ImgTrackedManager != null)
                {
                    Debug.LogError("shanks>>>>>Close ARTrackedImageManager!");
                    ImgTrackedManager.enabled = false;
                }

            }
        };


        Debug.Log("shanks>>>>>HoloPlayScene KeepTracking =  " + ScenesConfig.IsKeepTracking);
    }

    private void OnEnable()
    {
        if (ImgTrackedManager != null)
        {
            ImgTrackedManager.trackedImagesChanged += OnTrackedImagesChanged;
        }
    }

    private void OnDisable()
    {
        if (ImgTrackedManager != null)
        {
            ImgTrackedManager.trackedImagesChanged -= OnTrackedImagesChanged;
        }
    }

    private void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.I))
        {
            Instantiate(SpawnPrefab);
        }
#endif
    }

    public void OnBackToScanScene()
    {
        SceneManager.LoadScene(ScenesConfig.MainSceneId,LoadSceneMode.Single);    
    }

    private void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach (var trackedImage in eventArgs.added)
        {
            OnImagesChanged(trackedImage);
        }
    }

    private void OnImagesChanged(ARTrackedImage referenceImage)
    {
        Debug.Log("shanks>>>>>Image name:" + referenceImage.referenceImage.name);

        //switch (referenceImage.referenceImage.name)
        //{
        //    case "10001":

        //        Instantiate(SpawnPrefab, referenceImage.transform);
        //        break;

        //    case "10002":

        //        Instantiate(SpawnPrefab, referenceImage.transform);
        //        break;

        //    case "10003":

        //        Instantiate(SpawnPrefab, referenceImage.transform);
        //        break;

        //    default:
        //        break;
        //}
    }
}
