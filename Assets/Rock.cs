using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour {

    public GameObject particles;

    public string collisionObjectName;
    // Use this for initialization
    void Start () {
        //Destroy(gameObject, 15);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == collisionObjectName)
        {
            Destroy(collision.gameObject);
            GameObject g = Instantiate(particles, collision.contacts[0].point, Quaternion.identity);
            Destroy(g, 4);
        }
            
    }
}
