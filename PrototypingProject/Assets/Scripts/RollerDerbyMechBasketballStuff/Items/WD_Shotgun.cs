using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WD_Shotgun : WeaponData
{
    float notCounter;
    public int pellets;

    //public List<GameObject> BulletTrails = new List<GameObject>();
    //GameObject BulletTrail;
    public float maxDeviation;

    void Start()
    {
       // BulletTrails = new GameObject[pellets];
    }

    public override void Fire(GameObject myWeapon, Camera myCamera, EquipmentInventory myWeaponManager)
    {
        if (notCounter <= 0)
        {
            StartCoroutine(FireEnum(myWeapon, myCamera, myWeaponManager));
        }
        else
        {
            Debug.Log("weapon Not Ready");
        }
    }
    public override IEnumerator FireEnum(GameObject myWeapon, Camera myCamera, EquipmentInventory myWeaponManager)
    {

        if (ammo > 0)
        {
            notCounter = rateOfFire;
            RaycastHit hit;
            for (int i = 0; i < pellets; i++)
            {
                Vector3 forwardVector = Vector3.forward;
                float deviation = Random.Range(0f, maxDeviation);
                float angle = Random.Range(0f, 360f);
                forwardVector = Quaternion.AngleAxis(deviation, Vector3.up) * forwardVector;
                forwardVector = Quaternion.AngleAxis(angle, Vector3.forward) * forwardVector;
                forwardVector = myWeapon.transform.rotation * forwardVector;
                if (Physics.Raycast(myWeapon.transform.position, forwardVector, out hit, range))
                {
                    Debug.DrawRay(myWeapon.transform.position, forwardVector * hit.distance, Color.yellow);
                    // damage target or create decal or whatever. 
                    Debug.Log(hit.transform.name);
                }
                //create a bullet trails pool which can be pulled from for any weapon? 

                BulletTrail = ObjectPoolingManager.Instance.GetObject("bullet");
                BulletTrail.SetActive(true);
                BulletTrail.GetComponent<MusselEffect>().SetPositions(myWeapon.transform.position, hit.point);
            }
            ammo--;
            ApplyRecoil(myWeapon);
            //ApplyRecoil(myCamera.gameObject);
        }
        else
        {

            Debug.Log("weapon empty");
            Reload(myWeaponManager);
            //Click empty
        }
        yield return null;

        while (notCounter <= rateOfFire && notCounter > 0)
        {
            notCounter -= Time.deltaTime;
            //Debug.Log("weapon not ready: "+counter.ToString());
            yield return null;
        }
    }
    //override the fire script to to shotgun raycast things
}
