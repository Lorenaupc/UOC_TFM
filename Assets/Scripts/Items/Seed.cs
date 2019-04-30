using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seed : MonoBehaviour {

    internal Sprite normalGrassObject;
    internal Sprite wateredSeed;
    internal Sprite growSeed;

    internal bool watered;
    private bool growed;

    private bool enableWater;

    private int time;

    void Start()
    {
        time = 10;
        watered = false;
        growed = false;
        enableWater = true;
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
            Destroy(GetComponent<Seed>());
        }
    }
}
