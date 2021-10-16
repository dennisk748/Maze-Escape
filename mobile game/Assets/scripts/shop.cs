using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class shop : MonoBehaviour
{
    public GameObject shopButtonPrefab;
    public GameObject shopItemsContainer;
    public Material playerMaterial;
    public Text currencyText;
    // Start is called before the first frame update
    void Start()
    {
        changePlayerSkin(GameManager.Instance.currentSkinIndex);
        currencyText.text = "currency : " + GameManager.Instance.currency.ToString();


        int playerIndex = 0;
        Sprite[] textures = Resources.LoadAll<Sprite>("player");
        foreach (Sprite texture in textures)
        {
            int index = playerIndex;
            GameObject container = Instantiate(shopButtonPrefab) as GameObject;
            container.GetComponent<Image>().sprite = texture;
            container.transform.SetParent(shopItemsContainer.transform, false);
            container.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = costOfSkins[index].ToString();
            container.GetComponent<Button>().onClick.AddListener(() => changePlayerSkin(index));
            if ((GameManager.Instance.skinAvailability & 1 << index) == 1 << index)
            {
                container.transform.GetChild(0).gameObject.SetActive(false);
            }
            playerIndex++;
        }
    }



    // Update is called once per frame
    void Update()
    {
        
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
                    y = 0.0f;
                playerMaterial.SetTextureOffset("_MainTex", new Vector2(x, y));
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
                    shopItemsContainer.transform.GetChild(index).GetChild(0).gameObject.SetActive(false);
                    changePlayerSkin(index);
                }
            }
        }
        private int[] costOfSkins = {150,450,600,780,
                                         900,1000,1200,1450,
                                         1500,1750,1900,2200,
                                         2500,3900,4500,5000};
}

