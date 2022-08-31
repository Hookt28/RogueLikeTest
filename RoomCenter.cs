using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomCenter : MonoBehaviour
{
    public bool openWhenEnemiesCleared;

    public List<GameObject> enemies = new List<GameObject>();  // list of game objects  new game object created an empty list in unity

    public Room theRoom;

    // Start is called before the first frame update
    void Start()
    {
        if(openWhenEnemiesCleared)
        {
            theRoom.closeWhenEntered = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (enemies.Count > 0 && theRoom.roomActive && openWhenEnemiesCleared)
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                if (enemies[i] == null)
                {
                    enemies.RemoveAt(i);

                    i--;                 // when you remove an element from the list, there will be one less element - so if there were 3 and you delete one, now there are only 2 elements.  this reduced the amount by 1 list after every kill
                }
            }

            if (enemies.Count == 0)
            {
                theRoom.OpenDoors();         
            }
        }
    }
}
