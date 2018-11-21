using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDragon : MonoBehaviour {
    [SerializeField]
    private GameObject fireBall;
    [SerializeField]
    private GameObject muzzle;
    private GameObject playerPosition;
	// Use this for initialization
	void Start () {
        playerPosition = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Shot();
        }
	}
    void Shot()
    {
        Vector3 v = playerPosition.transform.position - muzzle.transform.position; 

        GameObject g =Instantiate(fireBall, muzzle.transform.position, Quaternion.identity);

        g.GetComponent<TestFireBall>().Shooting(v.normalized);
    }
}
