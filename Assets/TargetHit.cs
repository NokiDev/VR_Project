using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetHit : MonoBehaviour {

    public GameObject particles;

    public AudioClip crashSoft;

    private AudioSource source;
    private float lowPitchRange = .75F;
    private float highPitchRange = 1.5F;
    private float velToVol = .2F;

    

    public string collisionObjectName;
    // Use this for initialization
    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == collisionObjectName)
        {
            Destroy(collision.gameObject);
            GameObject g = Instantiate(particles, collision.contacts[0].point, Quaternion.identity);
            Destroy(g, 4);

            //score.addPoint();

            source.pitch = Random.Range(lowPitchRange, highPitchRange);
            float hitVol = collision.relativeVelocity.magnitude * velToVol;
            Debug.Log("Touché");
            source.PlayOneShot(crashSoft, hitVol);
        }

        if (collision.gameObject.name == "Terrain")
        {
            Vector3 pos = gameObject.transform.position;
            GameObject mainCamera = GameObject.FindGameObjectsWithTag("MainCamera")[0];
            float max;
            if (mainCamera.GetComponents<Spawn>()[0].nameTag == "Rock")
            {
                max = mainCamera.GetComponents<Spawn>()[0].maxDistance;
            } else
            {
                max = mainCamera.GetComponents<Spawn>()[1].maxDistance;
            }
            Debug.Log("dist" + max);
            if (pos.x > max || pos.z > max || pos.x < 0 - max || pos.z < 0 - max)
                Destroy(gameObject);
        }
    }
}
