using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssetBundleManager : MonoBehaviour
{
    public static AssetBundle assetbundle = null;

    static Dictionary<string, AssetBundle> DicAssetBundle = new Dictionary<string, AssetBundle>();

    public static T LoadResource<T>(string sourthName, string assetBundlePath, string assetBundleGroupName) where T : Object
    {
        if (string.IsNullOrEmpty(assetBundleGroupName))
        {
            return default(T);
        }

        if (!DicAssetBundle.TryGetValue(assetBundleGroupName, out assetbundle))
        {
            assetbundle = AssetBundle.LoadFromFile(assetBundlePath + "/" + assetBundleGroupName);
            DicAssetBundle.Add(assetBundleGroupName, assetbundle);
        }

        object obj = assetbundle.LoadAsset(sourthName, typeof(T));
        var one = obj as T;
        return one;
    }
    public static AssetBundle LoadResource(string assetBundlePath, string assetBundleGroupName)
    {
        if (string.IsNullOrEmpty(assetBundleGroupName))
        {
            return null ;
        }

        if (!DicAssetBundle.TryGetValue(assetBundleGroupName, out assetbundle))
        {
            assetbundle = AssetBundle.LoadFromFile(assetBundlePath + "/" + assetBundleGroupName);
            DicAssetBundle.Add(assetBundleGroupName, assetbundle);
        }

        
        return assetbundle;
    }

    public static AsyncOperation LoadAllAsync(string assetBundlePath, string assetBundleGroupName)
    {
        if (string.IsNullOrEmpty(assetBundleGroupName))
        {
            return null;
        }

        if (!DicAssetBundle.TryGetValue(assetBundleGroupName, out assetbundle))
        {
            //assetbundle = AssetBundle.LoadFromFile(GetStreamingAssetsPath() + assetBundleGroupName);//+ ".assetbundle"
            assetbundle = AssetBundle.LoadFromFile(assetBundlePath + "/" + assetBundleGroupName);
            //+ ".assetbundle"
            DicAssetBundle.Add(assetBundleGroupName, assetbundle);
        }
        AsyncOperation async = assetbundle.LoadAllAssetsAsync();
        return async;
    }
    public static void UnLoadResource(string assetBundleGroupName)
    {
        if (DicAssetBundle.TryGetValue(assetBundleGroupName, out assetbundle))
        {
            assetbundle.Unload(true);
            if (assetbundle != null)
            {
                assetbundle = null;
            }
            DicAssetBundle.Remove(assetBundleGroupName);
            Resources.UnloadUnusedAssets();
        }
        //assetbundle.Unload(false);
        //if (assetbundle != null)
        //{
        //    assetbundle = null;
        //}
        //Resources.UnloadUnusedAssets();
    }
}