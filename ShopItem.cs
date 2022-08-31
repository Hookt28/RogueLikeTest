using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;  // added for gun shop items

public class ShopItem : MonoBehaviour
{

    public GameObject buyMessage;

    private bool inBuyZone;  // set up so we can buy

    public bool isHealthRestore, isHealthUpgrade, isWeapon;

    public int itemCost;

    public int healthUpgradeAmount;

    public Gun[] potentialGuns;
    private Gun theGun;
    public SpriteRenderer gunSprite;
    public Text infoText;

    // Start is called before the first frame update
    void Start()
    {
        if(isWeapon)
        {
            int selectedGun = Random.Range(0, potentialGuns.Length);
            theGun = potentialGuns[selectedGun];

            gunSprite.sprite = theGun.gunShopSprite;
            infoText.text = theGun.weaponName + "\n - " + theGun.itemCost + " Gold - "; // \n translate it into a return/enter
            itemCost = theGun.itemCost;

        }
    }

    // Update is called once per frame
    void Update()
    {
        if(inBuyZone)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                if(LevelManager.instance.currentCoins >= itemCost) // do we have enough coins
                {
                    LevelManager.instance.SpendCoins(itemCost);

                    if(isHealthRestore)
                    {
                        PlayerHealthController.instance.HealPlayer(PlayerHealthController.instance.maxHealth);
                    }

                    if(isHealthUpgrade)
                    {
                        PlayerHealthController.instance.IncreaseMaxHealth(healthUpgradeAmount);
                    }
                    if(isWeapon)
                    {
                        Gun gunClone = Instantiate(theGun);  // copied from gunpickup script
                        gunClone.transform.parent = PlayerController.instance.gunArm;  // get the gun to the player
                        gunClone.transform.position = PlayerController.instance.gunArm.position;  // now move the gun to the player postion
                        gunClone.transform.localRotation = Quaternion.Euler(Vector3.zero);  // localRotation so that gun will be rotated in the correct way when it is picked up.
                        gunClone.transform.localScale = Vector3.one;  // so that when you pick up from teh right side, the gun is not upsidedown

                        // need to switch to the new gun when picked up.
                        PlayerController.instance.availableGuns.Add(gunClone);
                        PlayerController.instance.currentGun = PlayerController.instance.availableGuns.Count - 1;  // 
                        PlayerController.instance.SwitchGun();
                    }


                    gameObject.SetActive(false);
                    inBuyZone = false;  // wont buy twice

                    AudioManager.instance.PlaySFX(18);
                } else
                {
                    AudioManager.instance.PlaySFX(19);
                }

            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)  //  Message will appear when player walks into the area.
    {
        if(other.tag == "Player")
        {
            buyMessage.SetActive(true);

            inBuyZone = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)  //  Message will appear when player walks out the area.
    {
        if (other.tag == "Player")
        {
            buyMessage.SetActive(false);

            inBuyZone = false;
        }
    }

}
