using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    [SerializeField] GameObject canvas;
    [SerializeField] AudioClip winSfx;    

    private void OnTriggerEnter2D(Collider2D other)
    {
        AudioSource.PlayClipAtPoint(winSfx, Camera.main.transform.position);
        canvas.SetActive(true);
    }
}
