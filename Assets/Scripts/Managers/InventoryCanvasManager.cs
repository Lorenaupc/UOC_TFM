using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryCanvasManager : MonoBehaviour {

    private GameObject player;
    private bool inventoryUsed;
    public GameObject inventoryPanel;

    void Start()
    {
        inventoryUsed = false;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (Input.GetButtonDown("PanelInventory"))
        {
            activeInventoryMenu();
        }
    }

    private void activeInventoryMenu()
    {
        inventoryUsed = !inventoryUsed;
        if (inventoryUsed)
        {
            inventoryPanel.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            inventoryPanel.SetActive(false);
            Time.timeScale = 1f;
        }
    }
}
