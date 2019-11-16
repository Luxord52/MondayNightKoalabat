using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : ItemPickup
{
    //public WeaponData InventoryWeapon;


    // on collision pick up this thing

    private void OnTriggerEnter(Collider other)
    {
        ItemInventory player = other.gameObject.GetComponent<ItemInventory>();
        //StateManager playerA = collision.gameObject.GetComponent<StateManager>();
        Debug.Log("1");
        if (player != null && inventoryItem.GetComponent<WeaponData>() != null)
        {
            
            player.AddItemToInventory(inventoryItem.GetComponent<WeaponData>());
            //instantiate the weapon model in the players hand set inactive to be invisible.
            player.AddItemModel(this,inventoryItem.GetComponent<WeaponData>(),player.hardpointsManager.Hardpoints, player.hand);
            Debug.Log("2");
            Destroy(gameObject);
        }
    }

}
