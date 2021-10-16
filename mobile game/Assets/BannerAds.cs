using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class BannerAds : MonoBehaviour
{
    string gameId = "3830559";
    string placementId = "banner";
    bool tstmode = true;
    public BannerPosition bannerPosition;
    // Start is called before the first frame update
    IEnumerator Start()
    {
        Advertisement.Initialize(gameId, tstmode);

        while (!Advertisement.IsReady(placementId))
            yield return null;
        Advertisement.Banner.SetPosition(bannerPosition);
        Advertisement.Banner.Show(placementId);
    }
  
}
