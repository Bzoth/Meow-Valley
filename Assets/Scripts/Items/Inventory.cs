using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Inventory 
{
    [System.Serializable]
    public class Slot
    {
        public string itemName;
        public int count;
        public int maxAllowed;

        public int slotMax = 0;
        public Type type;
        public GameObject plant;
        public Sprite icon;
        public Slot()
        {
            itemName = "";
            count = 0;
            maxAllowed = 20;
        }
        

        public bool IsEmpty
        {
            get
            {
                if(itemName == "" && count == 0)
                {
                    return true;
                }

                return false;
            }
        }


        public bool CanAddItem(string itemName)
        {
            if(this.itemName == itemName && count < maxAllowed && slotMax < 24)
            {
                return true;
            }
            return false;  
        }

        public void AddItem(Item item)
        {
            this.itemName = item.data.itemName;
            this.icon = item.data.icon;
            this.maxAllowed = item.data.capacity;
            this.type = item.data.type;
            this.plant = item.data.plant;
            count++;
        }

        public void AddItem(string itemName, Sprite icon, int maxAllowed, Type type, GameObject plant)
        {
            this.itemName = itemName;
            this.icon = icon;
            this.type = type;
            this.plant = plant;
            count++;
            this.maxAllowed = maxAllowed;
        }

        public void RemoveItem()
        {
            if(count > 0 )
            {
                count--;

                if(count == 0)
                {
                    icon = null;
                    itemName = "";
                }
            }
        }
    }

    public List<Slot> slots = new List<Slot>();
    public Slot selectedSlot = null;
    public Inventory(int numSlots)
    {
        for(int i = 0; i < numSlots; i++)
        {
            Slot slot = new Slot();
            slots.Add(slot);
        }
    }

    public void MaxCapacity()
    {
        for(int z = 0; z < slots.Count; z++)
        {
            if(slots[z].itemName != "")
            {
                // use slotMax
            }
        }
    }

    public void Add(Item item)
    {
        foreach(Slot slot in slots)
        {
            if(slot.itemName == item.data.itemName && slot.CanAddItem(item.data.itemName))
            {
                slot.AddItem(item);
                return;
            }
        }

        foreach(Slot slot in slots)
        {
            if(slot.itemName == "")
            {
                slot.AddItem(item);
                return;
            }
        }
    }

    public void Remove(int index)
    {
        slots[index].RemoveItem();
    }
    
    public void Remove(int index, int numToRemove)
    {
        if(slots[index].count >= numToRemove)
        {
            for(int i = 0;i < numToRemove; i++)
            {
                Remove(index);
            }
        }
    }

    public void MoveSlot(int fromIndex, int toIndex, Inventory toInventory, int numToMove = 1)
    {
        Slot fromSlot = slots[fromIndex];
        Slot toSlot = toInventory.slots[toIndex];

        if(toSlot.IsEmpty || toSlot.CanAddItem(fromSlot.itemName))
        {
            for(int i = 0; i < numToMove; i++)
            {
                toSlot.AddItem(fromSlot.itemName, fromSlot.icon, fromSlot.maxAllowed, fromSlot.type, fromSlot.plant);
                fromSlot.RemoveItem();
            } 
        }
    }

    public void SelecSlot(int index)
    {
        if(slots != null && slots.Count > 0)
        {
            selectedSlot = slots[index];
        }
    }
}
