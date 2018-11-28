using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magic : MonoBehaviour {
    [SerializeField]
    private float rotateSpeed;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        Motion();
    }
    void Motion()
    {
        this.transform.Rotate(0, rotateSpeed, 0);
    }
}
