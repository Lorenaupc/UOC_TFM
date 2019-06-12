using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour {

    internal Sprite normalGrassObject;

    void Start () {
        StartCoroutine(GrowGrass());
    }

    IEnumerator GrowGrass()
    {
        yield return new WaitForSeconds(600);
        Debug.Log("It's Been 10 min");
        
        GetComponent<SpriteRenderer>().sprite = normalGrassObject;
        GetComponent<SpriteRenderer>().sortingOrder = 0;
        transform.tag = "NormalGrass";
        Destroy(GetComponent<Ground>());
    }
}
