using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin : MonoBehaviour
{
    [SerializeField] float walkingDistance = 5f;
    [SerializeField] float speed = 0.5f;
    [Tooltip("Horizontal = false")]
    [SerializeField] bool walkingDirection = false;

    [SerializeField] bool reverseWalking = false;
    BoxCollider2D boxCollider;

    int startPosX;
    int startPosY;
    bool currentDirection = true;
    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        startPosX = (int) transform.position.x;
        startPosY = (int) transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        int posX = (int) transform.position.x;
        int posY = (int) transform.position.y;
        if(reverseWalking){
            if(!walkingDirection && currentDirection)
            {
                transform.Translate(Vector2.left * speed * Time.deltaTime);
            } else if(!walkingDirection && !currentDirection)
            {
                transform.Translate(Vector2.right * speed * Time.deltaTime);
            }
            else if (walkingDirection && currentDirection)
            {
                transform.Translate(Vector2.down * speed * Time.deltaTime);
            }
            else if (walkingDirection && !currentDirection)
            {
                transform.Translate(Vector2.up * speed * Time.deltaTime);
            }

            if( FindDifference(posX, startPosX) > walkingDistance || 
                FindDifference(posY, startPosY) > walkingDistance)
            {
                currentDirection = false;
            } 
            
            if (startPosX < posX || startPosY < posY)
            {
                currentDirection = true;
            }
        } else {

            if(!walkingDirection && currentDirection)
            {
                transform.Translate(Vector2.right * speed * Time.deltaTime);
            } else if(!walkingDirection && !currentDirection)
            {
                transform.Translate(Vector2.left * speed * Time.deltaTime);
            }
            else if (walkingDirection && currentDirection)
            {
                transform.Translate(Vector2.up * speed * Time.deltaTime);
            }
            else if (walkingDirection && !currentDirection)
            {
                transform.Translate(Vector2.down * speed * Time.deltaTime);
            }

            if( FindDifference(posX, startPosX) > walkingDistance || 
                FindDifference(posY, startPosY) > walkingDistance)
            {
                currentDirection = false;
            } 
            if (startPosX > posX || startPosY > posY)
            {
                currentDirection = true;
            }

        }

        if (boxCollider.IsTouchingLayers(LayerMask.GetMask("Projectile")))
        {
            Destroy(gameObject);
        }

    }

    private void OnCollisionEnter2D(Collision2D other) {
        currentDirection = !currentDirection;
    }

    public float FindDifference(float nr1, float nr2)
    {
        return Mathf.Abs(nr1 - nr2);
    }

}
