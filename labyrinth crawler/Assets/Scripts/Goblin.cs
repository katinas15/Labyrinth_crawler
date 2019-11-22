using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin : MonoBehaviour
{
    [SerializeField] float walkingDistance = 5f;
    [SerializeField] float speed = 0.5f;
    [Tooltip("Horizontal = false")]
    [SerializeField] bool walkingDirection = false;
    BoxCollider2D boxCollider;

    float startPosX;
    float startPosY;
    bool currentDirection = true;
    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        startPosX = transform.position.x;
        startPosY = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {

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

        if(transform.position.x > startPosX + walkingDistance || 
            transform.position.y > startPosY + walkingDistance)
        {
            currentDirection = false;
        } else if (transform.position.x <= startPosX ||
            transform.position.y <= startPosY)
        {
            currentDirection = true;
        }

    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (boxCollider.IsTouchingLayers(LayerMask.GetMask("Projectile")))
        {
            Destroy(gameObject);
        }
    }
}
