using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialEnemyGenerate : MonoBehaviour
{
    public List<GameObject> enemys = new List<GameObject>();
    bool isgenerateflag;
    int count;
    private void Update()
    {
        if (isgenerateflag)
        {
            count++;
        }
        if(count >= 120)
        {
            int rnd = Random.Range(0, enemys.Count);
            Instantiate(enemys[rnd], transform.position, Quaternion.identity);
            count = 0;
            isgenerateflag = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            isgenerateflag = true;
            
        }
    }

}
