using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelData
{
    public LevelData(string levelName)
    {
        string data = PlayerPrefs.GetString(levelName);
        if (data == "")
            return;
        string[] allData = data.Split('&');
        BestTime = float.Parse (allData [0]);
        SilverTime = float.Parse (allData [1]);
        GoldTime = float.Parse (allData[2]);
    }


    public float BestTime{set; get;}
    public float SilverTime{set; get;}
    public float GoldTime{set; get;}
}

public class MainMenu : MonoBehaviour
{
    public Sprite[] winningStreaks;
    public GameObject levelButtonPrefab;
    public GameObject levelButtonContainer;
    public GameObject shopButtonPrefab;
    public GameObject shopItemsContainer;
    private const float quaternion_constant_multiplier = 3.0f;
    private Transform cameraTransform;
    private Transform cameraDesiredLookAt;
    public Material playerMaterial;
    public Text currencyText;
    public bool nextLevelLocked = false;

     private void Start()
    {
        changePlayerSkin(GameManager.Instance.currentSkinIndex);
        currencyText.text = "currency : " + GameManager.Instance.currency.ToString();
       cameraTransform = Camera.main.transform;
       Sprite[] thumbnails = Resources.LoadAll<Sprite>("levels");
       foreach (Sprite  thumbnail in thumbnails)
       {
           GameObject container= Instantiate (levelButtonPrefab) as GameObject;
           container.GetComponent<Image>().sprite = thumbnail; 
           container.transform.SetParent(levelButtonContainer.transform,false);
           LevelData level =  new LevelData (thumbnail.name);
           string minutes = ((int) level.BestTime / 60).ToString("00");
           string seconds = (level.BestTime % 60).ToString("00.00");

           GameObject bottomPanel = container.transform.GetChild(0).GetChild(0).gameObject;
           bottomPanel.GetComponent<Text>().text = (level.BestTime != 0.0f) ? minutes + ":" + seconds : "LEVEL LOCKED";

            container.transform.GetChild(1).GetComponent<Image> ().enabled = nextLevelLocked;
            container.GetComponent<Button> ().interactable = !nextLevelLocked;
            if (level.BestTime == 0.0f)
            {
                nextLevelLocked = true; 
            }
            else if(level.BestTime < level.GoldTime)
            {
                bottomPanel.GetComponentInParent<Image>().sprite = winningStreaks[2];
            }
            else if(level.BestTime < level.SilverTime)
            {
                bottomPanel.GetComponentInParent<Image>().sprite = winningStreaks[1];
            }
            else 
            {
                bottomPanel.GetComponentInParent<Image>().sprite = winningStreaks[0];
            }
           string sceneName = thumbnail.name;
           container.GetComponent<Button> ().onClick.AddListener (() => LoadLevel(sceneName));
       }
       int playerIndex = 0;
       Sprite[] textures = Resources.LoadAll<Sprite>("characters");
       foreach (Sprite texture in textures)
       {
           int index = playerIndex;
           GameObject container = Instantiate (shopButtonPrefab) as GameObject;
           container.GetComponent<Image>().sprite = texture; 
           container.transform.SetParent(shopItemsContainer.transform,false);
           container.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = costOfSkins[index].ToString();            
           container.GetComponent<Button> ().onClick.AddListener (() => changePlayerSkin(index));
           if((GameManager.Instance.skinAvailability & 1 << index) == 1 << index)
           {
                container.transform.GetChild (0).gameObject.SetActive(false);
           }
           playerIndex++;
       }
    }
    private void Update()
    {
        if(cameraDesiredLookAt != null)
        {
            cameraTransform.rotation = Quaternion.Slerp(cameraTransform.rotation, cameraDesiredLookAt.rotation, quaternion_constant_multiplier * Time.deltaTime);
        }

    }
    private void LoadLevel(string sceneName)
    {
        SceneManager.LoadScene (sceneName);
    }
    public void LookAtMenu(Transform menuTransform)
    {
        cameraDesiredLookAt = menuTransform;
        //Camera.main.transform.LookAt (menuTransform.position);
    }
    public void changePlayerSkin(int index)
    {
        if ((GameManager.Instance.skinAvailability & 1 << index) == 1 << index)
        {
            //Debug.Log(1 << index);
            float x = (index % 4) * 0.25f;
            float y = ((int)index / 4) * 0.25f;
            if (y == 0.0f)
                y = 0.75f;
            else if (y == 0.25f)
                y = 0.50f;
            else if (y == 0.50f)
                y = 0.25f;
            else if (y == 0.75f)
                y = 0.00f;
            playerMaterial.SetTextureOffset ("_MainTex", new Vector2(x,y));
        GameManager.Instance.currentSkinIndex = index;
        GameManager.Instance.save();
        }
        else
        {
            //you don't have that skin do you want buy it
            int costOfSkin = costOfSkins[index];

            if (GameManager.Instance.currency >= costOfSkin)
            {
                GameManager.Instance.currency -= costOfSkin;
                GameManager.Instance.skinAvailability += 1 << index;
                GameManager.Instance.save();
                currencyText.text = "currency : " + GameManager.Instance.currency.ToString();
                shopItemsContainer.transform.GetChild(index).GetChild(0).gameObject.SetActive (false);
                changePlayerSkin (index);
            }
        }
    }
    private int[] costOfSkins = {150,450,600,780,
                                 900,1000,1200,1450,
                                 1500,1750,1900,2200,
                                 2500,3900,4500,5000};
}
