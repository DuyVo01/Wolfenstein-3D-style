using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public List<GameObject> inventoryItems;
    public int carryWeight;

    private void Start()
    {
        inventoryItems = new List<GameObject>();
    }

    public void AddItem(GameObject newItems)
    {
        
        if (inventoryItems.Count < carryWeight)
        {
            inventoryItems.Add(newItems);
            if (newItems.CompareTag("Gun"))
            {
                PlayerStatus.currentWeaponEquipped = newItems;
            }
        }
    }
}
