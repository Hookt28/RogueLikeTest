using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenPieces : MonoBehaviour
{

    public float moveSpeed = 3f;
    private Vector3 moveDirection;

    public float deceleration = 5f;

    public float lifetime = 3f;

    public SpriteRenderer theSR;
    public float fadeSpeed = 2.5f;



    // Start is called before the first frame update
    void Start()
    {
        moveDirection.x = Random.Range(-moveSpeed, moveSpeed);
        moveDirection.y = Random.Range(-moveSpeed, moveSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += moveDirection * Time.deltaTime;

        moveDirection = Vector3.Lerp(moveDirection, Vector3.zero, deceleration * Time.deltaTime);  // so pieces dont fly off the screen.  lerp will make it go 50% each frame.  so it will slow down until it gets to 0.

        lifetime -= Time.deltaTime;

        if(lifetime <0)
        {
            theSR.color = new Color(theSR.color.r, theSR.color.g, theSR.color.b, Mathf.MoveTowards(theSR.color.a, 0f, fadeSpeed * Time.deltaTime));  // make the pieces fade to nothing.  MathF is similar to Lerp but will go to a certain value

            if (theSR.color.a == 0f)
            {
                Destroy(gameObject);
            }
        }
    }
}
