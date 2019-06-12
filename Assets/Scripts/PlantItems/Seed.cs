using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seed : MonoBehaviour {

    internal Sprite normalGrassObject;
    internal Sprite wateredSeed;
    internal Sprite growSeed;

    internal string type;

    internal bool watered;
    private bool growed;

    private bool enableWater;

    private int time;

    void Start()
    {
        watered = false;
        growed = false;
        enableWater = true;
        
        typeOfSeed();
    }

    private void typeOfSeed()
    {
        if (type.Equals("Cabbage")){
            time = 300;
        }
        else if (type.Equals("Carrot"))
        {
            time = 300;
        }
        else if (type.Equals("Cucumber"))
        {
            time = 420;
        }
        else if (type.Equals("Eggplant"))
        {
            time = 420;
        }
        else if (type.Equals("Onion"))
        {
            time = 180;
        }
        else if (type.Equals("Pineapple"))
        {
            time = 600;
        }
        else if (type.Equals("Potato"))
        {
            time = 420;
        }
        else if (type.Equals("Pumpkin"))
        {
            time = 600;
        }
        else if (type.Equals("Strawberry"))
        {
            time = 180;
        }
        else if (type.Equals("Tomato"))
        {
            time = 420;
        }
        else if (type.Equals("Turnip"))
        {
            time = 300;
        }
    }

    private void addToInventorySeed()
    {        
        InventoryItem itemToAdd = Resources.Load<InventoryItem>(type);
        GameObject.FindGameObjectWithTag("Canvas").GetComponent<Tools>().inventoryManager.AddItem(itemToAdd);
    }

    private void takeSeeds()
    {
        int x = Random.Range(0, 5);
        if (x == 0)
        {
            Debug.Log("Semilla conseguida!");
            string seed = type + "Seed";
            InventoryItem itemToAdd = Resources.Load<InventoryItem>(seed);
            GameObject.FindGameObjectWithTag("Canvas").GetComponent<Tools>().inventoryManager.AddItem(itemToAdd);
        }
    }

    private void Update()
    {
        if (enableWater)
        {
            if (watered && !growed)
            {
                GetComponent<SpriteRenderer>().sprite = wateredSeed;
                StartCoroutine(GrowPlant());
                enableWater = false;
            }
        }
    }

    IEnumerator GrowPlant()
    {
        yield return new WaitForSeconds(time);

        GetComponent<SpriteRenderer>().sprite = growSeed;
        growed = true;
    }

    private void OnMouseDown()
    {
        if (growed)
        {
            GetComponent<SpriteRenderer>().sprite = normalGrassObject;
            GetComponent<SpriteRenderer>().sortingOrder = 0;
            GetComponent<BoxCollider2D>().isTrigger = true;
            transform.tag = "NormalGrass";

            addToInventorySeed();
            takeSeeds();

            Destroy(GetComponent<Seed>());
        }
    }
}
