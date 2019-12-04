using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    [SerializeField] float speed = 4f;
    float deleteTime = 5f;
    int direction;
    BoxCollider2D boxCollider;
    public void SetDirection(int direction)
    {
        this.direction = direction;
    }

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        if (boxCollider.IsTouchingLayers(LayerMask.GetMask("Foreground")))
        {
            Destroy(gameObject);
        }

        if (boxCollider.IsTouchingLayers(LayerMask.GetMask("Enemy")))
        {
            Destroy(gameObject);
        }

        if (direction == 1)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        } 
        else if (direction == 2)
        {
            transform.Translate(Vector2.down * speed * Time.deltaTime);
        }
        else if (direction == 3)
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        }
        else if (direction == 4)
        {
            transform.Translate(Vector2.up * speed * Time.deltaTime);
        }
    }

}
