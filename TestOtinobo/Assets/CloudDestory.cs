using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudDestory : MonoBehaviour
{
   public float timer=0;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0;   
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer>2)
        {
            Destroy(this.gameObject);
        }
    }
}
