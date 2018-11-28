using UnityEngine;
using UnityEngine.UI;
using System.Collections;

//必要なものをオブジェクトにつける
[RequireComponent(typeof(Rigidbody))]
public abstract class Enemys : MonoBehaviour {
    // 以下キャラクターコントローラ用パラメタ
    // 動く速度
    [SerializeField]
    protected float moveSpeed = 10.0f;
    // 旋回速度
    [SerializeField]
    protected float rotateSpeed = 2.0f;
    // アニメーション再生速度設定
    [SerializeField]
    protected float animSpeed = 1.5f;
    //攻撃アニメーション待機時間
    [SerializeField]
    protected float attackCheckWait;
    [SerializeField]
    protected float nextAttackWait;
    protected Rigidbody rb;
    // キャラクターコントローラ（カプセルコライダ）の移動量
    protected Vector3 velocity;
    //HPバー
    [SerializeField]
    protected Slider hpBar;
    //敵追従範囲
    [SerializeField]
    protected GameObject searchRange;
    protected EnemySearchRange search;
    //攻撃モーションになる範囲
    [SerializeField]
    protected GameObject attackRange;
    protected EnemyAttackRange attackR;
    //フラグ
    protected bool deathflag;//死んだか
    //プレイヤーの場所
    protected Transform playerPosition;
    //死んだときに生成するオブジェクト
    [SerializeField]
    protected GameObject soul;
    //アニメーション用
    protected Animator anim;// キャラにアタッチされるアニメーターへの参照
    protected AnimatorStateInfo currentBaseState;// base layerで使われる、アニメーターの現在の状態の参照
    // アニメーター各ステートへの参照
    protected static int idleState = Animator.StringToHash("Base Layer.Idle");
    protected static int runState = Animator.StringToHash("Base Layer.Run");
    protected static int attackState = Animator.StringToHash("Base Layer.Attack");
    protected static int deathState = Animator.StringToHash("Base Layer.Death");

    [SerializeField]
    protected EnemyParameter parameter;

    //初期化
    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody>();

        playerPosition = GameObject.FindGameObjectWithTag("Player").transform;
        // Animatorコンポーネントを取得する
        anim = GetComponent<Animator>();

        search = searchRange.GetComponent<EnemySearchRange>();
        attackR = attackRange.GetComponent<EnemyAttackRange>();

        deathflag = false;

        attackRange.SetActive(true);
        searchRange.SetActive(true);

        anim.SetBool(AnimationState.BOOL_DEATH, false);
        //HPバー---------------------
        parameter.NowHp = parameter.MaxHp;
        hpBar.GetComponent<Slider>();
        hpBar.maxValue = parameter.MaxHp;
        hpBar.value = parameter.NowHp;
        //---------------------------

    }
    protected virtual void Update()
    {
        if (deathflag == false)
        {
            AnimationController();
            Death();
        }
    }
    protected virtual void FixedUpdate()
    {
        if (deathflag == false)
        {
            if (search.searchflag)
            {
                Vector3 v = playerPosition.position - transform.position;
                transform.rotation = Quaternion.LookRotation(v);
                Move(v.normalized);
            }
        }
    }
    protected virtual void Move(Vector3 velocity)
    {
        velocity *= moveSpeed;// 移動速度を掛ける

        // 上下のキー入力でキャラクターを移動させる
        transform.Translate(velocity * Time.fixedDeltaTime, Space.World);
    }
    public virtual void Damage(float damage)
    {
        parameter.NowHp -= damage;
        hpBar.value = parameter.NowHp;
    }
    protected  virtual void Death()
    {
        if (parameter.NowHp <= 0)
        {
            deathflag = true;
            anim.SetBool(AnimationState.BOOL_DEATH, true);
        }
    }

    protected virtual void AnimationController()
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
        }
        //------------------------------------------------
    }
    bool attackCoroutine = false;
    protected virtual IEnumerator Attack()
    {
        if (attackCoroutine)
        {
            yield break;
        }

        attackCoroutine = true;
        anim.SetBool(AnimationState.BOOL_ATTACK, true);

        yield return new WaitForSeconds(attackCheckWait);
        var p = Physics.OverlapSphere(transform.position, 2f);

        for (var i = 0; i < p.Length; i++)
        {
            var playerStatus = p[i].GetComponent<PlayerStatus>();
            if (playerStatus != null)
            {
                playerStatus.Damage(parameter.AttackPower);
            }
        }
        search.searchflag = false;
        yield return new WaitForSeconds(nextAttackWait);
        attackCoroutine = false;
        yield break;
    }

    protected virtual void OnTriggerEnter(Collider col)
    {
        if (deathflag)
        {
            if (col.gameObject.tag == "Absorption_Col")
            {
                var s = Instantiate(soul, this.transform.position + new Vector3(0, 2, 0), Quaternion.identity);
                s.GetComponent<EnemySoul>().Parameter = parameter;
                Destroy(this.gameObject);
            }
        }
    }
}
[System.Serializable]
public struct EnemyParameter
{
    [SerializeField]
    private float maxHp;
    [SerializeField]
    private float attackPower;
    //HP
    private float nowHp;
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
}
