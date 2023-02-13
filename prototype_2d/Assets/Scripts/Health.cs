using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    //maxHealth resets player health upon death and is used to determine what percentage the player's health is at
    [SerializeField, Range(0, 10)]
    float maxHealth = 10f;
    //length of health bar
    [SerializeField, Range(0.5f, 2f)]
    float healthBarLength = 2f;
    //height of health bar
    [SerializeField, Range(0.5f, 2f)]
    float healthBarHeight = 0.5f;

    //player health
    [System.NonSerialized]
    public float health;
    //percentage of health bar to fill
    [System.NonSerialized]
    public float healthBarPerc = 1f;

    Vector3 offset;
    LineRenderer lineRenderer;
    public Spawner spawnScript;
    public GameObject gameMaster;

    // Start is called before the first frame update
    void Start()
    {
        
        lineRenderer = GetComponent<LineRenderer>();

        //offset.x is half the bar length so middle of bar will be right above player
        offset = new Vector3(healthBarLength / 2, healthBarHeight);

        
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        gameMaster = GameObject.FindGameObjectWithTag("GameMaster");
        //spawnScript = gameMaster.GetComponent<Spawner>();
        //If the player loses their health, it respawns them in the center of the map
        //NOTE: This is temporary until there is a losing condition or respawn points
        if (health <= 0 && gameObject.name.Equals("Player")) {
            transform.position = new Vector3(0, 1, 0);
            health = maxHealth;
            healthBarPerc = getHealthBarPerc();
        } 
        //If an enemy loses their health, they dissapear
        else if (health <= 0) {
           // spawnScript.spawnPoint = Random.Range(0, spawnScript.Spawners.Length);
            Destroy(gameObject);
        } 

        //Draws health bar
        DrawHealthBar();
    }

    //Gets percentage of health left; gets a decimal value between 0 and 1
    public float getHealthBarPerc() {
        //
        return health * maxHealth / 100;
    }

    void DrawHealthBar()
    {
        //Set positions of health bar points
        //First Position: x value is player position minus offset.x, sets position to left of player; y value is player position plus offset.y, sets position above player; z value is untouched
        //Second position: same as before but adding the bar length to x value; (healthBarLength * healthBarPerc) means if player is at half health (0.5) the bar is halved in length
        lineRenderer.SetPosition(0, new Vector3(transform.position.x - offset.x, transform.position.y + offset.y, transform.position.z));
        lineRenderer.SetPosition(1, new Vector3(transform.position.x - offset.x + (healthBarLength * healthBarPerc), transform.position.y + offset.y, transform.position.z));

        //Set width of health bar
        lineRenderer.startWidth = 0.25f;
        lineRenderer.endWidth = 0.25f;

        //Bar is green for player; red for enemies
        if (gameObject.name.Equals("Player"))
        {
            lineRenderer.startColor = Color.green;
            lineRenderer.endColor = Color.green;
        }
        else
        {
            lineRenderer.startColor = Color.red;
            lineRenderer.endColor = Color.red;
        }
    }
}
