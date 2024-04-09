using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public InventoryManager inventoryManager;
    public GameObject inventoryTab;
    public InventoryScript inventoryScript;
    public UiManager uiManager;
    private Inventory inventory;
    public string inventoryName;
    private SlotsUi slotsUi;
    public ToolBarUi toolBarUi;
    public ItemData itemData;

    public GameObject dropLocation;
    public void Awake()
    {
        inventoryManager = GetComponent<InventoryManager>();
        inventory = GameManager.instance.player.inventoryManager.GetInventoryByName(inventoryName);
        toolBarUi = GetComponent<ToolBarUi>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(inventoryManager.toolbar.selectedSlot.type == Type.tool)
            {
                print(inventoryManager.toolbar.selectedSlot.itemName + " Used");

                uiManager.RefreshInventoryUI("Toolbar");
            }

        if(inventoryManager.toolbar.selectedSlot.type == Type.plant && inventoryManager.toolbar.selectedSlot.count >= 1)
        {
            inventoryManager.toolbar.selectedSlot.count --;
            Instantiate(inventoryManager.toolbar.selectedSlot.plant, dropLocation.transform.position, Quaternion.identity);

            if(inventoryManager.toolbar.selectedSlot.count == 0)
            {
                inventoryManager.toolbar.selectedSlot.icon = null;
                inventoryManager.toolbar.selectedSlot.itemName = "";
            }
            print("Seed Used");
            uiManager.RefreshInventoryUI("Toolbar");
        }
        }
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
