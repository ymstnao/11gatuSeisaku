using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Animator))]
public class Dragon : CharacterMove
{
    [SerializeField]
    private float maxHp = 100;//MaxHP
    [SerializeField]
    private Slider hpBar;//sliderObject
    [SerializeField]
    private float freezTime = 3.0f;//攻撃間隔調整
    [SerializeField]
    private GameObject searchRange;//敵追従範囲
    private EnemySearchRange search;
    [SerializeField]
    private GameObject attackRange;//攻撃の範囲
    private EnemyAttackRange attackR;
    [SerializeField]
    private GameObject attackCol;
    [SerializeField]
    private float animSpeed = 1.5f;// アニメーション再生速度設定

    public static float attackPower = 10;//攻撃力
    //フラグ-------------------------------
    private bool isDamage;//ダメージフラグ
    private bool freez;//攻撃間隔調整
    public static bool deathFlag;//死んだか
    private bool death;
    //------------------------------------
    private Transform player;//プレイヤーの場所
    //アニメーション用--------------------------------------------------
    private Animator anim;// キャラにアタッチされるアニメーターへの参照
    private AnimatorStateInfo currentBaseState;// base layerで使われる、アニメーターの現在の状態の参照
    // アニメーター各ステートへの参照
    static int idleState = Animator.StringToHash("Base Layer.Idle");
    static int runState = Animator.StringToHash("Base Layer.Run");
    static int attackState = Animator.StringToHash("Base Layer.Attack");
    static int deathState = Animator.StringToHash("Base Layer.Death");
    //------------------------------------------------------------------
    // Use this for initialization
    public override void Start()
    {
        base.Start();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        // Animatorコンポーネントを取得する
        anim = GetComponent<Animator>();

        search = searchRange.GetComponent<EnemySearchRange>();
        attackR = attackRange.GetComponent<EnemyAttackRange>();
        //HPバー---------------------
        hpBar.GetComponent<Slider>();
        hpBar.value = maxHp;
        //---------------------------
        freez = false;
        deathFlag = false;
        death = false;
        searchRange.SetActive(true);//攻撃中動かないように
    }

    // Update is called once per frame
    void Update()
    {
        if (death == false)
        {
            this.transform.LookAt(player);
            Damage();
            AnimationController();
            Death();
        }
    }
    private void AnimationController()
    {
        anim.speed = animSpeed;// Animatorのモーション再生速度に animSpeedを設定する
        currentBaseState = anim.GetCurrentAnimatorStateInfo(0);

        if (search.searchflag)
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
        }
        // IDLE中の処理
        // 現在のベースレイヤーがidleStateの時
        else if (currentBaseState.fullPathHash == idleState)
        {
        }
        //----------------攻撃----------------------------
        if (freez)
        {
            anim.SetBool(AnimationState.BOOL_ATTACK, false);
            searchRange.SetActive(false);//攻撃中動かないように
            search.searchflag = false;
            attackCol.SetActive(false);
            freezTime -= 1 * Time.deltaTime;
            if (freezTime <= 0)
            {
                freez = false;
                freezTime = 3.0f;
            }
        }
        else if (attackR.attackflag)
        {
            anim.SetBool(AnimationState.BOOL_ATTACK, true);
            searchRange.SetActive(false);//攻撃中動かないように
            search.searchflag = false;
            attackCol.SetActive(true);
            freezTime -= 2.5f * Time.deltaTime;
            if (freezTime <= 0)
            {
                freez = true;
                freezTime = 3.0f;
            }
        }
        else if (attackR.attackflag == false)
        {
            attackCol.SetActive(false);
            anim.SetBool(AnimationState.BOOL_ATTACK, false);//無限に攻撃しないため
            searchRange.SetActive(true);//攻撃終了時動けるように
        }
        //------------------------------------------------
    }
    public void FixedUpdate()
    {
        if (death == false)
        {
            if (search.searchflag)
            {
                Vector3 v = player.position - transform.position;

                transform.rotation = Quaternion.LookRotation(v);
                base.Move(v.normalized);
            }
        }
    }
    public void Damage()
    {
        if (isDamage)
        {
            maxHp -= PlayerStatus.attackPower;
            hpBar.value = maxHp;
            isDamage = false;
        }
    }
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "PlayerAttack_Col")
        {
            isDamage = true;
        }
    }
    void Death()
    {
        if (maxHp <= 0)
        {
            deathFlag = true;
            death = true;
            anim.SetBool(AnimationState.BOOL_DEATH, true);
        }
    }
}
