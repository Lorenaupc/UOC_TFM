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

    public UnityEvent this_event;

    public void Use()
    {
        Debug.Log("Using Item: " + itemName);
        this_event.Invoke();
    }

}
