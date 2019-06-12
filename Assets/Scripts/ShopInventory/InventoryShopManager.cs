﻿using System.Collections;
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
    public Button sell;
    public Button buy;

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

    public void SetupDescriptionButton(string description)
    {
        descriptionText.text = description;
    }

    //faltan los botones de vender y comprar
}
