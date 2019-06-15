using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckKeys : MonoBehaviour {

    public PlayerInventory playerInventory;
    public InventoryItem key;

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
}
