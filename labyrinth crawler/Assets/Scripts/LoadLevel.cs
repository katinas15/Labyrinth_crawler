using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevel : MonoBehaviour
{
    [SerializeField] int levelIndex;

    void Start() {
        if(PlayerPrefsController.GetLevelProgress() < levelIndex){
            gameObject.SetActive(false);
        }
    }

    public void Load()
    {
        SceneManager.LoadScene(levelIndex);
    }
}
