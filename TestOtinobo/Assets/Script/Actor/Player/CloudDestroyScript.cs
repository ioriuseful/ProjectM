using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudDestroyScript : MonoBehaviour
{
    private PlayerScript player;
    private PlayerScript.PlayerDate date;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        date = player.date;
        if(date == PlayerScript.PlayerDate.Normal)
        {
            Destroy(gameObject);
        }
    }
}
