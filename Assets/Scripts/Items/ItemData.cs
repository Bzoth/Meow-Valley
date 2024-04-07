using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item Data", menuName = "Item Data", order = 50)]
public class ItemData : ScriptableObject
{
    public string itemName = "Item Name";
    public Sprite icon;
    public GameObject plant;

    public Type type;

    public bool isPlantable;

    public int capacity;
}

public enum Type
{
    plant,
    tool
}
