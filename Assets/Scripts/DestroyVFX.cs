using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyVFX : MonoBehaviour
{
    public float time;// время после которого уничтожается VFX
    // Start is called before the first frame update
    void Start()
    {
        DestroyObject();
    }

    IEnumerator DestroyObject()// уничтожение VFX
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
