using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryShopSlot : MonoBehaviour {
    
    [SerializeField] private Image itemImage;
    
    public InventoryItem item;
    public InventoryShopManager manager;

    public void Setup(InventoryItem newItem, InventoryShopManager newManager)
    {
        item = newItem;
        manager = newManager;
        if (item)
        {
            itemImage.sprite = item.itemImage;
        }
    }

    public void OnClicked()
    {
        if (item)
        {
            manager.SetupDescriptionButton(item.itemDescription);
        }
    }
}
