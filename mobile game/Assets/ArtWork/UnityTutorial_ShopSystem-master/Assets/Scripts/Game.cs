using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
	#region SIngleton:Game

	public static Game Instance;

	void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else
		{
			Destroy(gameObject);
		}
	}

	#endregion

	[SerializeField] Text[] allCoinsUIText;

	
	public int Coins;

	void Start()
	{
		if (allCoinsUIText != null)
		{
			Coins += GameManager.Instance.currency;
			UpdateAllCoinsUIText();
		}
	}

	public void UseCoins(int amount)
	{
		Coins -= amount;
		GameManager.Instance.save();
	}

	public bool HasEnoughCoins(int amount)
	{
		return (Coins >= amount);
	}

	public void UpdateAllCoinsUIText()
	{
		for (int i = 0; i < allCoinsUIText.Length; i++)
		{
			allCoinsUIText[i].text = Coins.ToString();
		}
	}
	
}