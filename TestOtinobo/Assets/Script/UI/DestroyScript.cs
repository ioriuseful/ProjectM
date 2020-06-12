using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyScript : MonoBehaviour
{
    private PlayerScript aaa;

    private void Update()
    {
        aaa = GameObject.Find("Player").GetComponent<PlayerScript>();
    }


    public void objDestroy()
    {
        Destroy(gameObject);
    }
}
