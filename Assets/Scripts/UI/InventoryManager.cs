using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour {

    private Tools mainCanvas;

    public PlayerInventory playerInventory;
    [SerializeField] private GameObject blankSlot;
    [SerializeField] private GameObject inventoryPanel;
    [SerializeField] private Text descriptionText;
    [SerializeField] private GameObject useButton;
    [SerializeField] private GameObject putonUI;
    [SerializeField] private GameObject numberHeld;
    public InventoryItem currentItem;

    public void SetText(string description, bool buttonActive, bool uiActive, bool numberHeldActive)
    {
        descriptionText.text = description;
        if (buttonActive) useButton.SetActive(true);
        else useButton.SetActive(false);

        if (uiActive) putonUI.SetActive(true);
        else putonUI.SetActive(false);

        if (numberHeldActive) numberHeld.SetActive(true);
        else numberHeld.SetActive(false);
    }

    private void Start()
    {
        mainCanvas = GameObject.FindGameObjectWithTag("Canvas").GetComponent<Tools>();
        CreateInventory();
        SetText("", false, false, false);
        DeleteInventoryNullItems();
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

    public void SetupDescriptionButton(string description, bool buttonActive, InventoryItem newItem, bool uiActive, bool numberHeldActive)
    {
        currentItem = newItem;
        descriptionText.text = description;

        if (newItem != null)
        {
            if (newItem.name.Contains("Seed") || newItem.name.Contains("Key"))
            {
                numberHeld.SetActive(false);
                useButton.SetActive(false);
            }
            else
            {
                useButton.SetActive(buttonActive);
                numberHeld.SetActive(numberHeldActive);
            }
        }
        else
        {
            useButton.SetActive(buttonActive);
            numberHeld.SetActive(numberHeldActive);
        }
        putonUI.SetActive(uiActive);
    }

    public void UseButtonPressed()
    {
        if (currentItem) {
            currentItem.Use();
            currentItem.count--;
            ReloadInventoryFromExternal();
            if (currentItem)
            {
                if (currentItem.isOnUI)
                {
                    mainCanvas.updateCountItemsExternal(currentItem.positionOnUI);
                }
            }
        }
    }

    public void AddItem(InventoryItem newItem)
    {
        string check = "";
        if (newItem.name.Contains("Loot"))
        {
            check = newItem.name.Remove(0, 4);           
        }
        else
        {
            check = newItem.name;
        }

        InventoryItem itemToAdd = Resources.Load<InventoryItem>(check);
        int index = -1;

        for (int i = 0; i < playerInventory.inventory.Count; i++)
        {
            if (playerInventory.inventory[i].name == check)
            {
                index = i;
                break;
            }
        }
        if (index != -1)
        {
            playerInventory.inventory[index].count++;
            if (playerInventory.inventory[index].isOnUI)
            {
                mainCanvas.updateCountItemsExternal(playerInventory.inventory[index].positionOnUI);
            }
        }
        else
        {
            if (itemToAdd.count.Equals(0))
            {
                itemToAdd.count++;
            }
            playerInventory.inventory.Add(itemToAdd);

            GameObject instantiatedSlot = Instantiate(blankSlot, inventoryPanel.transform.position, Quaternion.identity);
            instantiatedSlot.transform.SetParent(inventoryPanel.transform);
            InventorySlot newSlot = instantiatedSlot.GetComponent<InventorySlot>();
            if (newSlot)
            {
                newSlot.Setup(playerInventory.inventory[playerInventory.inventory.Count-1], this);
            }
        }
        ReloadInventoryFromExternal();
    }

    internal void ReloadInventoryFromExternal()
    {
        InventorySlot[] slots = inventoryPanel.GetComponentsInChildren<InventorySlot>();
        foreach (InventorySlot slot in slots)
        {
            if (slot.item.count == 0)
            {
                if (currentItem == slot.item)
                {
                    //currentItem = null;
                    mainCanvas.updateCountItemsExternal(currentItem.positionOnUI);
                    SetupDescriptionButton("", false, null, false, false);
                }
                playerInventory.inventory.Remove(slot.item);
                Destroy(slot.gameObject);
            }
            else
            {
                slot.Reload();
            }
        }
    }

    internal void DeleteInventoryNullItems()
    {
        InventorySlot[] slots = inventoryPanel.GetComponentsInChildren<InventorySlot>();
        foreach (InventorySlot slot in slots)
        {
            if (slot.item.count == 0)
            {
                playerInventory.inventory.Remove(slot.item);
                Destroy(slot.gameObject);
            }
        }
    }

    public void SetObjectOnFastUI()
    {
        //If not seeds
        if (useButton.activeSelf)
        {
            if (!currentItem.isOnUI)
            {
                string a = numberHeld.GetComponent<InputField>().text;
                int num = 4;
                if (a != "")
                {
                    num = int.Parse(a);
                }
                GameObject.FindGameObjectWithTag("Canvas").GetComponent<Tools>().setNewObject(currentItem, num);
                currentItem.isOnUI = true;
            }
            else
            {
                GameObject.FindGameObjectWithTag("Canvas").GetComponent<Tools>().removeObjectFromUI(currentItem);
                string a = numberHeld.GetComponent<InputField>().text;
                int num = 4;
                if (a != "")
                {
                    num = int.Parse(a);
                }
                GameObject.FindGameObjectWithTag("Canvas").GetComponent<Tools>().setNewObject(currentItem, num);
                currentItem.isOnUI = true;
            }
        }
        else
        {
            if (!currentItem.isOnUI)
            {
                GameObject.FindGameObjectWithTag("Canvas").GetComponent<Tools>().setNewObject(currentItem, 3);
                currentItem.isOnUI = true;
            }
            else
            {
                GameObject.FindGameObjectWithTag("Canvas").GetComponent<Tools>().removeObjectFromUI(currentItem);
            }
        }
    }

    internal void SellItems(InventoryItem item)
    {
        int index = -1;

        if (!item.unique)
        {
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
        }
    }
}
