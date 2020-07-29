using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject controlPanel;
    public GameObject shield;
    public GameObject firePrefab;
    public GameObject explosionVFX;

    public Rigidbody rb;
    public Transform fireSpawn, fireSpawn2, fireSpawn3;

    public float boundary = 5f;

    float speed;
    public float normSpeed = 10;
    public float speedBust = 10f;
    public float speedBustRate = 10f;

    public float nextSpeedBust;

    public float tilt = 3f;
    public float shieldRate = 10f;

    private float nextShield;

    public int health = 100;

    public VariableJoystick joystick;

    Vector3 movement;

    // Start is called before the first frame update
    void Start()
    {
        speed = normSpeed;
    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_STANDALONE_WIN
    controlPanel.SetActive(false);

    movement.x = Mathf.Lerp(0f, Input.GetAxisRaw("Horizontal"), speed);
    movement.y = Mathf.Lerp(0f, Input.GetAxisRaw("Vertical"), speed);

    if (Input.GetButtonDown("Fire1"))
    {
        Shoot();
    }

    if (Input.GetButtonDown("Fire2"))
    {
        SecondShoot();
    }

    if (Input.GetButtonDown("Fire3"))
    {
        Shield();
    }

    if(Input.GetButtonDown("Run"))
    {
        SpeedPlus();
    }

    if (Input.GetButtonDown("Jump"))
    {
        SpeedMinus();
    }
#endif

#if UNITY_ANDROID
    controlPanel.SetActive(true);

    movement.x = joystick.Horizontal;
    movement.y = joystick.Vertical;
#endif

        if (speed != normSpeed && Time.time > nextSpeedBust - speedBustRate / 2)
        {
            speed = normSpeed;
        }

        if (health <= 0)
        {
            Instantiate(explosionVFX, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = transform.forward * speed;

        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
        rb.rotation = Quaternion.Euler(movement.y * -tilt, 0.0f, movement.x * -tilt);

        rb.position = new Vector3
        (
        Mathf.Clamp(rb.position.x, -boundary, boundary),
        Mathf.Clamp(rb.position.y, -boundary, boundary),
        rb.position.z
        );
    }

    public void Shoot()
    {
        Instantiate(firePrefab, fireSpawn.position, fireSpawn.transform.rotation);
    }

    public void SecondShoot()
    {
        Instantiate(firePrefab, fireSpawn2.position, fireSpawn2.transform.rotation);
        Instantiate(firePrefab, fireSpawn3.position, fireSpawn3.transform.rotation);
    }

    public void SpeedPlus()
    {
        if (Time.time > nextSpeedBust)
        {
            nextSpeedBust = Time.time + speedBustRate;
            speed += speedBust;
        }
    }

    public void SpeedMinus()
    {
        if (Time.time > nextSpeedBust)
        {
            nextSpeedBust = Time.time + speedBustRate;
            speed -= speedBust;
        }
    }

    public void Shield()
    {
        if (Time.time > nextShield)
        {
            nextShield = Time.time + shieldRate;
            shield.GetComponent<ShieldScript>().endTime = Time.time + shieldRate / 2;
            shield.SetActive(true);
        }
    }
}
