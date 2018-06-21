using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour {

    public string nameTag;
    public List<GameObject> objets;
    public bool isDispawnable;
    public float time = 0.01f;
    [Range(0, 360)]
    public int angle = 180;
    public int maxSpawn = 20;
    public float minDistance = 10f, maxDistance = 20f, minHauteur = 0f, maxHauteur = 10f;

	// Use this for initialization
	void Start () {
        StartCoroutine(spawnObject());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    protected Vector3 GeneratedPosition()
    {
        float x, y, z;
        float angleRadian = (float)angle / 180 * Mathf.PI;
        float randAngle = Random.Range(Mathf.PI / 2 - angleRadian / 2, Mathf.PI / 2 + angleRadian / 2);
        float randDistance = Random.Range(minDistance, maxDistance);

        x = (float)Mathf.Cos(randAngle) * randDistance;
        y = Random.Range(minHauteur, maxHauteur);
        z = (float)Mathf.Sin(randAngle) * randDistance;

        return new Vector3(x, y, z);
    }

    IEnumerator spawnObject()
    {
        while (true) {
            int randObjet = Random.Range(0, objets.Count);
            Vector3 positions = GeneratedPosition();
            Quaternion rotations = Quaternion.FromToRotation(new Vector3(0, 100, 0), positions);

            GameObject[] spawnedObjects = GameObject.FindGameObjectsWithTag(nameTag);
            //Debug.Log(nameTag + ": " + spawnedObject.Length);

            if (spawnedObjects.Length >= maxSpawn && isDispawnable)
            {
                Destroy(spawnedObjects[0]);
            }
            if (spawnedObjects.Length < maxSpawn || isDispawnable)
            {
                Instantiate(objets[randObjet], positions, rotations);
            }

            yield return new WaitForSeconds(time);
        }
    }
}
