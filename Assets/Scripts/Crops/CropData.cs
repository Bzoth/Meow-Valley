using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Crop Data", menuName = "Crop Data", order = 50)]
public class CropData : ScriptableObject
{
    public int growTime;
    public GameObject harvest;

    public Sprite tier0, tier1, tier2, tier3; 
}
