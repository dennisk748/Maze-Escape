using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class CoinCollection : MonoBehaviour
{
    //[SerializeField] GameObject coinNumPrefab;
    public AudioSource coinsound;
    public Camera cam;
    public Canvas canvas;
    public GameObject pickUp;
    public GameObject Collectable_type;
    public GameObject newCollectionUiImage;
    public Vector2 startingPos;
    public Vector2 endingpos;
    public Vector2 worldToScreen;
    public float speed = 1.0f;

    private void Start()
    {
        ScoringSystem.points = 0;
        ScoringSystem.lives = GameManager.Instance.lives;
    }
    private void OnTriggerEnter(Collider other)
    {
         if(other.tag == "Coins")
         {
            pickUp = other.gameObject;
            Collectable_type = other.GetComponent<CollectibleType>().CollectType;

            worldToScreen = cam.WorldToScreenPoint(other.transform.position);

            //Show (+7) number
            //Destroy(Instantiate(coinNumPrefab, worldToScreen, Quaternion.identity), 1f);

            newCollectionUiImage = Instantiate(Collectable_type, canvas.transform, false);


            string resourceType = other.GetComponent<CollectibleType>().ResourceType;

            newCollectionUiImage.GetComponent<PickUpUiMove>().SetResourceType(resourceType);

            startingPos = newCollectionUiImage.GetComponent<RectTransform>().anchoredPosition;
            startingPos = worldToScreen;
            coinsound.Play();
            ScoringSystem.points += 10;
            Destroy(other.gameObject);
            GameManager.Instance.currency += 10;
            GameManager.Instance.save();

        }else if(other.tag == "Heart")
        {
            pickUp = other.gameObject;
            Collectable_type = other.GetComponent<CollectibleType>().CollectType;

            worldToScreen = cam.WorldToScreenPoint(other.transform.position);

            //Show (+7) number
            //Destroy(Instantiate(coinNumPrefab, worldToScreen, Quaternion.identity), 1f);

            newCollectionUiImage = Instantiate(Collectable_type, canvas.transform, false);


            string resourceType = other.GetComponent<CollectibleType>().ResourceType;

            newCollectionUiImage.GetComponent<PickUpUiMove>().SetResourceType(resourceType);

            startingPos = newCollectionUiImage.GetComponent<RectTransform>().anchoredPosition;
            startingPos = worldToScreen;

            ScoringSystem.lives += 1;
            GameManager.Instance.lives += 1;
            GameManager.Instance.save();
        } 


        //Destroy(other.gameObject);
    }

}
