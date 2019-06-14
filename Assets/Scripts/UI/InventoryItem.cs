using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName="New Item", menuName ="Inventory/Items")]
public class InventoryItem : ScriptableObject {

    public string itemName;
    public string itemDescription;
    public Sprite itemImage;
    public int count;
    public bool unique;
    public bool usable;

    public bool isOnUI = false;
    public int positionOnUI;

    public int sell_cost;
    public int buy_cost;

    public UnityEvent this_event;

    public void Use()
    {
        Debug.Log("Using Item: " + itemName);
        this_event.Invoke();
    }

}
