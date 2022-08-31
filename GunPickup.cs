using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunPickup : MonoBehaviour
{
    public Gun theGun;

    public float waitToBeCollected = .5f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (waitToBeCollected > 0)
        {
            waitToBeCollected -= Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && waitToBeCollected <= 0)
        {
            bool hasGun = false;

            foreach(Gun gunToCheck in PlayerController.instance.availableGuns)
            {
                if(theGun.weaponName == gunToCheck.weaponName)
                {
                    hasGun = true;
                }
            }

            if(!hasGun)  //
            {
                Gun gunClone = Instantiate(theGun);
                gunClone.transform.parent = PlayerController.instance.gunArm;  // get the gun to the player
                gunClone.transform.position = PlayerController.instance.gunArm.position;  // now move the gun to the player postion
                gunClone.transform.localRotation = Quaternion.Euler(Vector3.zero);  // localRotation so that gun will be rotated in the correct way when it is picked up.
                gunClone.transform.localScale = Vector3.one;  // so that when you pick up from teh right side, the gun is not upsidedown

                // need to switch to the new gun when picked up.
                PlayerController.instance.availableGuns.Add(gunClone);
                PlayerController.instance.currentGun = PlayerController.instance.availableGuns.Count - 1;  // 
                PlayerController.instance.SwitchGun();

            }

            Destroy(gameObject);

            AudioManager.instance.PlaySFX(6);
        }
    }
}

