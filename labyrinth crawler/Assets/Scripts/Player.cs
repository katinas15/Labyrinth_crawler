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
    [SerializeField] Text coinText;
    [SerializeField] Tilemap tilemap;
    [SerializeField] Fire fire;
    [SerializeField] float fireDelta = 0.5f;

    float nextFire = 0.5f;
    float myTime = 0.0f;

    int bombs = 0;
    int fireScrolls = 0;
    int coins = 0;

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
        myTime = myTime + Time.deltaTime;
        Move();
        Fire();
    }

    private void Fire()
    {
        if (CrossPlatformInputManager.GetButton("Fire1") && myTime > nextFire)
        {
            nextFire = myTime + fireDelta;
            UseFireScroll();
            nextFire = nextFire - myTime;
            myTime = 0.0F;
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
        coinText.text = coins.ToString();
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

    public void AddCoin()
    {
        coins++;
        UpdateUI();
    }

    public void UseFireScroll()
    {
        var deltaY = CrossPlatformInputManager.GetAxisRaw("Vertical") * Time.deltaTime * movementSpeed;
        Fire newFire = Instantiate(fire, new Vector2(transform.position.x, transform.position.y), transform.rotation);

        var deltaX = CrossPlatformInputManager.GetAxisRaw("Horizontal") * Time.deltaTime * movementSpeed;
        if(deltaX > 0f)
        {
            newFire.GetComponent<Fire>().SetDirection(1);
        }
        else if(deltaX <0f)
        {
            newFire.GetComponent<Fire>().SetDirection(3);
        }
        else if (deltaY > 0f)
        {
            newFire.GetComponent<Fire>().SetDirection(4);
        }
        else if (deltaY < 0f)
        {
            newFire.GetComponent<Fire>().SetDirection(2);
        } else
        {
            newFire.GetComponent<Fire>().SetDirection(1);
        }

    }

}
