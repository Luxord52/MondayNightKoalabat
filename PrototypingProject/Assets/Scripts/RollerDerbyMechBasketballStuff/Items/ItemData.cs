using UnityEngine;

public class ItemData : MonoBehaviour
{
    public GameObject discardItem;
    public GameObject itemModel;
    // Menu Variables REQUIRED 
    public Color color;
    public Sprite icon;
    public string title;


    public virtual ItemData Clone()
    {
        return PrivateClone();
    }

    public virtual void UseItem()
    {
        // use item from inventory 
    }
    public virtual void UseItem(Interactable myInteractable, int index)
    {
        // use item to equip weapon
    }
    public virtual void ItemModelVisible(Interactable myInteractable, int index)
    {
        // normal Items dont have a model associated with them
        {
            /*
            if (myInteractable.myInventory.inventoryListSlotRestriction.Length > 0)
            {
                for (int i = 0; i < myInteractable.myInventory.inventoryListSlotRestriction.Length; i++)
                {
                    myInteractable.myInventory.ItemModelList[i].SetActive(false);
                    //Debug.Log("Ping"+i);
                }
            }
            else
            {
                for (int i = 0; i < myInteractable.myInventory.ItemModelList.Count; i++)
                {
                    myInteractable.myInventory.ItemModelList[i].SetActive(false);
                }
            }
            myInteractable.myInventory.ItemModelList[index].SetActive(true);
            Debug.Log(myInteractable.myInventory.equippedItemsList[0].name);
            */
        }
    }

    public virtual void ItemInHolster(Interactable myInteractable, int index,GameObject[] holster, GameObject hand)
    {
        if (myInteractable.myInventory.inventoryListSlotRestriction.Length > 0)
        {
            for (int i = 0; i < myInteractable.myInventory.inventoryListSlotRestriction.Length; i++)
            {
                // the model is the one selected set its position to hand otherwise set to holster
                if (myInteractable.myInventory.ItemModelList[i].name != "EmptyInventorySlot")
                {
                    myInteractable.myInventory.ItemModelList[i].transform.SetParent(holster[i].transform);
                    SetModelPosRot(myInteractable.myInventory.ItemModelList[i], holster[i]);

                }
            }
        }
        else
        {
            for (int i = 0; i < myInteractable.myInventory.ItemModelList.Count; i++)
            {
                if (myInteractable.myInventory.ItemModelList[i].name != "EmptyInventorySlot")
                {
                    myInteractable.myInventory.ItemModelList[i].transform.SetParent(holster[i].transform);
                    SetModelPosRot(myInteractable.myInventory.ItemModelList[i], holster[i]);
                    Debug.Log("Pong" + i + "/n"
                        + " position: " + hand.transform.position.ToString());
                }
            }
        }
        if (myInteractable.myInventory.ItemModelList[index].name != "EmptyInventorySlot" )
        {
            myInteractable.myInventory.ItemModelList[index].transform.SetParent(hand.transform);
            SetModelPosRot(myInteractable.myInventory.ItemModelList[index], hand);
            Debug.Log(myInteractable.myInventory.equippedItemsList[0].name);
        }
    }


    public void SetModelPosRot(GameObject model, GameObject targetTransform)
    {
        model.transform.position = targetTransform.transform.position;
        model.transform.rotation = targetTransform.transform.rotation;
    }

    public virtual void ItemModelDestroy(Interactable myInteractable, int index)
    {
    }

    public virtual void DropItem(Interactable myInteractable, int index)
    {
        // create a new pickup prefab
        // destory the clone in the player equipment list
        // destroy the model in the player equipment list

        GameObject itemPickup = Instantiate(discardItem, (myInteractable.gameObject.transform.position + myInteractable.GetComponent<StateManager>().myBody.transform.forward *3 ),new Quaternion()) as GameObject; // Temporary fix will not work with mech body as it does not have a state manager script 
    }
    private ItemData PrivateClone()
    {
        return (ItemData)MemberwiseClone();
    }
    // passive Effects If Needed?
}
