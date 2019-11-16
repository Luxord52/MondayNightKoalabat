using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingHook_Pickup : ItemPickup
{
    private void OnTriggerEnter(Collider other)
    {
        ItemInventory player = other.gameObject.GetComponent<ItemInventory>();
        //StateManager playerA = collision.gameObject.GetComponent<StateManager>();
        if (player != null && inventoryItem.GetComponent<GrapplingHook>() != null)
        {
            player.AddItemToInventory(inventoryItem.GetComponent<GrapplingHook>());
            //instantiate the weapon model in the players hand set inactive to be invisible.
            player.AddItemModel(this, inventoryItem.GetComponent<GrapplingHook>(), player.hardpointsManager.Hardpoints, player.hand);
            Destroy(gameObject);
        }
    }
}
