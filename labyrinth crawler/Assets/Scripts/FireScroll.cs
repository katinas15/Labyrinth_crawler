using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireScroll : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        FindObjectOfType<Player>().AddScroll();
        Destroy(gameObject);
    }
}
