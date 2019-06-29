using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckKeys : MonoBehaviour {

    public PlayerInventory playerInventory;
    public InventoryItem key;

    private GameObject canvas;

    private void Start()
    {
        canvas = GameObject.FindGameObjectWithTag("Canvas");
    }

    void Update()
    {
        if (playerInventory.inventory.Contains(key))
        {
            GetComponent<DisableTransitions>().Enable();
            Destroy(this.gameObject);
        }
        else
        {
            GetComponent<DisableTransitions>().Disable();
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
        {
            canvas.GetComponent<DialogBoxManager>().RandomMessage("No tienes la llave para entrar");
        }
    }

}
