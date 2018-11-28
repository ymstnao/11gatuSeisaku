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
    private HpUI hpUI;//HPBarの参照
    //攻撃力
    [SerializeField]
    private float attackPower = 10;//プレイヤーの攻撃力

    public float AttackPower
    {
        get
        {
            return attackPower;
        }

        set
        {
            attackPower = value;
        }
    }
    public float MaxHp
    {
        get
        {
            return maxHp;
        }

        set
        {
            maxHp = value;
        }
    }
    public float NowHp
    {
        get
        {
            return nowHp;
        }

        set
        {
            nowHp = value;
        }
    }

    // Use this for initialization
    void Start () {
        NowHp = MaxHp;
        hpUI.GetComponent<Slider>();
        hpUI.UIChange(MaxHp, NowHp);
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
        NowHp -= damage;
        hpUI.UIChange(MaxHp, NowHp);
    }
    /// <summary>
    /// HPが0になった時の処理
    /// </summary>
    void Death()
    {
        if (NowHp <= 0)
        {
            PlayerFlagManager.death_flag = true;
        }
    }
    public void Absorption(EnemyParameter targetParameter)
    {
        MaxHp += targetParameter.MaxHp;
        NowHp += targetParameter.MaxHp;
        hpUI.UIChange(MaxHp, NowHp);
        attackPower += targetParameter.AttackPower;
    }
    public void HpChange(float value)
    {
        nowHp = value;
        hpUI.UIChange(maxHp,nowHp);
    }
}
