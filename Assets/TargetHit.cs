using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetHit : MonoBehaviour {

    public GameObject particles;

    public AudioClip crashSoft;

    

    //public Score score;

    private AudioSource source;
    private float lowPitchRange = .75F;
    private float highPitchRange = 1.5F;
    private float velToVol = .2F;

    

    public string collisionObjectName;

    private GameManager gameManager;
    // Use this for initialization
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void Awake()
    {
        
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == collisionObjectName)
        {
            Destroy(collision.gameObject);
            GameObject g = Instantiate(particles, collision.contacts[0].point, Quaternion.identity);
            Destroy(g, 4);
            gameManager.IncrementScore(1);
            //score.addPoint();

            source.pitch = Random.Range(lowPitchRange, highPitchRange);
            float hitVol = collision.relativeVelocity.magnitude * velToVol;
            Debug.Log("Touché");
            source.PlayOneShot(crashSoft, hitVol);
        }

    }
}
