using UnityEngine;

public class ColorBlockScript : MonoBehaviour
{
    private SpriteRenderer sprite;
    public enum ColorState
    {
        White, Red, Blue, Green,
    }
    public ColorState CS;

    public PlayerScript player;
    private string Color;

    private GameObject colorobj;
    private Collider2D collider;
    [SerializeField, Header("カラーブロック入った時&出た時用パーティクル")]
    public GameObject RedBlockParticle;
    public GameObject GreenBlockParticle;
    public GameObject BlueBlockParticle;

    [SerializeField, Header("カラーブロックの移動用変数")]
    public float Yspeed;
    public float Xspeed;

    [SerializeField, Header("上下移動反転の間隔")] float time = 0.3f;
    private float timer = 0.01f;
    private float timelimit;
    private bool change = true;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        player = GameObject.Find("Player").GetComponent<PlayerScript>();
        collider = GetComponent<Collider2D>();
        Yspeed /= 100;
        Xspeed /= 100;
    }

    void Update()
    {
        switch (CS)
        {
            case ColorState.White:
                sprite.color = new Color(1, 1, 1, 1);
                Color = "white";
                break;
            case ColorState.Red:
                sprite.color = new Color(1, 0, 0, 1);
                Color = "Red";
                break;
            case ColorState.Green:
                sprite.color = new Color(0, 1, 0, 1);
                Color = "Green";
                break;
            case ColorState.Blue:
                sprite.color = new Color(0, 0, 1, 1);
                Color = "Blue";
                break;
        }

        Transform MyTransform = transform;

        Vector3 pos = MyTransform.position;

        #region 縦移動
        if (Yspeed != 0)
        {
            if (timelimit < time && change == true)
            {
                pos.y += Yspeed;
                timelimit += timer;
            }
            else
            {
                if (change == true)
                {
                    change = false;
                }
            }
            if (timelimit > 0f && change == false)
            {
                pos.y -= Yspeed;
                timelimit -= timer;
            }
            else
            {
                if (change == false)
                {
                    change = true;
                }
            }
        }
        #endregion

        #region 横移動

        if (Xspeed != 0)
        {
            pos.x -= Xspeed;
        }

        #endregion
        
        MyTransform.position = pos;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "Player" && Color == player.Color)
        {
            collider.isTrigger = true;
            switch(CS)
            {
                case ColorState.Red:
                    Instantiate(RedBlockParticle, player.transform.position, Quaternion.identity);
                    break;
                case ColorState.Green:
                    Instantiate(GreenBlockParticle, player.transform.position, Quaternion.identity);
                    break;
                case ColorState.Blue:
                    Instantiate(BlueBlockParticle, player.transform.position, Quaternion.identity);
                    break;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Rain")
        {
            Invoke("IsDead", 10.0f);
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            collider.isTrigger = false;
            switch (CS)
            {
                case ColorState.Red:
                    Instantiate(RedBlockParticle, player.transform.position, Quaternion.identity);
                    break;
                case ColorState.Green:
                    Instantiate(GreenBlockParticle, player.transform.position, Quaternion.identity);
                    break;
                case ColorState.Blue:
                    Instantiate(BlueBlockParticle, player.transform.position, Quaternion.identity);
                    break;
            }
        }
    }

    void IsDead()
    {
        Destroy(gameObject);
    }
}
