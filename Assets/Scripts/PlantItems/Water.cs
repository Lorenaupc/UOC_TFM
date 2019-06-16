using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour {

    internal GameObject canvas;
    internal int waterCounter;

    private void Start()
    {
        canvas = GameObject.FindGameObjectWithTag("Canvas");
        waterCounter = 0;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            canvas.GetComponent<Tools>().currentTool = "";
        }
    }

    internal void fillWaterCan()
    {
        waterCounter = 20;
    }

    internal bool decreaseWaterCan()
    {
        if (waterCounter != 0)
        {
            waterCounter--;
            return true;
        }
        else
        {
            canvas.GetComponent<DialogBoxManager>().RandomMessage("Rellena la regadera en una balsa de agua");
            return false;
        }
    }
}
