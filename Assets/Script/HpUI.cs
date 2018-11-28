using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpUI : MonoBehaviour {
    [SerializeField]
    private Slider bar;
    [SerializeField]
    private Text text;
    [SerializeField]
    private Image image;
    

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    /// <summary>
    /// UIの更新
    /// </summary>
    /// <param name="maxHp"></param>
    /// <param name="nowHp"></param>
    public void UIChange(float maxHp, float nowHp)
    {
        BarUpdate(maxHp,nowHp);
        TextUpdate(maxHp, nowHp);
    }
    public void BarUpdate(float maxHp,float nowHp)
    {
        bar.maxValue = maxHp;
        bar.value = nowHp;
    }
    public void TextUpdate(float maxHp, float nowHp)
    {
        text.text = string.Format("{0}/{1}", nowHp, maxHp);
    }
    public void ImageUpdate()
    {
    }
}
