using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsController : MonoBehaviour
{
    const string LEVEL_PROGRESS_KEY = "level progress";

    public static void ResetLevelProgress(){
        PlayerPrefs.SetInt(LEVEL_PROGRESS_KEY, 1);
    }
    public static void SetLevelProgress(int value){
        if(value > GetLevelProgress()){
            PlayerPrefs.SetInt(LEVEL_PROGRESS_KEY, value);
        }
    }

    public static int GetLevelProgress(){
        return PlayerPrefs.GetInt(LEVEL_PROGRESS_KEY);
    }
}