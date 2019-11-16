using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemWheel : Interactable
{

    //public ItemInventory myInventory;

    public override void OpenMenu()
    {
        // tell canvas to spawn a menu.
        ImportItemData(myInventory);
        RadialMenuSpawner.ins.SpawnMenu(this);
    }
    
    
    public void ImportItemData(ItemInventory inventory)
    {
        options.Clear();
        for (int i = 0; i < inventory.inventoryList.Count; i++)
        {
            if (i >= options.Count)
            {
                options.Add(new Element());
            }
            //Debug.Log(WeaponInventory[i].title + "" + i.ToString());
            options[i].title = inventory.inventoryList[i].GetComponent<ItemData>().title;
            options[i].color = inventory.inventoryList[i].GetComponent<ItemData>().color;
            options[i].icon = inventory.inventoryList[i].GetComponent<ItemData>().icon;
        }

    }
    public override void SelectItem(int index)
    {
        //use item 
        myInventory.inventoryList[index].GetComponent<ItemData>().UseItem(this, index);
        //myInventory.inventoryList[index].GetComponent<ItemData>().ItemModelVisible(this, index);
        myInventory.inventoryList[index].GetComponent<ItemData>().ItemInHolster(this, index, myInventory.hardpointsManager.Hardpoints, myInventory.hand);
        // equip Item Script here movign to use item function attached to weapon
        {
            /*
            // called by the weapon wheel when an item is selected.
            if (index < myInventory.inventoryList.Count)
            {
                if (myInventory.equippedItemsList.Capacity <= 0)
                {
                    myInventory.equippedItemsList.Add(myInventory.inventoryList[index]);
                    //myInventory.ItemModelList[index].SetActive(true);
                }
                else
                {
                    myInventory.equippedItemsList[0] = myInventory.inventoryList[index];
                   // myInventory.ItemModelList[index].SetActive(true);
                }

            }
            else
            {
                Debug.Log("Attempted to Equip Null inventory slot");
            }
            */
        }
    }
    public override void DropItem(int index)
    {
        myInventory.inventoryList[index].GetComponent<ItemData>().DropItem(this, index); // instantiate a new weapon pickup
        myInventory.inventoryList.Insert(index, myInventory.emptyItem.gameObject); // add an empty slot where the weapon was
        //myInventory.inventoryList[index + 1].GetComponent<ItemData>().ItemModelVisible(this, index);
        myInventory.inventoryList[index + 1].GetComponent<ItemData>().ItemModelDestroy(this, index); // destroy the model of the weapon
        Destroy(myInventory.inventoryList[index + 1]); // destroy the weapon object before removing it 
        myInventory.inventoryList.Remove(myInventory.inventoryList[index+1]);  // remove the weapon from your inventory 
        // set the item in your hand invisible 
        
    }
    /*
    public void ItemVisible(int index)
    {
        if (myInventory.inventoryListSlotRestriction.Length > 0)
        {
            for (int i = 0; i < myInventory.inventoryListSlotRestriction.Length; i++)
            {
                myInventory.ItemModelList[i].SetActive(false);
                //Debug.Log("Ping"+i);
            }
        }
        else
        {
            for (int i = 0; i < myInventory.ItemModelList.Count; i++)
            {
                myInventory.ItemModelList[i].SetActive(false);
            }
        }
        myInventory.ItemModelList[index].SetActive(true);
        Debug.Log(myInventory.equippedItemsList[0].name);
    }
    */
}
