﻿using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BossStage : MonoBehaviour {

    [SerializeField]
    private string goal_Scene, death_Scene;
    [SerializeField]
    private float speed = 0.01f;
    [SerializeField]
    private float time = 3f;
    private float alfa;
    private float red, green, blue;
    [SerializeField]
    private GameObject fadePanel;
    // Use this for initialization
    void Start()
    {
        red = fadePanel.GetComponent<Image>().color.r;
        green = fadePanel.GetComponent<Image>().color.g;
        blue = fadePanel.GetComponent<Image>().color.b;
        alfa = fadePanel.GetComponent<Image>().color.a;
    }
    void Update()
    {
    }
    void Goal()
    {
        fadePanel.GetComponent<Image>().color = new Color(red, green, blue, alfa);
        alfa += speed * Time.deltaTime;
        time -= speed * Time.deltaTime;
        if (time <= 0)
        {
            SceneManager.LoadScene(goal_Scene);
        }
    }
    void Death()
    {
        fadePanel.GetComponent<Image>().color = new Color(red, green, blue, alfa);
        alfa += speed * Time.deltaTime;
        time -= speed * Time.deltaTime;
        if (time <= 0)
        {
            SceneManager.LoadScene(death_Scene);
        }
    }
}
