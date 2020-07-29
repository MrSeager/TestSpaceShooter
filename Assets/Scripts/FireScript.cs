using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireScript : MonoBehaviour
{
    public int damage = 40;
    public float speed = 4f;
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
        if (hitInfo.gameObject.tag == "End")
        {
            Destroy(gameObject);
        }
        else 
        {
            Instantiate(fireVFX, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
    IEnumerator DestroyFire() //уничтожение пули если не во что не попала
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
