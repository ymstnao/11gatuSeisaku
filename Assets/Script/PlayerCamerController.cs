using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamerController : MonoBehaviour {
    private GameObject player;//プレイヤーオブジェクト

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void FixedUpdate() {
        //プレイヤーにカメラを追従させる
        this.gameObject.transform.position = player.transform.position;
	}
}
