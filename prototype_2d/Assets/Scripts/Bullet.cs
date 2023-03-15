using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField, Range (0, 100)]
    float maxDistance;

    [SerializeField, Range (0, 100)]
    float speed;

    [SerializeField, Range(0, 10)]
    public float damage;

    Vector3 startPos;


    // Start is called before the first frame update
    void Awake()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //Moves bullet
        Vector3 displacement = transform.up * Time.deltaTime * speed;
        transform.position += displacement;

        //Deletes bullet after travelling a certain distance
        if(Vector3.Distance(transform.position, startPos) > maxDistance)
        {
            Destroy(gameObject);
        }
    }
}
