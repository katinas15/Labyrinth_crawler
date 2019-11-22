using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

public class Player : MonoBehaviour
{
    [SerializeField] float movementSpeed = 1f;

    [SerializeField] Text bombText;
    [SerializeField] Text scrollText;
    [SerializeField] Tilemap tilemap;
    [SerializeField] GameObject fire;

    int bombs = 0;
    int fireScrolls = 0;
    Rigidbody2D rigidBody;
    BoxCollider2D boxCollider;
    Animator animator;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        
        UpdateUI();
    }

    void FixedUpdate()
    {
        Move();
        Fire();
    }

    private void Fire()
    {
        if (CrossPlatformInputManager.GetButton("Fire1"))
        {
            UseFireScroll();
        }
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

    private void UpdateUI()
    {
        bombText.text = bombs.ToString();
        scrollText.text = fireScrolls.ToString();
    }

    public void AddScroll()
    {
        fireScrolls++;
        UpdateUI();
    }

    public void AddBomb()
    {
        bombs++;
        UpdateUI();
    }

    public void UseFireScroll()
    {
        Instantiate(fire, transform.position, transform.rotation);
    }

}
