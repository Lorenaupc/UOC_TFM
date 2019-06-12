using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryCanvasManager : MonoBehaviour {

    private GameObject player;
    private bool inventoryUsed;
    public GameObject inventoryPanel;

    //Private things
    private Vector2 anchorMax;
    private Vector2 anchorMin;
    private Vector2 pivot;

    void Start()
    {
        anchorMax = inventoryPanel.GetComponent<RectTransform>().anchorMax;
        anchorMin = inventoryPanel.GetComponent<RectTransform>().anchorMin;
        pivot = inventoryPanel.GetComponent<RectTransform>().pivot;

        inventoryUsed = true;
        player = GameObject.FindGameObjectWithTag("Player");
        activeInventoryMenu();
    }

    void Update()
    {
        if (Input.GetButtonDown("PanelInventory"))
        {
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
}
