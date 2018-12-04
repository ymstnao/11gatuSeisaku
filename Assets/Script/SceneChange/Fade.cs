using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour {
    [SerializeField]
    private GameObject fadePanel,fadeText;
    [SerializeField]
    private float speed = 0.1f;
    private float imageRed, imageGreen, imageBlue,imageAlfa;//Image用 R,G,B
    private float textRed, textGreen, textBlue,textAlfa;//Text用 R,G,B

    // Use this for initialization
    void Start () {
        Initialize();
    }
    void Initialize()
    {
        //パネル用
        imageRed = fadePanel.GetComponent<Image>().color.r;
        imageGreen = fadePanel.GetComponent<Image>().color.g;
        imageBlue = fadePanel.GetComponent<Image>().color.b;
        imageAlfa = fadePanel.GetComponent<Image>().color.a;
        imageAlfa = 0.7f;
        //Text用
        textRed = fadeText.GetComponent<Text>().color.r;
        textGreen = fadeText.GetComponent<Text>().color.g;
        textBlue = fadeText.GetComponent<Text>().color.b;
        textAlfa = fadeText.GetComponent<Text>().color.a;
        textAlfa = 1.0f;
    }
	// Update is called once per frame
	void FixedUpdate () {
        ImageFade();
        TextFade();
        if (imageAlfa <= 0 && imageAlfa <= 0)
        {
            Initialize();
            this.gameObject.SetActive(false);
        }
    }
    void ImageFade()
    {
        fadePanel.GetComponent<Image>().color = new Color(imageRed, imageGreen, imageBlue, imageAlfa);
        imageAlfa -= speed * Time.fixedDeltaTime;
    }
    void TextFade()
    {
        fadeText.GetComponent<Text>().color = new Color(textRed, textGreen, textBlue, textAlfa);
        textAlfa -= speed * Time.fixedDeltaTime;
        
    }
}
