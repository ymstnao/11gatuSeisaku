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
    private StatuUI statuUI;//HPBarの参照
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
        statuUI.GetComponent<Slider>();
        statuUI.HPUIChange(MaxHp, NowHp);
        statuUI.AttackPowerUIChange(attackPower);
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
        statuUI.HPUIChange(MaxHp, NowHp);
    }
    /// <summary>
    /// HPが0になった時の処理
    /// </summary>
    void Death()
    {
        if (NowHp <= 0)
        {
            DeathPenalty();
            PlayerFlagManager.death_flag = true;
        }
    }
    /// <summary>
    /// 死んだ場合のデスペナルティ
    /// </summary>
    public void DeathPenalty()
    {
        MaxHp -= 10;
        NowHp -= 10;
        statuUI.HPUIChange(MaxHp, NowHp);

        attackPower -= 10;
        statuUI.AttackPowerUIChange(attackPower);
    }
    public void Absorption(EnemyParameter targetParameter)
    {
        MaxHp += targetParameter.MaxHp;
        NowHp += targetParameter.MaxHp;
        statuUI.HPUIChange(MaxHp, NowHp);

        attackPower += targetParameter.AttackPower;
        statuUI.AttackPowerUIChange(attackPower);
    }
    public void HpChange(float value)
    {
        nowHp = value;
        statuUI.HPUIChange(maxHp,nowHp);
    }
    public void RespownPosition(int number,Transform player)
    {
        if (PlayerPrefs.GetInt("RespownPosition", 0) == number)
        {
            transform.position = player.position;
        }
    }
}
