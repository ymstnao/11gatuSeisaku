using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePoint : MonoBehaviour {
    private int id;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Rotation();
    }
    void Rotation()
    {
        transform.Rotate(1, 1, 0);
    }
    public void Save(PlayerStatus status)
    {
        status.HpChange(status.MaxHp);

        PlayerPrefs.SetFloat("AttackPower", status.AttackPower);
        PlayerPrefs.SetFloat("HP", status.MaxHp);
        PlayerPrefs.Save();
    }
    void OnTriggerStay(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            if (Input.GetKeyDown(KeyCode.U))
            {
                var status = col.GetComponent<PlayerStatus>();
                Save(status);
            }
            if (Input.GetKeyDown(KeyCode.I))
            {
                Debug.Log(PlayerPrefs.GetFloat("HP", 0).ToString());
                Debug.Log(PlayerPrefs.GetFloat("AttackPower", 0).ToString());
            }
        }
    }
}
