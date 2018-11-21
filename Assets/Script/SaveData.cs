using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class SaveData : MonoBehaviour
{
    [SerializeField]
    private Text hp;

    private Dictionary<string, int> playerState;

    void Start()
    {
        playerState = new Dictionary<string, int>();
    }
    public void Save()
    {
        PlayerPrefs.SetInt("HP", 50);
        PlayerPrefs.Save();
    }

    public void Load()
    {
        hp.text =  PlayerPrefs.GetInt("HP",0).ToString();
    }
    public void Delete()
    {
        PlayerPrefs.DeleteAll();
    }
}