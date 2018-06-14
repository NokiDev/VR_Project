using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullerPusher : MonoBehaviour {

    public Vector3 pullforce = Vector3.zero;
    public Vector3 pushforce = Vector3.zero;
    public float maxPullDistance = 10f;


    List<GameObject> currentObjects = new List<GameObject>();

    private Rigidbody rb;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		if(OVRInput.GetDown(OVRInput.Touch.PrimaryIndexTrigger))//Touch L or R 
        {//Pull objects.
           
            //Trace a ray that come from the center of this object, and go forward from it.
            RaycastHit[] hits = Physics.RaycastAll(transform.position, Vector3.forward, maxPullDistance);
            foreach(var hit in hits)
            {
                //Add force that pull the item to the hand of the player.
                hit.collider.GetComponent<Rigidbody>().AddForce(pullforce); // FIXME control force overtime.
            }
        }

        if(OVRInput.GetUp(OVRInput.Touch.PrimaryIndexTrigger))
        {
            //Release and apply inverse force.
            foreach(var obj in currentObjects)
            {
                obj.GetComponent<Rigidbody>().AddForce(-pushforce);
            }

            //Clear lists
            currentObjects.Clear();
        }
	}


    private void OnTriggerEnter(Collider other)
    {
        //if(other.GetComponent<Pullable>())
        //pull, on object
        //store objects.//Check the type.
        currentObjects.Add(other.gameObject);
    }


    private void OnTriggerExit(Collider other)
    {
        // remove object from list.
        currentObjects.Remove(other.gameObject);
    }
}
