using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudGenerator : MonoBehaviour
{ 
    public GameObject[] Cloud;
    int rnd;
    public PlayerScript player;
    public PlayerScript.PlayerDate state;
    public bool onece;
    public float min = -0.2f, max = 0.2f;
    
    // Start is called before the first frame update
    void Start()
    {
        onece = true;
    }

    // Update is called once per frame
    void Update()
    {
        state = player.date;
        float random = Random.Range(min, max);
        float random1 = Random.Range(min, max);
        if (state==PlayerScript.PlayerDate.Cloud)
        {
            if(onece==true)
            {
                onece = false;
                rnd = Random.Range(0, Cloud.Length);
                Instantiate(Cloud[rnd], this.gameObject.transform.position+new Vector3(random,random1,0.0f), this.gameObject.transform.rotation,this.gameObject.transform.parent);                 
            }
        }
        else
        {
            onece = true;
        }
    }
}
