using UnityEngine;
using System;

#if UNITY_ADS
using UnityEngine.Advertisements;
#else
public class ShowResult
{
    public static ShowResult Finished = new ShowResult();
    public static ShowResult Failed = new ShowResult();
    public static ShowResult Skipped = new ShowResult();
}

public class ShowOptions
{
    public Action<ShowResult> resultCallback;
}

public class Advertisement
{
    public static bool IsReady(string type)
    {
        return true;
    }

    public static void Show()
    {
        Debug.Log("Show Ad");
    }

    public static void Show(string type, ShowOptions options)
    {
        options.resultCallback(ShowResult.Skipped);
    }
}
#endif

public class AdvertisementHelper : MonoBehaviour
{
    public void ShowAd()
    {
        if (Advertisement.IsReady())
        {
            Advertisement.Show();
        }
    }
    public void ShowAd(string type, Action<ShowResult> resultCallback)
    {
        if (Advertisement.IsReady(type))
        {
            var options = new ShowOptions { resultCallback = resultCallback };
            Advertisement.Show(type, options);
        }
    }

    public void DefaultShowResult(ShowResult result)
    {
        #if UNITY_ADS
        switch (result)
        {
            case ShowResult.Finished:
                Debug.Log("The ad was successfully shown.");
                break;
            case ShowResult.Skipped:
                Debug.Log("The ad was skipped before reaching the end.");
                break;
            case ShowResult.Failed:
                Debug.LogError("The ad failed to be shown.");
                break;
        }
        #else
        Debug.Log("The ad was skipped before reaching the end.");
        #endif
    }
}
