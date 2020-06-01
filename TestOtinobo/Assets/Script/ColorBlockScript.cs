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

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        player = GameObject.Find("Player").GetComponent<PlayerScript>();
        collider = GetComponent<Collider2D>();
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
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "Player" && Color == player.Color)
        {
            collider.isTrigger = true;
            gameObject.tag = "Ground";
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
}
