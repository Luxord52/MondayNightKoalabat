using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInventory : MonoBehaviour
{
    public List<GameObject> inventoryList = new List<GameObject>();
    public ItemData[] inventoryListSlotRestriction; // use this array to set restrictions on what slots are restricted to what type of item. 
    public List<GameObject> equippedItemsList = new List<GameObject>(); // creating a list of equipped items, ony equip items to specific slots. possibly switch out for an array later.
    public GameObject hand;
    public HardpointsManager hardpointsManager;
    public List<GameObject> ItemModelList = new List<GameObject>();

    public ItemData emptyItem;

    public virtual void AddItemModel(ItemPickup obj, ItemData item, GameObject[] hardpoints, GameObject hand)
    {
        GameObject ItemModel = Instantiate(obj.Model);
        ItemModel.transform.SetParent(hand.transform);
        ItemModel.transform.position = hand.transform.position;
        ItemModel.transform.rotation = hand.transform.rotation;
        ItemModel.SetActive(false);
        ItemModelList.Add(ItemModel);
        //instantiate weapon model
        // parent model to player hand
        // set model invisible
        // add the model to some kind of list so it can be set visible later 
        // add the item model to the item data so it can be deleted later 
        item.itemModel = ItemModel;

    }

    public virtual void AddItemToInventory(ItemData item)
    {
        ItemData clone = Instantiate(item.Clone());

        inventoryList.Add(clone.gameObject);
    }
        
}
