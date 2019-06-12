using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shops : MonoBehaviour {

    public GameObject fruitsShop;
    public GameObject seedsShop;
    public GameObject potionsShop;

    void Start () {
        fruitsShop.SetActive(false);
        seedsShop.SetActive(false);
        potionsShop.SetActive(false);
    }

}
