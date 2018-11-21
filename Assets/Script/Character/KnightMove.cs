using System;
using UnityEngine;
using UnityEngine.UI;

public class KnightMove : CharacterMove
{
    private Animator anim;// キャラにアタッチされるアニメーターへの参照
    private AnimatorStateInfo currentBaseState;// base layerで使われる、アニメーターの現在の状態の参照
    private Vector3 vel;
    //MaxHP
    [SerializeField]
    private float maxHp = 100;
    //sliderObject
    [SerializeField]
    private Slider hpBar;//HPBarの参照
    public static bool isDamage;//ダメージを受けているか
    [SerializeField]
    private GameObject attackCol;//攻撃範囲
    [SerializeField]
    private GameObject deathEffect;//死亡時エフェクト
    [SerializeField]
    private float animSpeed = 1.5f;// アニメーション再生速度設定
    public static bool death_flag;
    private bool wasDamege;//多重ダメージをなくすため
    private float frezeTime=3.0f;
    //攻撃力
    [SerializeField]
    protected float attackPower = 10;
    // アニメーター各ステートへの参照
    static int idleState = Animator.StringToHash("Base Layer.Idle");
    static int runState = Animator.StringToHash("Base Layer.Run");
    static int attackState = Animator.StringToHash("Base Layer.Attack");

    // //初期化
    public override void Start()
   {
        base.Start();
        // Animatorコンポーネントを取得する
        anim = GetComponent<Animator>();
        hpBar.GetComponent<Slider>();
        hpBar.value = maxHp;
        death_flag = false;
        wasDamege = false;
    }
    void Update()
    {
        Control();
        Damage();
        Death();
    }

    private void Control()
    {
        float h = Input.GetAxis("Horizontal");              // 入力デバイスの水平軸をhで定義
        float v = Input.GetAxis("Vertical");                // 入力デバイスの垂直軸をvで定義

        vel = new Vector3(h, 0, v);
        anim.speed = animSpeed;// Animatorのモーション再生速度に animSpeedを設定する
        currentBaseState = anim.GetCurrentAnimatorStateInfo(0);

        if (vel.magnitude > 0.01)
        {
            anim.SetBool(AnimationState.BOOL_MOVE, true);
        }
        else
        {
            anim.SetBool(AnimationState.BOOL_MOVE, false);
        }
        // 参照用のステート変数にBase Layer (0)の現在のステートを設定する
        // 以下、Animatorの各ステート中での処理
        // run中
        // 現在のベースレイヤーがrunStateの時
        if (currentBaseState.fullPathHash == runState)
        {
        }
        // Attack中
        // 現在のベースレイヤーがattackStateの時
        else if (currentBaseState.fullPathHash == attackState)
        {
            anim.SetBool(AnimationState.BOOL_ATTACK, false);//無限に攻撃しないため
            attackCol.SetActive(false);
        }
        // IDLE中の処理
        // 現在のベースレイヤーがidleStateの時
        else if (currentBaseState.fullPathHash == idleState)
        {
        }
        //----------------攻撃----------------------------
        if (Input.GetButtonDown("Attack") || Input.GetKeyDown(KeyCode.Z))
        {
            anim.SetBool(AnimationState.BOOL_ATTACK, true);// Animatorに攻撃に切り替えるフラグを送る
            attackCol.SetActive(true);
        }
        //------------------------------------------------
    }

    public void FixedUpdate()
    {
        base.Move(vel);
    }
    /// <summary>
    /// ダメージ処理
    /// </summary>
    public void Damage()
    {
        if (isDamage)
        {
            if (wasDamege==false)
            {
                maxHp -= attackPower;
                hpBar.value = maxHp;
                isDamage = false;
                wasDamege = true;
            }
            else if(wasDamege)
            {
                frezeTime -= 1.0f * Time.deltaTime;
                if(frezeTime<=0)
                {
                    isDamage = false;
                    wasDamege = false;
                    frezeTime = 3.0f;
                }
            }
        }
    }
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "EnemyAttack_Col")
        {
            isDamage = true;
        }
    }

    void Death()
    {
        if(maxHp<=0)
        {
            death_flag = true;
            Instantiate(deathEffect, transform.position + new Vector3(0, 1, 0), Quaternion.identity);
            GetComponent<KnightMove>().enabled = false;
        }
    }
}
