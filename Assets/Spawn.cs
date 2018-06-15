using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour {

    public List<GameObject> objets;
    public float time = 0.01f;
    [Range(0, 360)]
    public int angle = 180;
    public float minDistance = 10f, maxDistance = 20f, minHauteur = 0f, maxHauteur = 10f;

	// Use this for initialization
	void Start () {
        StartCoroutine(PlaceCubes());
        //PlaceCubes();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    Vector3 GeneratedPosition()
    {
        float x, y, z;
        float angleRadian = (float) angle / 180 * Mathf.PI;
        float randAngle = Random.Range(Mathf.PI/2 - angleRadian/2, Mathf.PI/2 + angleRadian/2);
        float randDistance = Random.Range(minDistance, maxDistance);
        x = (float) Mathf.Cos(randAngle)*randDistance;
        y = Random.Range(minHauteur, maxHauteur);
        z = (float) Mathf.Sin(randAngle)*randDistance;
        return new Vector3(x, y, z);
    }

    IEnumerator PlaceCubes()
    {
        while (true) {
            int randObjet = Random.Range(0, objets.Count);
            Instantiate(objets[randObjet], GeneratedPosition(), Quaternion.identity);
            yield return new WaitForSeconds(time);
        }
    }
}
