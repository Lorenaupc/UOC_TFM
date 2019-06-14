using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryCanvasManager : MonoBehaviour {

    private GameObject player;
    internal bool inventoryUsed;
    internal bool shopInventoryUsed;
    public GameObject inventoryPanel;
    public GameObject shopPanel;

    //Private things
    private Vector2 anchorMax;
    private Vector2 anchorMin;
    private Vector2 pivot;

    public Button sell;
    public Button buy;

    void Start()
    {
        anchorMax = inventoryPanel.GetComponent<RectTransform>().anchorMax;
        anchorMin = inventoryPanel.GetComponent<RectTransform>().anchorMin;
        pivot = inventoryPanel.GetComponent<RectTransform>().pivot;

        inventoryUsed = true;
        shopInventoryUsed = true;
        player = GameObject.FindGameObjectWithTag("Player");
        activeInventoryMenu();
        activeShopInventoryMenu();
    }

    void Update()
    {
        if (Input.GetButtonDown("PanelInventory"))
        {
            if (shopPanel.activeSelf) {
                activeShopInventoryMenu();
            }
            activeInventoryMenu();
        }
    }

    internal void activeInventoryMenu()
    {
        inventoryUsed = !inventoryUsed;
        if (inventoryUsed)
        {          
            inventoryPanel.SetActive(true);
            Time.timeScale = 0f;
            inventoryPanel.GetComponent<InventoryManager>().DeleteInventoryNullItems();
            inventoryPanel.GetComponent<InventoryManager>().ReloadInventoryFromExternal();
        }
        else
        {
            inventoryPanel.GetComponent<RectTransform>().anchorMax = anchorMax;
            inventoryPanel.GetComponent<RectTransform>().anchorMin = anchorMin;
            inventoryPanel.GetComponent<RectTransform>().pivot = pivot;

            inventoryPanel.SetActive(false);
            Time.timeScale = 1f;
        }
    }

    internal void activeShopInventoryMenu()
    {
        shopInventoryUsed = !shopInventoryUsed;
        if (shopInventoryUsed)
        {
            shopPanel.SetActive(true);
            sell.enabled = true;
            buy.enabled = true;
            Time.timeScale = 0f;
        }
        else
        {
            shopPanel.SetActive(false);
            sell.enabled = false;
            buy.enabled = false;
            Time.timeScale = 1f;
        }
    }

    public void SellButton()
    {
        int money = inventoryPanel.GetComponent<InventoryManager>().currentItem.sell_cost;
        inventoryPanel.GetComponent<InventoryManager>().SellItems(inventoryPanel.GetComponent<InventoryManager>().currentItem);
        player.GetComponent<PlayerMovement>().playerMoney += money;
        player.GetComponent<PlayerMovement>().UpdatePlayerMoney();
    }

    public void BuyButton()
    {
        InventoryItem itemToBuy = shopPanel.GetComponent<InventoryShopManager>().currentItem;
        int money = player.GetComponent<PlayerMovement>().playerMoney - itemToBuy.buy_cost;
        if (money >= 0)
        {
            inventoryPanel.GetComponent<InventoryManager>().AddItem(itemToBuy);
            player.GetComponent<PlayerMovement>().playerMoney -= itemToBuy.buy_cost;
            player.GetComponent<PlayerMovement>().UpdatePlayerMoney();
        }
    }
}
