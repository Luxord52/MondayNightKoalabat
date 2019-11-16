using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : ItemPickup
{

    public enum AmmoType
    {
        None,
        Pistol,
        Rifle,
        Shotgun
    }
    public AmmoType myAmmoType;
    public int ammoValue;
    private void OnTriggerEnter(Collider other)
    {
        EquipmentInventory player = other.gameObject.GetComponent<EquipmentInventory>();
        //StateManager playerA = collision.gameObject.GetComponent<StateManager>();
        if (player != null)
        {
            player.AddAmmoToBackpack((int)myAmmoType, ammoValue);
            Destroy(gameObject);
        }
    }
}
