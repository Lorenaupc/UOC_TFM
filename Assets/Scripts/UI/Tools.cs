using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tools : MonoBehaviour {

    internal string currentTool;

    void Start()
    {
        currentTool = "";
    }

    void Update()
    {

    }

    public void setHoe()
    {
        currentTool = "Hoe";
    }

    public void setHammer()
    {
        currentTool = "Hammer";
    }

    public void setScyther()
    {
        currentTool = "Scyther";
    }

    public void setWater()
    {
        currentTool = "Water";
        Debug.Log("WATER");
    }

    public void setAxe()
    {
        currentTool = "Axe";
    }

    public void setSeed()
    {
        currentTool = "Seed";
    }

    public void setFish()
    {
        currentTool = "Fish";
    }
}
