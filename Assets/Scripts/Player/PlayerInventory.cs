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
    public PlayerMovement playerMovement;




    bool canHarvest = true;
    public GameObject point;

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

                uiManager.RefreshInventoryUI("Toolbar");
            }
        }

        

        if(Input.GetKeyDown(KeyCode.F))
        {
            if(canHarvest == true)
            {
                StartCoroutine(HarvestTimer());
            }
        }
        
    }

    IEnumerator HarvestTimer()
    {
        point.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        point.SetActive(false);
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
