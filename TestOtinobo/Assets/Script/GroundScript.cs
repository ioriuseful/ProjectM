using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundScript : MonoBehaviour
{
    [SerializeField] GameObject player;
    [Header("何Wave目から移動を止めるか")] public int StopGroundWave;
    public WaveScript nowWave;

    public bool isStopGroundflag = false;


    private void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.x > 0 && !isStopGroundflag)
        {
            transform.position = new Vector3(player.transform.position.x, transform.position.y, transform.position.z);
        }

        //if (nowWave.wave >= StopGroundWave)
        //{
        //    isStopGroundflag = true;
        //}

    }
}
