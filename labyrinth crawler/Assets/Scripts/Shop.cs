using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    [SerializeField] int price = 1;
    [SerializeField] int item = 1;
    [SerializeField] AudioClip sfx;
    [SerializeField] Sprite bomb;
    [SerializeField] Sprite scroll;
    [SerializeField] SpriteRenderer purchaseSprite;
    [SerializeField] Text priceText;
    void Start(){
        priceText.text = price.ToString();
        if(item == 1){
            purchaseSprite.sprite = bomb;
        } else if(item == 2){
            purchaseSprite.sprite = scroll;
        }
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (FindObjectOfType<Player>().RemoveCoins(price))
        {
            AudioSource.PlayClipAtPoint(sfx, Camera.main.transform.position);
            if(item == 1)
            {
                FindObjectOfType<Player>().AddBomb();
            } else if (item == 2)
            {
                FindObjectOfType<Player>().AddScroll();
            } 
            
        }
    }
}
