using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

public class AdsManager : MonoBehaviour,IUnityAdsListener
{

    string placement = "rewardedVideo";
    string placementVideo = "video";
    public Text heartcounter;



    // Start is called before the first frame update
    void Start()
    {
        Advertisement.AddListener(this);
        Advertisement.Initialize("3830559", true);

    }

    public void ShowAd(string placement)
    {
        Advertisement.Show(placement);
    }

    public void OnUnityAdsDidError(string message)
    {
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        if(showResult == ShowResult.Finished)
        {
            //addplayerlifes;
            GameManager.Instance.lives += 1;
            GameManager.Instance.save();
            
            heartcounter.text = GameManager.Instance.lives.ToString();
        }
        else if(showResult == ShowResult.Failed)
        {
            //error
            Debug.Log("no internet connection");
        }
    }

    public void OnUnityAdsDidStart(string placementId)
    {
    }

    public void OnUnityAdsReady(string placementId)
    {
    }
    
}
