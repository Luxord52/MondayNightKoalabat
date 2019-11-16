using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentInventory : ItemInventory
{
    //public ItemData[] inventoryListSlotRestriction; // use this array to set restrictions on what slots are restricted to what type of item. 
    public int[] AmmoBackpack; // store the value of carried ammo in here
    public int[] AmmoBackpackMax; // store the maximum value of carried ammo here.

    //public ItemData emptyItem;

    public void Start()
    {
        for (int i = 0; i <inventoryListSlotRestriction.Length; i++)
        {
            inventoryList.Add(emptyItem.gameObject);
            ItemModelList.Add(emptyItem.gameObject);
        }
        
    }

    public override void AddItemModel(ItemPickup obj, ItemData item,GameObject[] hardpoints, GameObject hand)
    {
        GameObject ItemModel = Instantiate(obj.Model);
        ItemModel.transform.SetParent(hand.transform);
        item.SetModelPosRot(ItemModel, hand);
        
        //ItemModel.SetActive(false);
        if (inventoryListSlotRestriction.Length > 0)
        {
            for (int i = 0; i < inventoryListSlotRestriction.Length; i++)
            {
                if (inventoryListSlotRestriction[i] != null)
                {
                    if (obj.inventoryItem.gameObject == inventoryListSlotRestriction[i].gameObject)
                    {
                        ItemModelList[i] = ItemModel;
                        item.itemModel = ItemModel;
                        // set parent to hardpoint 
                        ItemModel.transform.SetParent(hardpoints[i].transform);
                        // set position & rotation to hardpoint
                        item.SetModelPosRot(ItemModel, hardpoints[i]);
                        
                       

                    }
                }
            }
        }

    }

    public override void AddItemToInventory(ItemData item)
    {
        if (inventoryListSlotRestriction.Length > 0)
        {
            for (int i = 0; i < inventoryListSlotRestriction.Length; i++)
            {
                if (inventoryListSlotRestriction[i] != null)
                {
                    if (item.name == inventoryListSlotRestriction[i].name && inventoryList[i].name == emptyItem.name)
                    {
                        ItemData clone = Instantiate(item.Clone());
                        inventoryList[i] = clone.gameObject;
                        break;
                    }
                }
            }
        }
        else
        {
            ItemData clone = Instantiate(item.Clone());
            inventoryList.Add(clone.gameObject);
        }
        
    }

    public void AddAmmoToBackpack(int ammoType, int ammoVal)
    {

        if (AmmoBackpack[ammoType] + ammoVal > AmmoBackpackMax[ammoType])
        {
            AmmoBackpack[ammoType] = AmmoBackpackMax[ammoType];
        }
        else
        {
            AmmoBackpack[ammoType] += ammoVal;
        }
    }
}
