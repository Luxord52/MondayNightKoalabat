using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorPickup : ItemPickup
{
    // might not need this since it is going into the equipment wheel? 
    // needed to add armor to hard point visually
    private void OnTriggerEnter(Collider other)
    {
        ItemInventory player = other.gameObject.GetComponent<ItemInventory>();
        //StateManager playerA = collision.gameObject.GetComponent<StateManager>();
        if (player != null && inventoryItem.GetComponent<ArmorData>() != null)
        {

            player.AddItemToInventory(inventoryItem.GetComponent<ArmorData>());
            //instantiate the weapon model in the players hand set inactive to be invisible.
            player.AddItemModel(this, inventoryItem.GetComponent<ArmorData>(), player.hardpointsManager.Hardpoints, player.hand);
            Destroy(gameObject);
        }
    }

}
