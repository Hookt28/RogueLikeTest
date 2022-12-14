using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    
    public float moveSpeed;
    private Vector2 moveInput;

    public Rigidbody2D theRB;

    public Transform gunArm;

   // private Camera theCam;  // Added to make more effecient

    public Animator anim;

    /* public GameObject bulletToFire;
    public Transform firePoint;

    public float timeBetweenShots;
    private float shotCounter; */

    public SpriteRenderer bodySR;

    private float activeMoveSpeed;  // dash speed back to normal?
    public float dashSpeed = 8f, dashLength = .5f, dashCooldown = 1f, dashInvincibility = .5f;  // dash move speed and how long our dash should be and cooldown.  comma adds multple variables on the same line
    
    [HideInInspector]
    public float dashCounter;
    private float  dashCoolCounter;  //  private cant be seen by other scripts

    [HideInInspector]
    public bool canMove = true;  // true by default - making the player unable to move when exiting level

    public List<Gun> availableGuns = new List<Gun>();
    [HideInInspector]
    public int currentGun;


    private void Awake()
    {
        instance = this;

        DontDestroyOnLoad(gameObject);  // dont destroy the player when load new scene
    }

    // Start is called before the first frame update
    void Start()
    {
        //theCam = Camera.main;

        activeMoveSpeed = moveSpeed;

        UIController.instance.currentGun.sprite = availableGuns[currentGun].gunUI;
        UIController.instance.gunText.text = availableGuns[currentGun].weaponName;
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove && !LevelManager.instance.isPaused)  // so that you can't move during level exit and if the game isnt paused, we can move.
        {

            moveInput.x = Input.GetAxisRaw("Horizontal");
            moveInput.y = Input.GetAxisRaw("Vertical");


            moveInput.Normalize();  // Make daigonal movement match up and down speed

            //transform.position += new Vector3(moveInput.x * Time.deltaTime * moveSpeed, moveInput.y * Time.deltaTime * moveSpeed, 0f);

            theRB.velocity = moveInput * activeMoveSpeed;

            Vector3 mousePos = Input.mousePosition;
            Vector3 screenPoint = CameraController.instance.mainCamera.WorldToScreenPoint(transform.localPosition);

            // Change looking direction
            if (mousePos.x < screenPoint.x)
            {
                transform.localScale = new Vector3(-1f, 1f, 1f);
                gunArm.localScale = new Vector3(-1f, -1f, 1f);
            }
            else
            {
                transform.localScale = Vector3.one; // Same as -1f, 1f, 1f - f means float value
                gunArm.localScale = Vector3.one;
            }

            //rotate gun arm
            Vector2 offset = new Vector2(mousePos.x - screenPoint.x, mousePos.y - screenPoint.y);
            float angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
            gunArm.rotation = Quaternion.Euler(0, 0, angle);


            // When the player fires gun
           /* if (Input.GetMouseButtonDown(0))
            {
                Instantiate(bulletToFire, firePoint.position, firePoint.rotation);
                shotCounter = timeBetweenShots;
                AudioManager.instance.PlaySFX(12);
            }
            if (Input.GetMouseButton(0))  // Held down mouse button
            {
                shotCounter -= Time.deltaTime;

                if (shotCounter <= 0)  // is it equal to 0
                {
                    Instantiate(bulletToFire, firePoint.position, firePoint.rotation);
                    AudioManager.instance.PlaySFX(12);

                    shotCounter = timeBetweenShots;
                }
            }*/

            if(Input.GetKeyDown(KeyCode.Tab))
            {
                if(availableGuns.Count > 0)
                {
                    currentGun++;
                    if(currentGun >= availableGuns.Count)
                    {
                        currentGun = 0;
                    }

                    SwitchGun();

                } else
                {
                    Debug.LogError("Player has no guns!");
                }
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (dashCoolCounter <= 0 && dashCounter <= 0)
                {
                    activeMoveSpeed = dashSpeed;
                    dashCounter = dashLength;

                    anim.SetTrigger("dash");

                    PlayerHealthController.instance.MakeInvincible(dashInvincibility);

                    AudioManager.instance.PlaySFX(8);
                }
            }

            if (dashCounter > 0)
            {
                dashCounter -= Time.deltaTime;
                if (dashCounter <= 0)
                {
                    activeMoveSpeed = moveSpeed;
                    dashCoolCounter = dashCooldown;
                }
            }

            if (dashCoolCounter > 0)
            {
                dashCoolCounter -= Time.deltaTime;
            }




            // if it is moving

            if (moveInput != Vector2.zero)
            {
                anim.SetBool("isMoving", true);
            }
            else
            {
                anim.SetBool("isMoving", false);
            }
        } else
        {
            theRB.velocity = Vector2.zero;
            anim.SetBool("isMoving", false); // if you cant move, then stop moving ( for exit of the level)
        }

    }

    public void SwitchGun()  // switching guns
    {
        foreach (Gun theGun in availableGuns)
        {
            theGun.gameObject.SetActive(false);
        }

        availableGuns[currentGun].gameObject.SetActive(true);

        UIController.instance.currentGun.sprite = availableGuns[currentGun].gunUI;
        UIController.instance.gunText.text = availableGuns[currentGun].weaponName;

    }
}
