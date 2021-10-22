using UnityEngine;
using UnityEngine.SceneManagement;

public class FirstSceneManager : MonoBehaviour
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="index"></param>
    public void OnSceneSelected(int index)
    {
        ScenesConfig.SceneType = (PlaySceneType)index;
        Debug.Log("shanks>>>>>PlayScene Type = " + ScenesConfig.SceneType);

        SceneManager.LoadScene(ScenesConfig.MainSceneId, LoadSceneMode.Single);
    }
}
