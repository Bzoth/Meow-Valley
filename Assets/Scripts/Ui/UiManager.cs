using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    public Dictionary<string, InventoryScript> inventoryScriptByName = new Dictionary<string, InventoryScript>();
    public List<InventoryScript> InventoryScripts;

    public static SlotsUi draggedSlot;
    public static Image draggedIcon;
    public static bool dragSingle;

    public GameObject inventoryTab;

    void Awake()
    {
        Initialize();
    }

    void Update()
    {

        if(Input.GetKeyDown(KeyCode.E))
        {
            ToggleInventoryUI();
            RefreshInventoryUI("Backpack");
        }
        if(Input.GetKey(KeyCode.LeftShift))
        {
            dragSingle = true;
        }
        else
        {
            dragSingle = false;
        }
    }

    
    public void ToggleInventoryUI()
    {
        if(inventoryTab != null)
        {
            if (!inventoryTab.activeSelf)
            {
                inventoryTab.SetActive(true);
            }
            else
            {
                inventoryTab.SetActive(false);
            }
        }
    }

    public void RefreshInventoryUI(string inventoryName)
    {
        if(inventoryScriptByName.ContainsKey(inventoryName))
        {
            inventoryScriptByName[inventoryName].Refresh();
        }
    }

    public void RefreshAll()
    {
        foreach(KeyValuePair<string, InventoryScript> keyValuePair in inventoryScriptByName)
        {
            keyValuePair.Value.Refresh();
        }
    }

    public InventoryScript GetInventoryScript(string inventoryName)
    {
        if(inventoryScriptByName.ContainsKey(inventoryName))
        {
            return inventoryScriptByName[inventoryName];
        }

        Debug.LogWarning("There is not inventory ui for " + inventoryName);
        return null;
    }

    void Initialize()
    {
        foreach(InventoryScript script in InventoryScripts)
        {
            if(!inventoryScriptByName.ContainsKey(script.inventoryName))
            {
                inventoryScriptByName.Add(script.inventoryName, script);
            }
        }
    }
    



    public void InventoryCloser()
    {
        inventoryTab.SetActive(false);
    }

    
}
