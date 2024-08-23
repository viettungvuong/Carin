using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<InventoryItem> items = new List<InventoryItem>();

    public void AddItem(string itemName, int quantity)
    {
        InventoryItem item = items.Find(i => i.itemName == itemName);

        if (item != null)
        {
            item.quantity += quantity;
        }
        else
        {
            items.Add(new InventoryItem(itemName, quantity));
        }
    }

    public void RemoveItem(string itemName, int quantity)
    {
        InventoryItem item = items.Find(i => i.itemName == itemName);

        if (item != null)
        {
            item.quantity -= quantity;
            if (item.quantity <= 0)
            {
                items.Remove(item);
            }
        }
    }

    public bool HasItem(string itemName, int quantity)
    {
        InventoryItem item = items.Find(i => i.itemName == itemName);
        return item != null && item.quantity >= quantity;
    }
}