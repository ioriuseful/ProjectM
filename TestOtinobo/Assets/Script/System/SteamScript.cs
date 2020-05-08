using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteamScript : MonoBehaviour
{
    int timer;
    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timer++;
        if(timer>=60)
        {
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag=="Player")
        {
            timer = 60;
        }
    }
}
