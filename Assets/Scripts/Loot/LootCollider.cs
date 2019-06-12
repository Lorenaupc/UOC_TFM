using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootCollider : MonoBehaviour {

    public InventoryItem item;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
        {
            GameObject.FindGameObjectWithTag("Canvas").GetComponent<Tools>().inventoryManager.AddItem(item);
            Destroy(this.gameObject);
        }
    }
}
