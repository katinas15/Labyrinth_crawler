using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] AudioClip sfx;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player"){
            AudioSource.PlayClipAtPoint(sfx, Camera.main.transform.position);
            FindObjectOfType<Player>().AddCoin();
            Destroy(gameObject);
        }
        
    }
}
