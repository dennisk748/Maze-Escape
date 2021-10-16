using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

public class Shop : MonoBehaviour
{
	#region Singlton:Shop

	public static Shop Instance;

	void Awake()
	{
		if (Instance == null)
			Instance = this;
		else
			Destroy(gameObject);
	}

	#endregion

	[System.Serializable]
	public class ShopItem
	{
		//public Sprite Image;
		public int Price;
		public bool IsPurchased = false;
	}

	//public Sprite[] image;
	public List<Sprite> image;
	public List<ShopItem> ShopItemsList;
	[SerializeField] Animator NoCoinsAnim;
	public Material playerMaterial;
	public Material[] material;
	public Renderer rend;
	public GameObject nocoins;
	



	[SerializeField] GameObject ItemTemplate;
	GameObject g;
	[SerializeField] Transform ShopScrollView;
	[SerializeField] Text[] allCoinsUIText;
	[SerializeField] GameObject ShopPanel;
	Button buyButton;


	void Start()
	{
		//mine
		nocoins.SetActive(false);

		rend.enabled = true;
		rend.sharedMaterial = material[0];
		PlayerData data = SaveSystem.LoadData();
		if (data != null)
		{
			ShopItemsList = data.shopItems;
		}
		for (int i = 0; i < allCoinsUIText.Length; i++)
		{
			allCoinsUIText[i].text = GameManager.Instance.currency.ToString();
		}
		//changePlayerSkin(GameManager.Instance.currentSkinIndex);

		int len = ShopItemsList.Count;
		int playerIndex = 0;
		for (playerIndex = 0; playerIndex < len;)
		{
			int index = playerIndex;
			g = Instantiate(ItemTemplate, ShopScrollView);
			g.transform.GetChild(0).GetComponent<Image>().sprite = image[index];
			g.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = ShopItemsList[index].Price.ToString();
			buyButton = g.transform.GetChild(2).GetComponent<Button>();
			buyButton.onClick.AddListener(() => onButtonClicked (index));
			if ((GameManager.Instance.skinAvailability & 1 << index) == 1 << index)
			{
				DisableBuyButton();
				ShopItemsList[index].IsPurchased = true;
				SaveSystem.SaveShop(this);
			}
			if (ShopItemsList[index].IsPurchased)
			{
				DisableBuyButton();
			}
			playerIndex++;

		}
	}



	/**int len = ShopItemsList.Count;
for (int i = 0; i < len; i++) {
	g = Instantiate (ItemTemplate, ShopScrollView);
	g.transform.GetChild (0).GetComponent <Image> ().sprite = ShopItemsList [i].Image;
	g.transform.GetChild (1).GetChild (0).GetComponent <Text> ().text = ShopItemsList [i].Price.ToString ();
	buyButton = g.transform.GetChild (2).GetComponent <Button> ();
	if (ShopItemsList [i].IsPurchased) {
		DisableBuyButton ();
	}
	buyButton.AddEventListener (i, OnShopItemBtnClicked);
}**/


	//void OnShopItemBtnClicked (int itemIndex)
	//{
	//Sprite[] textures = Resources.LoadAll<Sprite>("player");
	//if (Game.Instance.HasEnoughCoins (costOfSkins[itemIndex])) {
	//Game.Instance.UseCoins (costOfSkins[itemIndex]);
	//purchase Item
	//ShopItemsList [itemIndex].IsPurchased = true;
	//SaveSystem.SaveShop(this);
	//GameManager.Instance.save();

	//disable the button
	//buyButton = ShopScrollView.GetChild (itemIndex).GetChild (2).GetComponent <Button> ();
	//DisableBuyButton ();
	//change UI text: coins
	//Game.Instance.UpdateAllCoinsUIText ();
	//changePlayerSkin(itemIndex);

	//add avatar
	//Profile.Instance.AddAvatar (textures[itemIndex]);
	//} else {
	//NoCoinsAnim.SetTrigger ("NoCoins");
	//Debug.Log ("You don't have enough coins!!");
	//}
	//}

	void DisableBuyButton()
	{
		buyButton.interactable = false;
		buyButton.transform.GetChild(0).GetComponent<Text>().text = "PURCHASED";
	}
	/*---------------------Open & Close shop--------------------------*/
	public void OpenShop()
	{
		ShopPanel.SetActive(true);
	}

	public void CloseShop()
	{
		ShopPanel.SetActive(false);
	}


	//mine
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
			//playerMaterial.SetTextureOffset("_MainTex", new Vector2(x, y));
			rend.sharedMaterial = material[index];
			GameManager.Instance.currentSkinIndex = index;
			GameManager.Instance.save();
		}
		else
		{
			GameManager.Instance.skinAvailability += 1 << index;
			GameManager.Instance.save();
			changePlayerSkin(index);
		}
	}
	public void onButtonClicked(int index)
	{
		int costOfSkin = ShopItemsList[index].Price;

		if (GameManager.Instance.currency >= costOfSkin)
		{
			GameManager.Instance.currency -= costOfSkin;
			ShopItemsList[index].IsPurchased = true;
			SaveSystem.SaveShop(this);
			GameManager.Instance.save();
			//add avatar
			Profile.Instance.AddAvatar(image[index]);
			buyButton = ShopScrollView.GetChild(index).GetChild(2).GetComponent<Button>();
			DisableBuyButton();
			GameManager.Instance.skinAvailability += 1 << index;
			GameManager.Instance.save();
			for (int i = 0; i < allCoinsUIText.Length; i++)
			{
				allCoinsUIText[i].text = GameManager.Instance.currency.ToString();
			}
			changePlayerSkin(index);

		}
		else
		{
			nocoins.SetActive(true);
			NoCoinsAnim.SetTrigger("NoCoins");
			Debug.Log("You don't have enough coins!!");
		}
	}
}
