using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup: MonoBehaviour
{
    public GameObject inventoryItem;
    // basic spin and bounce stuff if the game calls for it.
    public GameObject Model;
    public float spinSpeed;
    public float bounceSpeed;
    public float bounceHeight;
    int count;

    public void Update()
    {
        SpinAndBounce(Model);
    }
    /*
    public GameObject CloneModel()
    {
        return Model; ;
    }
    */
    public void SpinAndBounce(GameObject obj)
    {
        obj.transform.Rotate(transform.up,spinSpeed,Space.Self);

        //get the objects current position and put it in a variable so we can access it later with less code
        Vector3 pos = Model.transform.localPosition;
        //calculate what the new Y position will be
        float newY = Mathf.Sin(Time.time * bounceSpeed);
        //set the object's Y to the new calculated Y
        Model.transform.localPosition = new Vector3(pos.x, newY, pos.z) * bounceHeight;

    }

    private void OnTriggerEnter(Collider other)
    {
        ItemInventory player = other.gameObject.GetComponent<ItemInventory>();
        if (player != null && inventoryItem.GetComponent<ItemData>() != null)
        {
            player.AddItemToInventory(inventoryItem.GetComponent<ItemData>());
            Destroy(gameObject);
        }

    }
}
