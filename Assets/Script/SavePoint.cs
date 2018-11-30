using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePoint : MonoBehaviour {
    [SerializeField,Header("浮遊する高さ")]
    private float floatingY;
    [SerializeField, Header("浮遊する速さ")]
    private float floatingSpeed;
    private int id;
    private Vector3 defaultPosition;
    // Use this for initialization
    void Start() {
        defaultPosition = transform.position;
    }

    // Update is called once per frame
    void Update() {
        Rotation();
        Floating();
    }
    void Rotation()
    {
        transform.Rotate(1, 1, 0);
    }

    /// <summary>
    /// オブジェクトをふよふよさせる
    /// </summary>
    void Floating() {
        var position = defaultPosition;
        
        position.y += Mathf.Sin(Time.timeSinceLevelLoad * floatingSpeed) * floatingY;

        transform.position = position;
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
