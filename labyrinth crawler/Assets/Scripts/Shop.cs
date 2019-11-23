using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] int price = 1;
    [SerializeField] int item = 1;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (FindObjectOfType<Player>().RemoveCoins(price))
        {
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
