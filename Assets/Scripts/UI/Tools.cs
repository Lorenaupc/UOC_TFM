﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tools : MonoBehaviour {

    static Tools instance;

    internal InventoryItem thirdItem;
    internal InventoryItem fourthItem;
    internal InventoryItem fifthItem;
    internal InventoryItem sixItem;

    internal InventoryManager inventoryManager;
    public GameObject inventoriesManager;

    internal string currentTool;

    public Image objectthird;
    public Text textthird;

    public Image objectfour;
    public Text textfour;

    public Image objectfive;
    public Text textfive;

    public Image objectsix;
    public Text textsix;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        currentTool = "";

        objectfour.enabled = false;
        objectfive.enabled = false;
        objectsix.enabled = false;

        textthird.enabled = false;
        textfour.enabled = false;
        textfive.enabled = false;
        textsix.enabled = false;

        inventoryManager = inventoriesManager.GetComponent<InventoryCanvasManager>().inventoryPanel.GetComponent<InventoryManager>();
    }

    public void setHoe()
    {
        currentTool = "Hoe";
    }

    public void setWater()
    {
        currentTool = "Water";
    }

    public void setSeed()
    {
        currentTool = "Seed";
    }

    public void setNewObject(InventoryItem newItem, int position)
    {
        switch (position)
        {
            case 3:

                if (thirdItem != null)
                {
                    thirdItem.isOnUI = false;
                    thirdItem.positionOnUI = 0;
                }

                thirdItem = newItem;
                objectthird.sprite = thirdItem.itemImage;
                textthird.text = thirdItem.count.ToString();
                newItem.positionOnUI = 3;

                textthird.enabled = true;

                break;
            case 4:

                if (fourthItem != null)
                {
                    fourthItem.isOnUI = false;
                    fourthItem.positionOnUI = 0;
                }

                fourthItem = newItem;
                objectfour.sprite = fourthItem.itemImage;
                textfour.text = fourthItem.count.ToString();
                newItem.positionOnUI = 4;

                objectfour.enabled = true;
                textfour.enabled = true;

                break;
            case 5:

                if (fifthItem != null)
                {
                    fifthItem.isOnUI = false;
                    fifthItem.positionOnUI = 0;
                }

                fifthItem = newItem;
                objectfive.sprite = fifthItem.itemImage;
                textfive.text = fifthItem.count.ToString();
                newItem.positionOnUI = 5;

                objectfive.enabled = true;
                textfive.enabled = true;

                break;
            case 6:

                if (sixItem != null)
                {
                    sixItem.isOnUI = false;
                    sixItem.positionOnUI = 0;
                }

                sixItem = newItem;
                objectsix.sprite = sixItem.itemImage;
                textsix.text = sixItem.count.ToString();
                newItem.positionOnUI = 6;

                objectsix.enabled = true;
                textsix.enabled = true;

                break;
        }
    }

    public void removeObjectFromUI(InventoryItem item)
    {
        bool finished = false;

        if (thirdItem != null)
        {
            if (thirdItem.Equals(item))
            {
                objectthird.sprite = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().sproutSprite;
                textthird.text = "";
                thirdItem = null;
                item.positionOnUI = 0;
                item.isOnUI = false;

                textthird.enabled = false;

                finished = true;
            }
        }

        if (fourthItem != null && !finished)
        {
            if (fourthItem.Equals(item))
            {
                objectfour.sprite = null;
                textfour.text = "";
                fourthItem = null;
                item.positionOnUI = 0;
                item.isOnUI = false;

                objectfour.enabled = false;
                textfour.enabled = false;

                finished = true;
            }
        }
        if (fifthItem != null && !finished)
        {
            if (fifthItem.Equals(item))
            {
                objectfive.sprite = null;
                textfive.text = "";
                fifthItem = null;
                item.positionOnUI = 0;
                item.isOnUI = false;
                
                objectfive.enabled = false;
                textfive.enabled = false;

                finished = true;
            }
        }
        if (sixItem != null && !finished)
        {
            if (sixItem.Equals(item))
            {
                objectsix.sprite = null;
                textsix.text = "";
                sixItem = null;
                item.positionOnUI = 0;
                item.isOnUI = false;
                
                objectsix.enabled = false;
                textsix.enabled = false;
            }
        }
    }

    internal void updateCountItemsExternal(int position)
    {
        switch (position)
        {
            case 3:
                textthird.text = thirdItem.count.ToString();
                if (thirdItem.count == 0)
                {
                    objectthird.sprite = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().sproutSprite;
                    textthird.text = "";
                    thirdItem.positionOnUI = 0;
                    thirdItem.isOnUI = false;

                    textthird.enabled = false;
                    thirdItem = null;
                }
                break;

            case 4:
                textfour.text = fourthItem.count.ToString();
                if (fourthItem.count == 0)
                {
                    objectfour.sprite = null;
                    textfour.text = "";
                    fourthItem.positionOnUI = 0;
                    fourthItem.isOnUI = false;

                    objectfour.enabled = false;
                    textfour.enabled = false;

                    fourthItem = null;
                }

                break;
            case 5:

                textfive.text = fifthItem.count.ToString();
                if (fifthItem.count == 0)
                {
                    objectfive.sprite = null;
                    textfive.text = "";
                    fifthItem.positionOnUI = 0;
                    fifthItem.isOnUI = false;

                    objectfive.enabled = false;
                    textfive.enabled = false;

                    fifthItem = null;
                }

                break;
            case 6:

                textsix.text = sixItem.count.ToString();
                if (sixItem.count == 0)
                {
                    objectsix.sprite = null;
                    textsix.text = "";
                    sixItem.positionOnUI = 0;
                    sixItem.isOnUI = false;

                    objectsix.enabled = false;
                    textsix.enabled = false;

                    sixItem = null;
                }

                break;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown("1"))
        {
            setWater();
        }
        else if (Input.GetKeyDown("2"))
        {
            setHoe();
        }
        else if (Input.GetKeyDown("3"))
        {
            setSeed();
        }
        else if (Input.GetKeyDown("4") && !inventoryManager.isActiveAndEnabled)
        {
            if (fourthItem != null)
            {
                fourthItem.Use();
                fourthItem.count--;
                textfour.text = fourthItem.count.ToString();
                if (fourthItem.count == 0)
                {
                    objectfour.sprite = null;
                    textfour.text = "";
                    fourthItem.positionOnUI = 0;
                    fourthItem.isOnUI = false;

                    objectfour.enabled = false;
                    textfour.enabled = false;

                    fourthItem = null;
                }
                if (inventoryManager.isActiveAndEnabled)
                {
                    inventoryManager.ReloadInventoryFromExternal();
                }
            }
        }
        else if (Input.GetKeyDown("5") && !inventoryManager.isActiveAndEnabled)
        {
            if (fifthItem != null)
            {
                fifthItem.Use();
                fifthItem.count--;
                textfive.text = fifthItem.count.ToString();
                if (fifthItem.count == 0)
                {
                    objectfive.sprite = null;
                    textfive.text = "";
                    fifthItem.positionOnUI = 0;
                    fifthItem.isOnUI = false;

                    objectfive.enabled = false;
                    textfive.enabled = false;

                    fifthItem = null;
                }
                if (inventoryManager.isActiveAndEnabled)
                {
                    inventoryManager.ReloadInventoryFromExternal();
                }
            }
        }
        else if (Input.GetKeyDown("6") && !inventoryManager.isActiveAndEnabled)
        {
            if (sixItem != null)
            {
                sixItem.Use();
                sixItem.count--;
                textsix.text = sixItem.count.ToString();
                if (sixItem.count == 0)
                {
                    objectsix.sprite = null;
                    textsix.text = "";
                    sixItem.positionOnUI = 0;
                    sixItem.isOnUI = false;

                    objectsix.enabled = false;
                    textsix.enabled = false;

                    sixItem = null;
                }
                if (inventoryManager.isActiveAndEnabled)
                {
                    inventoryManager.ReloadInventoryFromExternal();
                }
            }
        }
    }
}
