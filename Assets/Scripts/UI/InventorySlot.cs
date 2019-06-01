using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{

    [SerializeField] private Text itemNumber;
    [SerializeField] private Image itemImage;
    
    public InventoryItem item;
    public InventoryManager manager;

    public void Setup(InventoryItem newItem, InventoryManager newManager)
    {
        item = newItem;
        manager = newManager;
        if (item)
        {
            itemImage.sprite = item.itemImage;
            itemNumber.text = "" + item.count;
        }
    }

    public void OnClicked()
    {
        if (item)
        {
            manager.SetupDescriptionButton(item.itemDescription, item.usable, item);
        }
    }
}
