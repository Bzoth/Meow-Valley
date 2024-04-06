using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public InventoryManager inventory;
    public GameObject inventoryTab;
    public InventoryScript inventoryScript;
    public UiManager uiManager;

    public GameObject dropLocation;
    public void Awake()
    {
        inventory = GetComponent<InventoryManager>();
    }

    public void DropItem(Item item)
    {
        Vector2 spawnLocation = dropLocation.transform.position;


        Vector2 spawnOffset = Random.insideUnitCircle * 1.25f;

        Item droppedItem = Instantiate(item, spawnLocation, Quaternion.identity);

        droppedItem.rb2d.AddForce(spawnOffset * 0.2f, ForceMode2D.Impulse);
    }


    public void DropItem(Item item, int numToDrop)
    {
        for(int i = 0; i < numToDrop; i++)
        {
            DropItem(item);
        }
    }
}
