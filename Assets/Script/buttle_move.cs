using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttle_move : MonoBehaviour
{
    public float buttle_movespeed = 10;
    public bool is_player_bullet;

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(transform.up * buttle_movespeed * Time.deltaTime, Space.World);
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "Enemy":
                if (is_player_bullet)
                {
                    collision.SendMessage("Die");
                    Destroy(gameObject);
                }
                break;
            case "Wall":
                Destroy(collision.gameObject);//摧毁墙体
                Destroy(gameObject);
                break;
            case "Barrier":
                if (is_player_bullet)
                {
                    collision.SendMessage("Play_Audio");
                }
                Destroy(gameObject);
                break;
            case "Tank":
                if (!is_player_bullet)//敌人的子弹
                {
                    collision.SendMessage("Die");
                    Destroy(gameObject);
                }
                break;
            case "Home":
                collision.SendMessage("Die");
                Destroy(gameObject);
                break;
            default:
                break;
        }
    }
}
