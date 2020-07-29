using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFireScript : MonoBehaviour
{
    public int damage = 20;
    public float speed = 50f;
    public Rigidbody rb;
    public GameObject fireVFX;
    public float time = 1f;
    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.up * speed;
        StartCoroutine(DestroyFire());
    }

    void OnTriggerEnter(Collider hitInfo)
    {
        if (hitInfo.tag == "Player")
        {
            hitInfo.GetComponent<PlayerController>().health -= damage;
        }

        Instantiate(fireVFX, transform.position, transform.rotation);
        Destroy(gameObject);
    }
    IEnumerator DestroyFire()// уничтожение пули если не во что не попала
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
