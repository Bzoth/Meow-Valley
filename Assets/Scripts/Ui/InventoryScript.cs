using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class InventoryScript : MonoBehaviour
{
    public string inventoryName;
    public List<SlotsUi> slots = new List<SlotsUi>();

    [SerializeField] private Canvas canvas;
    private Inventory inventory;

    void Awake()
    {
        canvas = FindObjectOfType<Canvas>();
    }

    void Start()
    {
        inventory = GameManager.instance.player.inventory.GetInventoryByName(inventoryName);

        SetupSlots();
        Refresh();
    }

    public void Refresh()
    {
        if(slots.Count == inventory.slots.Count)
        {
            for(int i = 0; i < slots.Count; i++)
            {
                if(inventory.slots[i].itemName != "")
                {
                    slots[i].SetItem(inventory.slots[i]);
                }
                else
                {
                    slots[i].SetEmpty();
                }
            }
        }
    }

    public void Remove()
    {
        Item itemToDrop = GameManager.instance.itemManager.GetItemByName(inventory.slots[UiManager.draggedSlot.slotID].itemName);

        if(itemToDrop != null)
        {
            if(UiManager.dragSingle )
            {
                GameManager.instance.player.DropItem(itemToDrop);
                inventory.Remove(UiManager.draggedSlot.slotID);
            }
            else
            {
                GameManager.instance.player.DropItem(itemToDrop, inventory.slots[UiManager.draggedSlot.slotID].count);
                inventory.Remove(UiManager.draggedSlot.slotID, inventory.slots[UiManager.draggedSlot.slotID].count);
            }
            
            Refresh();
        }

        UiManager.draggedSlot = null;
        
    }

    public void SlotBeginDrag(SlotsUi slot)
    {
        UiManager.draggedSlot = slot;
        UiManager.draggedIcon = Instantiate(slot.itemIcon);
        UiManager.draggedIcon.transform.SetParent(canvas.transform);
        UiManager.draggedIcon.raycastTarget = false;
        UiManager.draggedIcon.rectTransform.sizeDelta = new Vector2(100, 100);

        MoveToMousePosition(UiManager.draggedIcon.gameObject);
    }

    public void SlotDrag()
    {
        MoveToMousePosition(UiManager.draggedIcon.gameObject);
    }

    public void SlotEndDrag()
    {
        print(UiManager.draggedIcon);
        Destroy(UiManager.draggedIcon.gameObject);
        UiManager.draggedIcon = null;
    }

    public void SlotDrop(SlotsUi slot)
    {
        UiManager.draggedSlot.inventory.MoveSlot(UiManager.draggedSlot.slotID, slot.slotID, slot.inventory);
        
        GameManager.instance.uiManager.RefreshAll();
    }

    private void MoveToMousePosition(GameObject toMove)
    {
        if(canvas != null)
        {
            Vector2 position;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, Input.mousePosition, null, out position);

            toMove.transform.position = canvas.transform.TransformPoint(position) ;
        }
    }

    void SetupSlots()
    {
        int counter = 0;

        foreach(SlotsUi slot in slots)
        {
            slot.slotID = counter;
            counter++;
            slot.inventory = inventory;
        }
    }
}
