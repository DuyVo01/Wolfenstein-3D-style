using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [field: SerializeField] public List<GameObject> InventoryItems { get; private set; }
    [SerializeField] private int carryWeight;
    
    private void Awake()
    {
        InventoryItems = new List<GameObject>();
    }

    public void AddItem(GameObject newItems)
    {
        if (InventoryItems.Count < carryWeight)
        {
            if (GetItemCount(newItems.tag) < 2)
            {
                InventoryItems.Add(newItems);
            }
            if (newItems.CompareTag("Gun"))
            {
                PlayerStatus.currentWeaponEquipped = newItems;
            }
        }
    }

    public int GetItemCount(string tag)
    {
        int count = 0;
        foreach(GameObject item in InventoryItems)
        {
            if (item.CompareTag(tag))
            {
                count++;
            }
        }
        return count;
    }

    public void RemoveItem(string tag)
    {
        foreach (GameObject item in InventoryItems)
        {
            if (item.CompareTag(tag))
            {
                InventoryItems.Remove(item);
                break;
            }
        }
    }
}
