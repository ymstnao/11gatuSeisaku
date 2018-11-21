using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public class Skeleton : CharacterMove
{
    //MaxHP
    [SerializeField]
    private float maxHp = 100;
    //HPバー
    [SerializeField]
    private Slider hpBar;
    //攻撃間隔調整
    [SerializeField]
    private float freezTime = 3.0f;
    // アニメーション再生速度設定
    [SerializeField]
    private float animSpeed = 1.5f;
    //敵追従範囲
    [SerializeField]
    private GameObject searchRange;
    private EnemySearchRange search;
    //攻撃モーションになる範囲
    [SerializeField]
    private GameObject attackRange;
    private EnemyAttackRange attackR;
    //攻撃範囲
    [SerializeField]
    private GameObject attackCol;
    //攻撃力
    public static float attackPower = 10;//考えるべき
    //フラグ-------------------------------
    private bool isDamage;//ダメージフラグ
    private bool freez;//攻撃間隔調整
    public bool deathflag;
    //------------------------------------
    //プレイヤーの場所
    private Transform playerPosition;
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
        playerPosition = GameObject.FindGameObjectWithTag("Player").transform;
        // Animatorコンポーネントを取得する
        anim = GetComponent<Animator>();

        search = searchRange.GetComponent<EnemySearchRange>();
        attackR = attackRange.GetComponent<EnemyAttackRange>();
        //HPバー---------------------
        hpBar.GetComponent<Slider>();
        hpBar.value = maxHp;
        //---------------------------
        freez = false;
        deathflag = false;

        attackRange.SetActive(true);
        searchRange.SetActive(true);

        anim.SetBool(AnimationState.BOOL_DEATH, false);
    }

    // Update is called once per frame
    void Update()
    {
        if (deathflag == false)
        {
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
            searchRange.SetActive(true);
        }
        // Attack中
        // 現在のベースレイヤーがattackStateの時
        else if (currentBaseState.fullPathHash == attackState)
        {
            searchRange.SetActive(false);
            search.searchflag = false;
        }
        // IDLE中の処理
        // 現在のベースレイヤーがidleStateの時
        else if (currentBaseState.fullPathHash == idleState)
        {
            searchRange.SetActive(true);
        }
        else if (currentBaseState.fullPathHash == deathState)
        {
        }
        //----------------攻撃----------------------------
        if (attackR.attackflag)
        {
            StartCoroutine("Attack");
        }
        else if (attackR.attackflag == false)
        {
            anim.SetBool(AnimationState.BOOL_ATTACK, false);//無限に攻撃しないため
            attackCol.SetActive(false);
        }
        //------------------------------------------------
    }

    public void FixedUpdate()
    {
        if (deathflag == false)
        {
            if (search.searchflag)
            {
                Vector3 v = playerPosition.position - transform.position;
                transform.rotation = Quaternion.LookRotation(v);
                base.Move(v.normalized);
            }
        }
    }
    public void Damage()
    {
        if (isDamage)
        {
            isDamage = false;
            maxHp -= PlayerStatus.attackPower;
            hpBar.value = maxHp;
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
            deathflag = true;
            anim.SetBool(AnimationState.BOOL_DEATH, true);
        }
    }

    bool attackCoroutine = false;
    private IEnumerator Attack()
    {
        if (attackCoroutine)
        {
            yield break;
        }

        attackCoroutine = true;
        anim.SetBool(AnimationState.BOOL_ATTACK, true);

        yield return new WaitForSeconds(0.3f);
        var p = Physics.OverlapSphere(transform.position, 2f);

        for (var i = 0; i < p.Length; i++)
        {
            var playerStatus = p[i].GetComponent<PlayerStatus>();
            if (playerStatus != null)
            {
                playerStatus.Damage(10f);
            }
        }
        search.searchflag = false;
        yield return new WaitForSeconds(1.3f);
        attackCoroutine = false;
        yield break;
    }
}
