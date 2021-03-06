﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puller : MonoBehaviour {

    public Vector3 maxPullForce; //Force on origin. Decrease over distance.
    public float maxPullDistance = 10f; // Max distance in which the objects can be pulled.

    // Use this for initialization

    private bool started = false;
    private List<Rigidbody> pulledObjects = new List<Rigidbody>();
    private List<Rigidbody> catchedObjects = new List<Rigidbody>();

    public float catchDistance = 0.5f;
    public float energyCostPerSeconds = 2f;
    public float energyReloadedPerSecond = 1f;

    public HandsData data;
    private bool pulling;

    Animator animator;

    Coroutine consumeEnergy;
    Coroutine reloadEnergy;

    void Start() {
        data.outOfEnergy += Stop;
        animator = GetComponent<Animator>();
    }

    private void OnDestroy()
    {
        data.outOfEnergy -= Stop;
    }

    // Update is called once per frame
    void Update() {

    }

    private void FixedUpdate()
    {
        if (started)
        {
            if(pulling)
            {
                List<Rigidbody> tmp = new List<Rigidbody>();
                foreach (var pulledObject in pulledObjects)
                {
                    Vector3 direction = (transform.position - pulledObject.transform.position);
                    float distance = direction.magnitude;
                    if (distance < catchDistance)
                    {
                        pulledObject.isKinematic = true;
                        pulledObject.transform.SetParent(transform.parent);
                        catchedObjects.Add(pulledObject);
                    }
                    else
                    {
                        tmp.Add(pulledObject);
                        Vector3 appliedForce = Vector3.Lerp(maxPullForce, Vector3.zero, distance / maxPullDistance); // Keep objects at close range.
                        Debug.Log("applied force : " + appliedForce);
                        Debug.Log("Direction : " + direction);
                        Debug.Log("Distance " + distance);
                        appliedForce.Scale(direction);
                        pulledObject.AddForceAtPosition(appliedForce, transform.position);
                    }
                }
                pulledObjects = tmp;
            }
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (started && other.tag == "Rock") ;
        {
            var rb = other.GetComponent<Rigidbody>();
            rb.useGravity = false;
            pulledObjects.Add(rb);
        }
    }

    public void OnTriggerStay(Collider other)
    {
        var rb = other.GetComponent<Rigidbody>();
        if (!pulledObjects.Contains(rb))
        {
            pulledObjects.Add(rb);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        Debug.Log("Triggered");
        Debug.Log(other.name);
        var rb = other.GetComponent<Rigidbody>();
        if (pulledObjects.Contains(rb))
        {
            rb.useGravity = true;
            pulledObjects.Remove(rb);
        }
    }

    public void Pull()
    {
        if(data.CurrentEnergy > 0)
        {
            started = true;
            pulling = true;
            StopCoroutine("ReloadEnergy");
            reloadEnergy = null;
            if (consumeEnergy == null)
                consumeEnergy = StartCoroutine("ConsumeEnergy");
            //Start coroutine for energy consumtion.
            //Pull effect code.
            animator.SetBool("isPull", pulling);
        }
        else
        {
            StopPulling();
        }
        
    }

    public void Stop()
    {
        started = false;
        StopPulling();
        ReleaseObjects();
    }

    public void StopPulling()
    {
        started = false;
        pulling = false;
        animator.SetBool("isPull", false);
        StopCoroutine("ConsumeEnergy");
        consumeEnergy = null;
        //Stop coroutine for energy consumtion.
        //Stop pull effect nicely.
        if(reloadEnergy == null)
        {
            reloadEnergy = StartCoroutine("ReloadEnergy");
        }
        
        foreach (var rb in pulledObjects)
        {
            rb.useGravity = true;
        }
        pulledObjects.Clear();
    }

    public List<GameObject> GetCatchedObject() {
        return catchedObjects.ConvertAll<GameObject>((rigidbody) => { return rigidbody.gameObject; });
    }

    public void ClearCatchedObjects()
    {
        catchedObjects.Clear();
    }

    public void ReleaseObjects()
    {
        foreach (var catchedObject in catchedObjects)
        {
            catchedObject.transform.parent = null;
            catchedObject.isKinematic = false;
            catchedObject.useGravity = true;
        }
        foreach(var rb in pulledObjects)
        {
            rb.useGravity = true;
        }
        pulledObjects.Clear();
        catchedObjects.Clear();
    }

    IEnumerator ConsumeEnergy()
    {
        while(started)
        {
            data.UseEnergy(pulling ? energyCostPerSeconds : energyCostPerSeconds/2);
            yield return new WaitForSeconds(1f);
        }
    }

    IEnumerator ReloadEnergy()
    {
        Debug.Log("reload started");
        while (!started)
        {
            Debug.Log(data.CurrentEnergy);
            data.ReloadEnergy(energyReloadedPerSecond);
            yield return new WaitForSeconds(1f);
        }
    }


}