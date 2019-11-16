using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponData : ItemData
{
    //public GameObject weaponPickupPrefab;
    // Menu Variables Inherited 
    //public Color color;
    //public Sprite icon;
    //public string title;

    //variables that every weapon has
    public float rateOfFire;//Rate of Fire
    public float reloadTime; //how long it takes to reload
    float counter;
    public int ammo;//Ammo
    public int ammoMax;
    public enum AmmoType //Ammo Type
    {
        None,
        Pistol,
        Rifle,
        Shotgun
    }
    public AmmoType myAmmoType;
    public float range; //Range 
    public int damage; //Damage
    [Range(-1,1)]
    public float recoilX; //rotation axis of recoil (x,y)
    [Range(-1, 1)]
    public float recoilY;
    public float recoilImpulse;// degrees rotates around recoil axis

    //example line renderer for testing purposes 
    public GameObject musselEffects;
    public GameObject BulletTrail;

    private WeaponData PrivateClone()
    {
        return (WeaponData)MemberwiseClone();
    }

    //Functions that every weapon needs to have 
    //Equip weapon using UseItemFunction
    public override void UseItem(Interactable itemWheel, int index)
    {
        
            // called by the weapon wheel when an item is selected.
            if (index < itemWheel.myInventory.inventoryList.Count)
            {
                if (itemWheel.myInventory.equippedItemsList.Capacity <= 0)
                {
                    itemWheel.myInventory.equippedItemsList.Add(itemWheel.myInventory.inventoryList[index]);
                    //itemWheel.myInventory.ItemModelList[index].SetActive(true);
                }
                else
                {
                    itemWheel.myInventory.equippedItemsList[0] = itemWheel.myInventory.inventoryList[index];
                    //itemWheel.myInventory.ItemModelList[index].SetActive(true);
                }

            }
            else
            {
                Debug.Log("Attempted to Equip Null inventory slot");
            }
            
    }

    // set THIS item model to be visible and all ther item modesl invisible 
    public override void ItemModelVisible(Interactable myInteractable, int index)
    {
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
    }

    // set this item model to be in the players hand and all other item models to their hardpoints
    public override void ItemInHolster(Interactable myInteractable, int index, GameObject[] holster, GameObject hand)
    {
        base.ItemInHolster(myInteractable, index, holster, hand);
    }

    public override void ItemModelDestroy(Interactable myInteractable, int index)
    {
        Destroy(myInteractable.myInventory.ItemModelList[index]);
    }

    //Fire
    public virtual void Fire(GameObject myWeapon, Camera myCamera, EquipmentInventory myWeaponManager)
    {
        if (counter <= 0)
        {
            StartCoroutine(FireEnum(myWeapon, myCamera, myWeaponManager));
        }
        else
        {
            Debug.Log("weapon Not Ready");
        }
    }
    public virtual IEnumerator FireEnum(GameObject myWeapon, Camera myCamera, EquipmentInventory myInventory)
    {
        
        if (ammo > 0)
        {
            counter = rateOfFire;
            RaycastHit hit;
            if (Physics.Raycast(myWeapon.transform.position, myWeapon.transform.forward, out hit, range))
            {
                // damage target or create decal or whatever. 
                Debug.Log(hit.transform.name);
            }
            BulletTrail = ObjectPoolingManager.Instance.GetObject("bullet");
            BulletTrail.SetActive(true);
            BulletTrail.GetComponent<MusselEffect>().SetPositions(myWeapon.transform.position, hit.point);
            ammo--;
            ApplyRecoil(myWeapon);
            //ApplyRecoil(myCamera.gameObject);
        }
        else
        {
            Debug.Log("weapon empty");
            Reload(myInventory);
            //Click empty
        }
        yield return null;

        while (counter <= rateOfFire && counter > 0)
        {
            counter -= Time.deltaTime;
            //Debug.Log("weapon not ready: "+counter.ToString());
            yield return null;
        }
    }
    //Reload
    public virtual void Reload(EquipmentInventory myInventory)
    {
        
        if (counter <= 0)
        {
            if (myInventory.AmmoBackpack[(int)myAmmoType] > 0)
            {
                counter = reloadTime;
                Debug.Log("reloading");
                StartCoroutine(ReloadEnum(myInventory));
            }
            else
            {
                Debug.Log(" NO AMMO");
            }
        }
        //Debug.Log("reloading");
        //ammo in gun is lost, new ammo added from backpack
        //ammoBackpack = ammoBackpack - (ammo + ammoMax);
        //ammo = ammoMax;
    }
    public virtual IEnumerator ReloadEnum(EquipmentInventory myWeaponManager)
    {
        // do nothing
        yield return null;

        while (counter <= reloadTime && counter > 0)
        {
            counter -= Time.deltaTime;
            //Debug.Log("reloading" + counter);
            if (Input.GetButtonDown("Fire1"))
            {
                counter = 0;
                StopCoroutine(ReloadEnum(null));
            }
            else if (counter <= 0)
            {
                //throws out all ammo in the gun 
                if (ammoMax > myWeaponManager.AmmoBackpack[(int)myAmmoType])
                {
                    ammo = myWeaponManager.AmmoBackpack[(int)myAmmoType];
                    myWeaponManager.AmmoBackpack[(int)myAmmoType] = 0;
                }
                else
                {
                    myWeaponManager.AmmoBackpack[(int)myAmmoType] -= ammoMax;
                    ammo = ammoMax;
                }
                Debug.Log("RELOADED");
            }
            yield return null;
        }

  
    }

    //Utility Functions to make firing make sense
    // Void doRecoil
    public void ApplyRecoil(GameObject RecoilTarget)
    {
        // needs to interpolate to a new position NOT set to new position
        RecoilTarget.transform.Rotate(new Vector3(recoilX,recoilY,0.0f),recoilImpulse , Space.Self);
    }
    public void CreateBulletLine()
    {
       
    }
    
    //optional additional functions?
    // Secondairy Fire 
    // Check Ammo
}
