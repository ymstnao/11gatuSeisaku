using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySoul : Enemys
{
    private Transform playerPosition;
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
        transform.rotation = Quaternion.LookRotation(v);
        base.Move(v);
    }
    void OnTriggerStay(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            Destroy(this.gameObject);
        }
    }
}
