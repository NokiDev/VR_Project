﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pusher : MonoBehaviour {

    //public Vector3 originForce;
    public float power;

    public List<GameObject> testObjects;

    public bool testMode;

    Animator animator;


	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
        //Only for test
        if (testMode)
        {
            Push(testObjects);
        }
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Push(List<GameObject> objects)
    {
        animator.SetTrigger("Push");
        Debug.Log(objects);
        Debug.Log(objects.Count);
        foreach (GameObject obj in objects)
        {
            obj.tag = "Untagged";
            Rigidbody rb = obj.GetComponent<Rigidbody>(); //Get rigidbody component of object
            rb.isKinematic = false; //disable kinematic mode to Add force
            rb.useGravity = true;
            obj.transform.SetParent(null); // remove parent of object
            Debug.Log("applied push force + " + power);
            rb.AddForce(transform.right * power);//FIXME use transform.forward
            rb.AddTorque(obj.transform.forward * Random.Range(power, power * 1.5f)); //to simumulate rotation during expulse
            ResetTagObject(obj);
        }

    }

    IEnumerator ResetTagObject(GameObject obj)
    {

        yield return new WaitForSeconds(3f);
        obj.tag = "Rock";
    }
}
