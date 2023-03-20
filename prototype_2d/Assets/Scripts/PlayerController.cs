using System.Collections;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{
    [SerializeField, Range(0, 100)]
    float maxAcceleration;

    [SerializeField, Range(0, 100)]
    float maxSpeed;

    //NOTE: TEMPORARY UNTIL ENEMIES HAVE SET ATTACKS
    [SerializeField, Range(0, 5)]
    float damage;

    Vector3 velocity;

    public GameObject bullet;
    GameObject spawnPoint;
    Health healthController;

    //cube so we can tell where mouse is hitting
    public GameObject testObject;

    // Telemetry Data Input Struct
    [Serializable]
    struct TelemetryData
    {
        public Vector2 playerPos;
        public string objectName;
        public float bulletsFired;
    }


    // Start is called before the first frame update
    void Start()
    {
        //Gets health script on player
        healthController = GetComponent<Health>();
        //Finds spawnPoint gameobject
        spawnPoint = GameObject.Find("SpawnPoint");

        //Start Telemetry Coroutine
        StartCoroutine("Telemetry");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Rotates player
        transform.rotation = Quaternion.Euler(0, -MouseRotation(), 0);


        //Moves player
        transform.position += MovePlayer();


    }

    private void Update()
    {
        //Spawns bullet on mouse click (not held down)
        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(bullet, spawnPoint.transform.position, Quaternion.Euler(90f, -MouseRotation(), 0f));

            var data = new TelemetryData()
            {
                bulletsFired =+ 1
            };

            TelemetryLogger.Log(this, "BulletFired", data);
        }

    }

    //Gets angle between player and mouse
    float MouseRotation()
    {
        Vector3 mousePosInWorld = Vector3.zero;

        //Gets a raycast from the camera to the world
        //If it hits an object, it sets mousePosInWorld to where the ray hit
        //NOTE: if mouse cursor goes into the void this won't work and mousePosInWorld will stay at Vector3.zero
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            mousePosInWorld = ray.GetPoint(hit.distance);
        }

        testObject.transform.position = mousePosInWorld;

        //Calculates angle and converts it to degrees
        Vector3 diff = transform.position - mousePosInWorld;
        float angleRads = Mathf.Atan2(diff.z, diff.x);
        float angleDegs = angleRads * Mathf.Rad2Deg + 90f;
        return angleDegs;
    }

    Vector3 MovePlayer()
    {
        //Gets player input and clamps the magnitude to 1 so player movement isn't faster diagonally
        Vector2 input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        input = Vector3.ClampMagnitude(input, 1f);

        //sets the desired velocity by multiplying the player input by maxSpeed
        Vector3 desiredVelocity = new Vector3(input.x, 0f, input.y) * maxSpeed;

        //Gets the maximum allowed speed change by multiplying the maxAcceleration by Time.deltatime
        float maxSpeedChange = maxAcceleration * Time.deltaTime;

        //Sets the velocity of the player with MoveTowards, using the current velocity as the current,
        //the target as desiredVelocity, and the maxDelta as maxSpeedChange
        velocity.x = Mathf.MoveTowards(velocity.x, desiredVelocity.x, maxSpeedChange);
        velocity.z = Mathf.MoveTowards(velocity.z, desiredVelocity.z, maxSpeedChange);

        //Sets the displacement between each frame to velocity * Time.deltaTime
        Vector3 displacement = velocity * Time.deltaTime;
        return displacement;
    }

    void OnTriggerEnter(Collider collision)
    {
        //Player loses health when an enemy touches them
        //NOTE: TEMPORARY UNTIL ENEMIES CAN ATTACK
        if (collision.tag == "Enemy") healthController.health -= 5f;

        //Sets percentage of health bar fill in health script attatched to player
        healthController.healthBarPerc = healthController.getHealthBarPerc();
    }

    //Telemetry to get the player position
    IEnumerator Telemetry()
    {
        //Serialize data struct
        var data = new TelemetryData()
        {
            playerPos = transform.position,
            objectName = this.name
        };

        //Log Telemetry
        TelemetryLogger.Log(this, "Position", data);

        yield return new WaitForSeconds(10);

        yield return StartCoroutine(Telemetry());
    }
}
