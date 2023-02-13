using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public GameObject enemy;
    public GameObject player;
    public GameObject[] Spawners;

    public int spawnPoint;
    // Start is called before the first frame update
    void Start()
    {
        spawnPoint = Random.Range(0, Spawners.Length);
    }

    // Update is called once per frame
    void Update()
    {
        //If there is no enemy in the scene it spawns an enemy
        if (GameObject.FindGameObjectWithTag("Enemy") == null)
        {
            spawnPoint = Random.Range(0, Spawners.Length);
            spawnObjectRandom(enemy);
        } 
    }

    public void spawnObjectRandom(GameObject obj) {
        //Sets randomPos
        Vector3 randomPos = Spawners[spawnPoint].gameObject.transform.position;

        //Prevents spawns too close to the center
        while (Vector3.Distance(Vector3.zero, randomPos) < 3)
        {
            //Resets randomPos
            randomPos = new Vector3(Random.Range(-8, 8), 1f, Random.Range(-4, 4));
        }

        //Instantiates enemy
        Instantiate(obj, randomPos, Quaternion.identity);
    }
}
