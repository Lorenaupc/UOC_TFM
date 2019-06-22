using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    //Singleton
    static GameManager instance;

    private GameObject player;
    private GameObject canvas;

    public Image sproutImage; 
    public Sprite sproutSprite; 

    public Sprite groundObject;
    public Sprite seedObject;
    public Sprite growSeedObject;
    public Sprite wateredSeedObject;
    public Sprite normalGrassObject;
    public Sprite grassObject;

    //Inventory panel
    public GameObject inventoryPanel;
    public GameObject shopInventoryPanel;

    //Shops inventories
    public PlayerInventory fruitsInventory;
    public PlayerInventory seedsInventory;
    public PlayerInventory potionsInventory;

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

    private void Start()
    {
        canvas = GameObject.FindGameObjectWithTag("Canvas");
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
            if (hit.collider != null)
            {
                if (hit.collider.tag.Equals("NormalGrass") || hit.collider.tag.Equals("River") || hit.collider.tag.Equals("Ground") || hit.collider.tag.Equals("Seed"))
                {
                    if (canvas.GetComponent<Tools>().currentTool != null)
                    {
                        switch (canvas.GetComponent<Tools>().currentTool)
                        {
                            case ("Hoe"):
                                if (hit.collider.tag == "NormalGrass")
                                {
                                    hit.collider.gameObject.GetComponent<SpriteRenderer>().sprite = groundObject;
                                    hit.collider.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 1;
                                    hit.collider.gameObject.AddComponent<Ground>();
                                    hit.collider.gameObject.GetComponent<Ground>().normalGrassObject = normalGrassObject;
                                    hit.collider.tag = "Ground";
                                }
                                break;
                            case ("Water"):
                                if (hit.collider.tag == "Seed")
                                {
                                    if (player.GetComponent<Water>().decreaseWaterCan())
                                    {
                                        hit.collider.gameObject.GetComponent<Seed>().watered = true;
                                    }
                                }
                                else if (hit.collider.tag == "River")
                                {
                                    player.GetComponent<Water>().fillWaterCan();
                                }
                                break;
                            case ("Seed"):
                                if (hit.collider.tag == "Ground")
                                {
                                    if (sproutImage.sprite != sproutSprite)
                                    {
                                        Destroy(hit.collider.gameObject.GetComponent<Ground>());

                                        hit.collider.gameObject.GetComponent<SpriteRenderer>().sprite = seedObject;
                                        hit.collider.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 1;

                                        hit.collider.gameObject.GetComponent<BoxCollider2D>().isTrigger = false;

                                        hit.collider.gameObject.AddComponent<Seed>();
                                        hit.collider.gameObject.GetComponent<Seed>().normalGrassObject = normalGrassObject;
                                        hit.collider.gameObject.GetComponent<Seed>().growSeed = growSeedObject;
                                        hit.collider.gameObject.GetComponent<Seed>().wateredSeed = wateredSeedObject;
                                        hit.collider.gameObject.GetComponent<Seed>().type = typeOfSeed();

                                        hit.collider.tag = "Seed";

                                        //count
                                        GameObject.FindGameObjectWithTag("Canvas").GetComponent<Tools>().thirdItem.count--;
                                        GameObject.FindGameObjectWithTag("Canvas").GetComponent<Tools>().updateCountItemsExternal(3);
                                    }
                                    else
                                    {
                                        GameObject.FindGameObjectWithTag("Canvas").GetComponent<DialogBoxManager>().RandomMessage("Selecciona las semillas para plantar");
                                    }
                                }
                                break;
                        }
                    }
                }
                else if (hit.collider.tag.Equals("Shops") && !inventoryPanel.activeSelf)
                {
                    inventoryPanel.transform.parent.GetComponent<InventoryCanvasManager>().activeInventoryMenu();
                    inventoryPanel.GetComponent<RectTransform>().anchorMax = new Vector2(0, 0.5f);
                    inventoryPanel.GetComponent<RectTransform>().anchorMin = new Vector2(0, 0.5f);
                    inventoryPanel.GetComponent<RectTransform>().pivot = new Vector2(0, 0.5f);
                    inventoryPanel.GetComponent<RectTransform>().anchoredPosition = new Vector2(126, 0);

                    if (hit.collider.name.Contains("Fruit"))
                    {
                        shopInventoryPanel.GetComponent<InventoryShopManager>().playerInventory = fruitsInventory;
                    }
                    else if (hit.collider.name.Contains("Seed"))
                    {
                        shopInventoryPanel.GetComponent<InventoryShopManager>().playerInventory = seedsInventory;
                    }
                    else if (hit.collider.name.Contains("Potion"))
                    {
                        shopInventoryPanel.GetComponent<InventoryShopManager>().playerInventory = potionsInventory;
                    }
                    shopInventoryPanel.transform.parent.GetComponent<InventoryCanvasManager>().activeShopInventoryMenu();
                }
            }
        }
    }

    private string typeOfSeed()
    {
        string result = "";

        InventoryItem itemSeed = GameObject.FindGameObjectWithTag("Canvas").GetComponent<Tools>().thirdItem;
        if (itemSeed.itemName.Contains("lechuga"))
        {
            result = "Cabbage";
        }
        else if (itemSeed.itemName.Contains("zanahoria"))
        {
            result = "Carrot";
        }
        else if (itemSeed.itemName.Contains("pepino"))
        {
            result = "Cucumber";
        }
        else if (itemSeed.itemName.Contains("berenjena"))
        {
            result = "Eggplant";
        }
        else if (itemSeed.itemName.Contains("cebolla"))
        {
            result = "Onion";
        }
        else if (itemSeed.itemName.Contains("piña"))
        {
            result = "Pineapple";
        }
        else if (itemSeed.itemName.Contains("patata"))
        {
            result = "Potato";
        }
        else if (itemSeed.itemName.Contains("calabaza"))
        {
            result = "Pumpkin";
        }
        else if (itemSeed.itemName.Contains("fresa"))
        {
            result = "Strawberry";
        }
        else if (itemSeed.itemName.Contains("tomate"))
        {
            result = "Tomato";
        }
        else if (itemSeed.itemName.Contains("nabo"))
        {
            result = "Turnip";
        }

        return result;
    }
}
