using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public int health = 100;
    public int damage = 20;
    public int points = 10;

    public Transform pastThroughPoint;
    public Vector3 pastThroughRange;
    public LayerMask playerLayer;

    Controller controller;
    // Start is called before the first frame update
    void Start()
    {
        pastThroughPoint.position = new Vector3(0f, 0f, transform.position.z + 1);
        health = 100;
        controller = GameObject.FindGameObjectWithTag("Controller").GetComponent<Controller>();
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            controller.points += points;
            Destroy(gameObject);
        }
        PassingThrough();
    }

    void PassingThrough()
    {
        Collider[] playerDitect = Physics.OverlapBox(pastThroughPoint.position, pastThroughRange, Quaternion.identity, playerLayer);

        foreach (Collider player in playerDitect)
        {
            controller.points += points;
            GetComponent<Obstacle>().enabled = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerController>().health -= damage;
            controller.points += points;
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
    private void OnDrawGizmosSelected()
    {
        if (pastThroughPoint == null)
            return;

        Gizmos.DrawCube(pastThroughPoint.position, pastThroughRange);
    }
}
