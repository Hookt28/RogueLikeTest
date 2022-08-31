using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteSortOrder : MonoBehaviour
{
    private SpriteRenderer theSR;

    // Start is called before the first frame update
    void Start()
    {
        theSR = GetComponent<SpriteRenderer>();

        theSR.sortingOrder = Mathf.RoundToInt( transform.position.y * -10f);  // position of the boxes (layers) -10 so that they dont stack on top of each other.
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
