using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
    [SerializeField] float movementSpeed = 1f;

    Rigidbody2D rigidBody;
    BoxCollider2D boxCollider;
    Animator animator;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        var deltaX = CrossPlatformInputManager.GetAxisRaw("Horizontal") * Time.deltaTime * movementSpeed;
        var newXPos = transform.position.x + deltaX;
        var deltaY = CrossPlatformInputManager.GetAxisRaw("Vertical") * Time.deltaTime * movementSpeed;
        var newYPos = transform.position.y + deltaY;

        if (deltaY > 0f)
        {
            animator.SetBool("NormalWalk", false);
            animator.SetBool("BackWalk", true);
        }
        else if (deltaY < 0f)
        {
            animator.SetBool("NormalWalk", true);
            animator.SetBool("BackWalk", false);
        } else if (deltaX != 0)
        {
            animator.SetBool("NormalWalk", true);
            animator.SetBool("BackWalk", false);
        } else
        {
            animator.SetBool("NormalWalk", false);
            animator.SetBool("BackWalk", false);
        }

        if (deltaX < 0f)
        {
            transform.localScale = new Vector2(-1f, 1f);
        }
        else
        {
            transform.localScale = new Vector2(1f, 1f);
        }

        transform.position = new Vector2(newXPos, newYPos);
    }
}
