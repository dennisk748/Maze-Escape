using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

public class AdsManagerShop : MonoBehaviour,IUnityAdsListener
{

    string placement = "rewardedVideo";
    string placementVideo = "video";
    int coinsToAdd = 500;
    [SerializeField] Text[] allCoinsUIText;



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
            GameManager.Instance.currency += coinsToAdd;
            GameManager.Instance.save();
            for (int i = 0; i < allCoinsUIText.Length; i++)
            {
                if(allCoinsUIText[i] != null)
                allCoinsUIText[i].text = GameManager.Instance.currency.ToString();
            }
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
