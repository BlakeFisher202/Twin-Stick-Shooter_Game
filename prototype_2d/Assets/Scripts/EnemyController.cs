using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{


    [SerializeField, Range(0,20)]
    float speed;

    [SerializeField, Range(0,20)]
    float rotateSpeed;

    GameObject player;

    Health healthController;
    

    // Start is called before the first frame update
    void Start()
    {
        healthController = GetComponent<Health>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        //Rotates enemy
        //NOTE: TEMPORARY UNTIL UNITY AI IS IMPLEMENTED
        RotateEnemy();

        //Moves enemy
        Vector3 displacement = transform.forward * Time.deltaTime * speed;
        transform.position += displacement;
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            //Reduces the enemy's health by the damage amount set in bullet script
            Bullet bullet = collision.gameObject.GetComponent<Bullet>();
            healthController.health -= bullet.damage;

            //Sets percentage of health bar to "fill"
            healthController.healthBarPerc = healthController.getHealthBarPerc();

            //Destroys enemy
            Destroy(collision.gameObject);
        }
    }

    void RotateEnemy()
    {
        //gets the difference between player and enemy
        Vector3 diff = player.transform.position - transform.position;

        //Sets rotation so it in the direction of diff, then isolates the y value
        Quaternion rotation = Quaternion.LookRotation(diff, Vector3.up);
        rotation = Quaternion.Euler(0f, rotation.eulerAngles.y, 0f);

        //Lerps between the current rotation and the rotation the enemy should be looking towards
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * rotateSpeed);
    }
}
