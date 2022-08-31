using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{

    public bool closeWhenEntered; // openWhenEnemiesCleared

    public GameObject[] doors;  // array

    //public List<GameObject> enemies = new List<GameObject>();  // list of game objects  new game object created an empty list in unity

    [HideInInspector]
    public bool roomActive;

    public GameObject mapHider;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /* if(enemies.Count > 0 && roomActive && openWhenEnemiesCleared)
        {
            for(int i = 0; i < enemies.Count; i++)
            {
                if (enemies[i] == null)
                {
                    enemies.RemoveAt(i);
                    
                    i--;                 // when you remove an element from the list, there will be one less element - so if there were 3 and you delete one, now there are only 2 elements.  this reduced the amount by 1 list after every kill
                }
            }

            if(enemies.Count == 0)
            {
                foreach (GameObject door in doors) 
                {
                    door.SetActive(false);

                    closeWhenEntered = false;  // doors wont close if the room is empty so after clearning and going back in you wont get locked in it.
                }
            }
        } */
    }

    public void OpenDoors()
    {
        foreach (GameObject door in doors)
        {
            door.SetActive(false);

            closeWhenEntered = false;  // doors wont close if the room is empty so after clearning and going back in you wont get locked in it.
        }
    }

     private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            CameraController.instance.ChangeTarget(transform);

            if(closeWhenEntered)
            {
                foreach(GameObject door in doors)  // can use anything for "door"
                {
                    door.SetActive(true);
                }
            }

            roomActive = true;

            mapHider.SetActive(false);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            roomActive = false;
        }
    }
}
