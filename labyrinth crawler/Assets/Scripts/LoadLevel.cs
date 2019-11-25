using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevel : MonoBehaviour
{
    [SerializeField] int levelIndex;

    public void Load()
    {
        SceneManager.LoadScene(levelIndex);
    }
}
