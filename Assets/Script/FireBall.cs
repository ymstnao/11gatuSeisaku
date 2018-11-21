using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour {
    private GameObject player;//プレイヤーの座標をとるため
    private Rigidbody rigid;//弾のrigidbody 
    [SerializeField]
    private float fireBallSpeed = 100;//弾の速度

    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        rigid = this.gameObject.GetComponent<Rigidbody>();
        Move();
    }
	
	
    void Move()
    {
        rigid.AddForce(transform.forward * fireBallSpeed*Time.deltaTime);
    }
}
