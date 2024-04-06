using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public Dictionary<string, Inventory> inventoryByName = new Dictionary<string, Inventory>();

    [Header("Backpack")]
    public Inventory backpack;
    public int backpackSlotsCount;

    [Header("Toolbar")]
    public Inventory toolbar;
    public int toolbarSlotsCount;

    void Awake()
    {
        backpack = new Inventory(backpackSlotsCount);
        toolbar = new Inventory(toolbarSlotsCount);

        inventoryByName.Add("Backpack", backpack);
        inventoryByName.Add("Toolbar", toolbar);
    }

    public void Add(string invenrotyName, Item item)
    {
        if (inventoryByName.ContainsKey(invenrotyName))
        {
            inventoryByName[invenrotyName].Add(item);
        }
    }
    

    public Inventory GetInventoryByName(string inventoryName)
    {
        if(inventoryByName.ContainsKey(inventoryName))
        {
            return inventoryByName[inventoryName];
        }

        return null;
    }
}
