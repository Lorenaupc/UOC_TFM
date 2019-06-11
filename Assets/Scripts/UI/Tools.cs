using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tools : MonoBehaviour {

    internal string currentTool;

    void Start()
    {
        currentTool = "Hoe";
    }

    public void setHoe()
    {
        currentTool = "Hoe";
    }

    public void setWater()
    {
        currentTool = "Water";
        Debug.Log("WATER");
    }

    public void setSeed()
    {
        currentTool = "Seed";
    }
}
