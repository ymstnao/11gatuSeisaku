using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatus : MonoBehaviour {
    //MaxHP
    [SerializeField]
    private float maxHp = 100;//HPの最大値
    private float nowHp = 0;//現在のHP
    [SerializeField]
    private Slider hpBar;//HPBarの参照

　　 // Use this for initialization
    void Start () {
        nowHp = maxHp;
        hpBar.GetComponent<Slider>();
        hpBar.maxValue = maxHp;
        hpBar.value = nowHp;
    }
	// Update is called once per frame
	void Update ()
    {
        Death();
    }
    /// <summary>
    /// ダメージ処理
    /// </summary>
    public void Damage(float damage)
    {
        nowHp -= damage;
        hpBar.value = nowHp;
    }
    /// <summary>
    /// HPが0になった時の処理
    /// </summary>
    void Death()
    {
        if (nowHp <= 0)
        {
            PlayerFlagManager.death_flag = true;
        }
    }
}
