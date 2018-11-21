using UnityEngine;

public class EnemySearchRange : MonoBehaviour {
    public bool searchflag;
    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }
    void OnTriggerStay(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            searchflag = true;
        }
    }
    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            searchflag = false;
        }
    }
}
