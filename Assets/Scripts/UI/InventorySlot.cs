using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour {

    [SerializeField] private Text itemNumber;
    [SerializeField] private Image itemImage;
    
    public InventoryItem item;
    public InventoryManager manager;

    public void Setup(InventoryItem newItem, InventoryManager newManager)
    {
        item = newItem;
        manager = newManager;
        if (item)
        {
            itemImage.sprite = item.itemImage;
            itemNumber.text = "" + item.count;

            //ESTO ES PARA QUE CUANDO SE CREE EL INVENTARIO SE LIMPIE LA INFORMACION DE LAS PARTIDAS ANTERIORES POR SI SE PUSO EN EL INVENTARIO
            item.isOnUI = false;
            item.positionOnUI = 0;
        }
    }

    public void OnClicked()
    {
        if (item)
        {
            manager.SetupDescriptionButton(item.itemDescription, item.usable, item, item.usable, item.usable);
        }
    }

    public void Reload()
    {
        if (item)
        {
            itemNumber.text = "" + item.count;
        }
    }
}
