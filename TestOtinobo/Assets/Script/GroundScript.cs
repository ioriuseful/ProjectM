using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundScript : MonoBehaviour
{
    [SerializeField] GameObject player;

    private void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.x > 0)
        {
            transform.position = new Vector3(player.transform.position.x, transform.position.y, transform.position.z);
        }
    }
}
