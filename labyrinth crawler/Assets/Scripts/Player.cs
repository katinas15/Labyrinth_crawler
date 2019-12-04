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
    [SerializeField] float bombDelta = 0.5f;

    [SerializeField] float deathPush = 5f;

    [SerializeField] GameObject gameScreen;
    [SerializeField] GameObject deathScreen;
    [SerializeField] GameObject pauseMenu;
    [SerializeField] AudioClip bombSfx;    
    [SerializeField] AudioClip fireSfx;
    [SerializeField] AudioClip deathSfx;
    float nextFire = 0.5f;
    float nextBomb = 0.5f;
    float nextPress = 0.5f;

    float myTime = 0.0f;
    bool stop = false;
    int bombs = 0;
    int fireScrolls = 0;
    int coins = 0;

    Rigidbody2D rigidBody;
    BoxCollider2D boxCollider;
    Animator animator;

    int lastKeypress = 0;

    
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
        if(!stop){
            Move();
            Fire();
            Bomb();
            Enemy();
        }
        Pause();
        
    }

    private void Pause(){
        if (CrossPlatformInputManager.GetButton("Cancel") && myTime > nextPress)
        {
            if(pauseMenu.activeSelf){
                stop = true;
                pauseMenu.SetActive(false);
            } else {
                stop = false;
                pauseMenu.SetActive(true);
            }

            nextPress = myTime + 1f;
            nextPress = nextPress - myTime;
            myTime = 0.0F;
        }
    }

    private void Enemy()
    {
        if (boxCollider.IsTouchingLayers(LayerMask.GetMask("Enemy")))
        {
            boxCollider.enabled = false;
            deathScreen.SetActive(true);
            rigidBody.velocity = new Vector2(deathPush, deathPush);
            gameScreen.SetActive(false);
            AudioSource.PlayClipAtPoint(deathSfx, Camera.main.transform.position);
            
        }
    }

    private void Bomb()
    {
        if (CrossPlatformInputManager.GetButton("Fire2") && myTime > nextBomb)
        {
            nextBomb = myTime + bombDelta;
            UseBomb();
            nextBomb = nextBomb - myTime;
            myTime = 0.0F;
        }
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
            lastKeypress = 4;
            animator.SetBool("NormalWalk", false);
            animator.SetBool("BackWalk", true);
        }
        else if (deltaY < 0f)
        {
            lastKeypress = 2;
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
            lastKeypress = 3;
            transform.localScale = new Vector2(-1f, 1f);
        }
        else if(deltaX > 0f)
        {
            lastKeypress = 1;
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
        if(fireScrolls > 0)
        {
            Fire newFire = Instantiate(fire, new Vector2(transform.position.x, transform.position.y), transform.rotation);

            newFire.GetComponent<Fire>().SetDirection(lastKeypress);
            AudioSource.PlayClipAtPoint(fireSfx, Camera.main.transform.position);

            fireScrolls--;
            UpdateUI();
        }
    }

    public void UseBomb()
    {
        if (bombs > 0)
        {
            int playerX = (int)transform.position.x;
            int playerY = (int)transform.position.y;

            if(0 > transform.position.x) playerX -= 1;
            if(0 > transform.position.y) playerY -= 1;

            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    var tilePos = new Vector3Int(playerX + j, playerY + i, 0);
                    tilemap.SetTile(tilePos, null);
                }
            }
            AudioSource.PlayClipAtPoint(bombSfx, Camera.main.transform.position);
            bombs--;
            UpdateUI();
        }
    }

    public bool RemoveCoins(int price)
    {
        if(coins - price < 0)
        {
            return false;
        }
        else
        {
            coins -= price;
            return true;
        }
    }

    public void StopPlayer(){
        stop = true;
    }
}
