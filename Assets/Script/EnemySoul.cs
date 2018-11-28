using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySoul:MonoBehaviour
{
    // 動く速度
    [SerializeField]
    protected float moveSpeed = 10.0f;
    private Transform playerPosition;
    private EnemyParameter parameter;
    protected Vector3 velocity;

    public EnemyParameter Parameter
    {
        get
        {
            return parameter;
        }

        set
        {
            parameter = value;
        }
    }

    // Use this for initialization
    void Start () {
        playerPosition = GameObject.FindGameObjectWithTag("Player").transform;
    }
	
	// Update is called once per frame
	void Update () {
    }
    void FixedUpdate()
    {
        Move();
    }
    void Move()
    {
        Vector3 v = playerPosition.position - transform.position;
        velocity *= moveSpeed;// 移動速度を掛ける
        transform.Translate(v * Time.fixedDeltaTime, Space.World);
    }
    void OnTriggerStay(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            col.gameObject.GetComponent<PlayerStatus>().Absorption(parameter);

            Destroy(this.gameObject);
        }
    }
}
