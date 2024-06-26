using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolBarUi : MonoBehaviour
{
    public List <SlotsUi> toolbarSlots = new List<SlotsUi>();
    private SlotsUi selectedSlot;
    public Inventory inventory;

    void Start()
    {
        SelectSlot(0);
    }

    void Update()
    {
        CheckAlphaNumericKeys();
    }

    public void SelectSlot(SlotsUi slot)
    {
        SelectSlot(slot.slotID);
    }

    public void SelectSlot(int index)
    {
        if(toolbarSlots.Count == 8)
        {
            if(selectedSlot != null)
            {
                selectedSlot.SetHighlight(false);
            }
            selectedSlot = toolbarSlots[index];
            selectedSlot.SetHighlight(true);

            GameManager.instance.player.inventoryManager.toolbar.SelecSlot(index);
        }
    }

    private void CheckAlphaNumericKeys()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            SelectSlot(0);
        }
        
        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            SelectSlot(1);
        }

        if(Input.GetKeyDown(KeyCode.Alpha3))
        {
            SelectSlot(2);
        }

        if(Input.GetKeyDown(KeyCode.Alpha4))
        {
            SelectSlot(3);
        }

        if(Input.GetKeyDown(KeyCode.Alpha5))
        {
            SelectSlot(4);
        }

        if(Input.GetKeyDown(KeyCode.Alpha6))
        {
            SelectSlot(5);
        }

        if(Input.GetKeyDown(KeyCode.Alpha7))
        {
            SelectSlot(6);
        }

        if(Input.GetKeyDown(KeyCode.Alpha8))
        {
            SelectSlot(7);
        }
    }
}

