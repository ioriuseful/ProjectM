using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemJump : MonoBehaviour
{
    [SerializeField, Header("アイテムを入手したときの飛ぶ高さ")] public float boundHeight;
    [SerializeField, Header("アイテムを入手したときの飛ぶ回数")] public float boundCount;
    AudioSource audioSource;
    [SerializeField, Header("アイテムをとるSE")] public AudioClip ItemSE;
    private float time;
    private bool death;
    //敵を踏んだ時のフラグ
    public bool playerjump;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerjump == true)
        {
            time += Time.deltaTime;
            audioSource.PlayOneShot(ItemSE);
            Destroy(gameObject);
        }
        if(time>0.01f)
        {
            Destroy(gameObject);
        }
    }
}
