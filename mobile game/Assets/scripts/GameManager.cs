using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance{get {return instance;}}


    public int currentSkinIndex = 0;
    public int lives = 0;
    public int currency = 0;
    public int skinAvailability = 1;

    
    private void Awake()
    {

        //instance = this;
        if (Instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else if (Instance != null)
        {
            Destroy(this);
        }
        
      
        if (PlayerPrefs.HasKey("CurrentSkin"))
        {
            currentSkinIndex = PlayerPrefs.GetInt("CurrentSkin");
            currency = PlayerPrefs.GetInt("Currency");
            skinAvailability = PlayerPrefs.GetInt("skinAvailability");
            lives = PlayerPrefs.GetInt("lives");
        }
        else
        {
           save();
        }
    }
    public void save()
    {
        PlayerPrefs.SetInt("lives", lives);
        PlayerPrefs.SetInt("CurrentSkin", currentSkinIndex);
        PlayerPrefs.SetInt("Currency", currency);
        PlayerPrefs.SetInt("skinAvailability", skinAvailability);
    }

}
