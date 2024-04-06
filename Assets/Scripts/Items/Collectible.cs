using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Item))]
public class Collectible : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerInventory player = other.GetComponent<PlayerInventory>();

        if(player)
        {
            Item item = GetComponent<Item>();
            if(item != null)
            {
                player.inventory.Add("Backpack", item);
                Destroy(this.gameObject);
            }
        }
    }
}
