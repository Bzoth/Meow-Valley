using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CropScript : MonoBehaviour
{
    public CropData cropData;
    public SpriteRenderer spriteRenderer;
    int growthStage = 0;
    int growthTime;
    Sprite tier1, tier2;
    public bool harvestable = false;
    public GameObject harvest;
    

    void Start()
    {
        StartCoroutine(CropGrowth());
    }

    public IEnumerator CropGrowth()
    {
        growthTime = cropData.growTime;
        tier1 = cropData.tier1;
        tier2 = cropData.tier2;
        harvest = cropData.harvest;

        if(growthStage < 2)
        {
            yield return new WaitForSeconds(growthTime);
            growthStage++;

            if(growthStage == 1)
            {
                spriteRenderer.sprite = tier1;
                StartCoroutine(CropGrowth());
            }
            else if(growthStage == 2)
            {
                spriteRenderer.sprite = tier2;
                harvestable = true;
            }
        }
    }

    public void Harvest()
    {
        if(harvestable == true)
        {
            Instantiate(harvest, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Point"))
        {
            Harvest();
        }
    }
}
