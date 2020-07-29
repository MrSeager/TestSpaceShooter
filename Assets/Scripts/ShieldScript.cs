using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldScript : MonoBehaviour
{
    public float time = 10;

    public float endTime;// время после которго щит выключается

    public Transform player;

    public Animator animator;

    // Update is called once per frame
    void Update()
    {
        if (Time.time > endTime)
        {
            animator.SetTrigger("End");

            if (transform.localScale.x == 0)
            {
                gameObject.SetActive(false);
            }
        }

        transform.position = player.position;
        transform.rotation = player.rotation;
    }
}
