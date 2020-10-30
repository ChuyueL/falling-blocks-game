using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 7;
    public event System.Action OnPlayerDeath;

    float screenHalfWidthInWorldUnits;
    Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        //need to account for width of player when wrapping around screen
        //so that player moves only once it is completely out of the screen
        float halfPlayerWidth = transform.localScale.x / 2f;
        screenHalfWidthInWorldUnits = cam.aspect * cam.orthographicSize + halfPlayerWidth;
    }

    // Update is called once per frame
    void Update()
    {
        /*
        Vector2 direction = new Vector2(Input.GetAxisRaw("Horizontal"), 0);
        Vector2 velocity = direction * speed;

        Vector2 amountToMove = velocity * Time.deltaTime;

        transform.Translate(amountToMove);
        */

        float inputX = Input.GetAxisRaw("Horizontal");
        float velocity = speed * inputX;

        transform.Translate(Vector2.right * velocity * Time.deltaTime);

        /*
        Vector2 currentPos = transform.position;
        Vector2 screenPos = cam.WorldToScreenPoint(currentPos);

        if (screenPos.x > Screen.width)
        {
            transform.Translate(Vector2.left * 18);

        }
        else if (screenPos.x < 0)
        {
            transform.Translate(Vector2.right * 18);
        }
        */

        if (transform.position.x < -screenHalfWidthInWorldUnits)
        {
            transform.position = new Vector2(screenHalfWidthInWorldUnits, transform.position.y);
        }
        else if (transform.position.x > screenHalfWidthInWorldUnits)
        {
            transform.position = new Vector2(-screenHalfWidthInWorldUnits, transform.position.y);
        }
    }

    void OnTriggerEnter2D(Collider2D triggerCollider)
    {
        if (triggerCollider.tag == "Falling Block")
        {
            //elvis operator - equivalent to 'if OnPlayerDeath != null, invoke'
            OnPlayerDeath?.Invoke();
            Destroy(gameObject);
        }
    }
}
