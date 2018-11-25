using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFlagManager : MonoBehaviour {
    [SerializeField]
    private GameObject[] PlayerChangeAvater;//プレイヤーアバターリスト

    //フラグ----------------------------------------


    public static bool attack_flag;//攻撃しているか

    public static bool run_flag;//走っているか

    public static bool death_flag;//死んだか
    
    public static bool isPlayerStornMonster;//ストーンモンスターを使うか
  //-----------------------------------------------
    // Use this for initialization
    void Start () {
        death_flag = false;
        isPlayerStornMonster = true;//最初はストーンモンスター
    }
	
	// Update is called once per frame
	void Update () {
        PlayerChange();
    }
    void PlayerChange()
    {
        if (isPlayerStornMonster)
        {
            PlayerChangeAvater[0].SetActive(true);
        }
    }
}
