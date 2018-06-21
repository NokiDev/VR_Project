using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puller : MonoBehaviour {

    public Vector3 maxPullForce; //Force on origin. Decrease over distance.
    public float maxPullDistance = 10f; // Max distance in which the objects can be pulled.

    // Use this for initialization

    private bool started = true;
    private List<Rigidbody> pulledObjects = new List<Rigidbody>();
    private List<Rigidbody> catchedObjects = new List<Rigidbody>();

    public float catchDistance = 0.5f;


	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

    }

    private void FixedUpdate()
    {
        if (started)
        {
            List<Rigidbody> tmp = new List<Rigidbody>();
            foreach(var pulledObject in pulledObjects)
            {
                Vector3 direction = (transform.position - pulledObject.transform.position);
                float distance = direction.magnitude;
                if(distance < catchDistance)
                {
                    pulledObject.constraints = RigidbodyConstraints.FreezePosition;
                    catchedObjects.Add(pulledObject);
                }
                else
                {
                    tmp.Add(pulledObject);
                    Vector3 appliedForce = Vector3.Lerp(maxPullForce, Vector3.zero, distance/maxPullDistance); // Keep objects at close range.
                    pulledObject.AddForceAtPosition(appliedForce, transform.position);
                }
            }
            pulledObjects = tmp;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        pulledObjects.Add(other.GetComponent<Rigidbody>());
    }

    public void Pull()
    {
        started = true;
        //Pull effect code.
    }

    public void Stop()
    {
        started = false;
        pulledObjects.Clear();
        foreach(var catchedObject in catchedObjects)
        {
            catchedObject.constraints = RigidbodyConstraints.None;
        }
        //Stop pull effect nicely.
        //Stop animation.
    }


}
