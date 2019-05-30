using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour {

    public PlayerInventory playerInventory;
    [SerializeField] private GameObject blankSlot;
    [SerializeField] private GameObject inventoryPanel;
    [SerializeField] private Text descriptionText;
    [SerializeField] private GameObject useButton;

    public void SetText(string description, bool buttonActive)
    {
        descriptionText.text = description;
        if (buttonActive) useButton.SetActive(true);
        else useButton.SetActive(false);
    }

    private void Start()
    {
        CreateInventory();
        SetText("", false);
    }

    private void CreateInventory()
    {
        if (playerInventory)
        {
            for(int i = 0; i < playerInventory.inventory.Count; i++)
            {
                GameObject instantiatedSlot = Instantiate(blankSlot, inventoryPanel.transform.position, Quaternion.identity);
                instantiatedSlot.transform.SetParent(inventoryPanel.transform);
                InventorySlot newSlot = instantiatedSlot.GetComponent<InventorySlot>();
                if (newSlot)
                {
                    newSlot.Setup(playerInventory.inventory[i], this);
                }
            }
        }
    }
}
