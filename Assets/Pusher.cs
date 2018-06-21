﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pusher : MonoBehaviour {

    //public Vector3 originForce;
    public float power;

    public GameObject[] testObjects;

    public bool testMode;

	// Use this for initialization
	void Start () {
        //Only for test
        if (testMode)
        {
            Push(testObjects);
        }
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void Push(GameObject[] objects)
    {
        foreach (GameObject obj in objects)
        {
            Rigidbody rb = obj.GetComponent<Rigidbody>(); //Get rigidbody component of object
            rb.isKinematic = false; //disable kinematic mode to Add force
            obj.transform.SetParent(null); // remove parent of object
            rb.AddForce(obj.transform.forward * power);
            rb.AddTorque(obj.transform.right * Random.Range(power, power * 1.5f)); //to simumulate rotation during expulse
        }
    }
}
