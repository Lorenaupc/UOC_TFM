using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Shops : MonoBehaviour {

    internal GameObject oleadatext;
    internal GameObject fruitsShop;
    internal GameObject seedsShop;
    internal GameObject potionsShop;

    internal bool activateFruitShop = false;
    internal bool activateSeedShop = false;
    internal bool activatePotionShop = false;

    internal void Start () {

        GameObject[] shops = GameObject.FindGameObjectsWithTag("Shops");
        foreach (GameObject shop in shops)
        {
            if (shop.name.Contains("Fruit"))
            {
                fruitsShop = shop;
            }
            else if (shop.name.Contains("Seed"))
            {
                seedsShop = shop;
            }
            else if (shop.name.Contains("Potion"))
            {
                potionsShop = shop;
            }
        }
        if (!activateFruitShop)
        {
            fruitsShop.SetActive(false);
        }
        if (!activatePotionShop)
        {
            potionsShop.SetActive(false);
        }
        if (!activateSeedShop)
        {
            seedsShop.SetActive(false);
        }

        oleadatext = GameObject.FindGameObjectWithTag("OleadaText");
        oleadatext.GetComponent<Text>().enabled = false;
    }

    internal void Reload()
    {
        oleadatext.SetActive(false);
    }
}
