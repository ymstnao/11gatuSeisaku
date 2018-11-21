using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class TestFireBall : MonoBehaviour {
    private Rigidbody rig;
    [SerializeField]
    private float speed;
	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void Shooting(Vector3 vel)
    {
        rig = GetComponent<Rigidbody>();
        rig.AddForce(vel*speed, ForceMode.Impulse);
    }
    void OnTriggerEnter(Collider col)
    {
        Destroy(gameObject);
    }
}
