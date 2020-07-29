using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Rigidbody rb;
    Transform player;

    public GameObject explosionVFX;

    public GameObject firePrefab;
    public Transform fireSpawn;

    public int health = 100;
    public int points = 20;
    public int damage = 20;

    public float fireRate = .25f;

    private float nextFire;

    public float speed = 10;
    public float tilt = 5;

    Vector3 movement;
    Controller controller;

    // Start is called before the first frame update
    void Start()
    {
        health = 100;
        controller = GameObject.FindGameObjectWithTag("Controller").GetComponent<Controller>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    private void Update()
    {
        Movement();

        if (player != null && player.position.z >= transform.position.z - 50 && player.position.z <= transform.position.z)// если игрок близко открыть огонь
        {
            Shoot();
        }

        if (health <= 0)
        {
            controller.points += points;
            Instantiate(explosionVFX, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }

    void Movement() // дивжение за игроком по х и у
    {  
        if (player != null)
        {
            movement.x = player.position.x;
            movement.y = player.position.y;
            movement.z = transform.position.z;

            transform.position = Vector3.MoveTowards(transform.position, movement, speed * Time.fixedDeltaTime);
        }

        rb.rotation = Quaternion.Euler(movement.y * -tilt, -180f, movement.x * -tilt);
    }

    void Shoot()
    {
        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Instantiate(firePrefab, fireSpawn.position, fireSpawn.transform.rotation);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerController>().health -= damage;
            controller.points += points;
            Instantiate(explosionVFX, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Fire")
        {
            health -= other.GetComponent<FireScript>().damage;
        }

        if (other.tag == "Shield")
        {
            controller.points += points;
            Destroy(gameObject);
        }
    }
}
