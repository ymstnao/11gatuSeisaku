using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 20.0f;
    [SerializeField]
    private float rotateSpeed = 20.0f;
    private PlayerStatus status;
    //移動の時に使うVector3
    private Vector3 vel;
    private Rigidbody rigid;
    private float h;// 入力デバイスの水平軸をhで定義
    private float v;// 入力デバイスの垂直軸をvで定義

    // Use this for initialization
    void Start ()
    {
        rigid = this.GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        h = Input.GetAxis("Horizontal");// 入力デバイスの水平軸をhで定義
        v = Input.GetAxis("Vertical");// 入力デバイスの垂直軸をvで定義
    }
    /// <summary>
     /// 移動処理
     /// </summary>
    private void Move()
    {
        vel = new Vector3(0, 0, v);//上下で移動左右で移動方向の時
        var quaternion = Quaternion.AngleAxis((h * rotateSpeed * Time.fixedDeltaTime + transform.rotation.eulerAngles.y) % 360, Vector3.up);
        rigid.MoveRotation(quaternion);
        rigid.MovePosition(transform.position + transform.forward * v * moveSpeed * Time.fixedDeltaTime);
        if (vel.magnitude > 0.01)
        {
            PlayerFlagManager.run_flag = true;
        }
        else
        {
            PlayerFlagManager.run_flag = false;
        }
    }
    public void FixedUpdate()
    {
        if (PlayerFlagManager.death_flag == false)
        {
            Move();
        }
    }
    void OnTriggerStay(Collider col)
    {
        if (col.gameObject.tag == "DeathArea")
        {
            PlayerFlagManager.death_flag = true;
        }
    }
}
