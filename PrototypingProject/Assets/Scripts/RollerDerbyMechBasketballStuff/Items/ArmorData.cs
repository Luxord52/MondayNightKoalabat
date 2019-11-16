using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorData : ItemData
{
    //needs an armor value
    public int ArmorRating;
    //needs a speed reduction penalty
    [Range(0,100)]
    public float SpeedPenaltyPercent;
    //eject armor from inventory
    
    private ArmorData PrivateClone()
    {
        return (ArmorData)MemberwiseClone();
    }

    public override void UseItem()
    {
        // jettison armor piece 
    }

    public override void ItemModelDestroy(Interactable myInteractable, int index)
    {
        Destroy(myInteractable.myInventory.ItemModelList[index]);
    }

    // set this item model to be in the players hand and all other item models to their hardpoints
    public override void ItemInHolster(Interactable myInteractable, int index, GameObject[] holster, GameObject hand)
    {
        base.ItemInHolster(myInteractable, index, holster, hand);
    }

}
