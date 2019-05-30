using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Create new inventory", menuName ="Player inventory")]
public class PlayerInventory : ScriptableObject {

    public List<InventoryItem> inventory = new List<InventoryItem>();

}
