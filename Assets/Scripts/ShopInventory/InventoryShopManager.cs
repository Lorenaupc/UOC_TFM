using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class InventoryShopManager : MonoBehaviour {
    
    public PlayerInventory playerInventory;
    [SerializeField] private GameObject blankSlot;
    [SerializeField] private GameObject inventoryPanel;
    [SerializeField] private Text descriptionText;
    public InventoryItem currentItem;

    public void SetText(string description)
    {
        descriptionText.text = description;
    }

    private void Start()
    {
        CreateInventory();
        SetText("");
    }

    private void CreateInventory()
    {
        if (playerInventory)
        {
            for(int i = 0; i < playerInventory.inventory.Count; i++)
            {
                GameObject instantiatedSlot = Instantiate(blankSlot, inventoryPanel.transform.position, Quaternion.identity);
                instantiatedSlot.transform.SetParent(inventoryPanel.transform);
                InventoryShopSlot newSlot = instantiatedSlot.GetComponent<InventoryShopSlot>();
                if (newSlot)
                {
                    newSlot.Setup(playerInventory.inventory[i], this);
                }
            }
        }
    }

    public void SetupDescriptionButton(string description, InventoryItem newItem)
    {
        currentItem = newItem;
        descriptionText.text = description;
    }

    /*internal InventoryItem BuyItems(InventoryItem item)
    {
        int index = -1;

        for (int i = 0; i < playerInventory.inventory.Count; i++)
        {
            if (playerInventory.inventory[i].name == item.name)
            {
                index = i;
                break;
            }
        }
        if (index != -1)
        {
            playerInventory.inventory[index].count--;
            if (playerInventory.inventory[index].count == 0)
            {
                GameObject.FindGameObjectWithTag("Canvas").GetComponent<Tools>().removeObjectFromUI(playerInventory.inventory[index]);
                DeleteInventoryNullItems();
            }
            else if (playerInventory.inventory[index].isOnUI)
            {
                mainCanvas.updateCountItemsExternal(playerInventory.inventory[index].positionOnUI);
            }
            ReloadInventoryFromExternal();
        }
    }*/
}
