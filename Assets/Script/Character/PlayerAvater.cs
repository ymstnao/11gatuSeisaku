using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Animator))]
public class PlayerAvater : MonoBehaviour
{
    [SerializeField]
    private GameObject attackCol;//攻撃範囲
//アニメーション関連--------------------------------------------------
    [SerializeField]
    private float animSpeed = 1.5f;// アニメーション再生速度設定
    private Animator anim;// キャラにアタッチされるアニメーターへの参照
    private AnimatorStateInfo currentBaseState;// base layerで使われる、アニメーターの現在の状態の参照
    // アニメーター各ステートへの参照
    static int idleState = Animator.StringToHash("Base Layer.Idle");
    static int runState = Animator.StringToHash("Base Layer.Run");
    static int attackState = Animator.StringToHash("Base Layer.Attack");
    static int deathState = Animator.StringToHash("Base Layer.Death");
//---------------------------------------------------------------------
    // //初期化
    void Start()
    {
        // Animatorコンポーネントを取得する
        anim = GetComponent<Animator>();
        anim.SetBool(AnimationState.BOOL_DEATH, false);
    }
    void Update()
    {
        AnimUpdate();
    }
    void AnimUpdate()
    {
        anim.speed = animSpeed;// Animatorのモーション再生速度に animSpeedを設定する
        currentBaseState = anim.GetCurrentAnimatorStateInfo(0);// 参照用のステート変数にBase Layer (0)の現在のステートを設定する
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
        else if (currentBaseState.fullPathHash == deathState)
        {
        }
        //----------------攻撃----------------------------
        if (Input.GetButtonDown("Attack") || Input.GetKeyDown(KeyCode.Z))
        {
            anim.SetBool(AnimationState.BOOL_ATTACK, true);// Animatorに攻撃に切り替えるフラグを送る
            attackCol.SetActive(true);
        }
        //------------------------------------------------
        if (PlayerFlagManager.run_flag)
        {
            anim.SetBool(AnimationState.BOOL_MOVE, true);
        }
        if (PlayerFlagManager.run_flag == false)
        {
            anim.SetBool(AnimationState.BOOL_MOVE, false);
        }
        if (PlayerFlagManager.death_flag)
        {
            anim.SetBool(AnimationState.BOOL_DEATH, true);
        }
    }
}
