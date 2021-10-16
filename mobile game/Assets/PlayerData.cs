using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public List<Shop.ShopItem> shopItems;

    public PlayerData(Shop shop)
    {
        shopItems = shop.ShopItemsList;
    }
}
