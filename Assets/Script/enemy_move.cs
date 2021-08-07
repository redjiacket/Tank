using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_move : MonoBehaviour
{
    //属性值
    public float move_speed = 3f;
    private Vector3 bullet_EulerAngles;
    
    //引用
    private SpriteRenderer sr;
    public Sprite[] tankSprites;//w d s a
    public GameObject bullet_prefab;
    public GameObject explosion_prefab;

    //计时器
    private float time_Val = 1f;
    private float timeValChangeDirection;
    private float hor;
    private float ver=-1;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
        //攻击间隔
        if (time_Val >= 4f)
        {
            Attack();
        }
        else
        {
            time_Val += Time.deltaTime;
        }
    }
    private void FixedUpdate()
    {
        Move();
    }

    //enemymove
    private void Move()
    {
        if (timeValChangeDirection>3.5)
        {
            int num = Random.Range(0, 8);
            if (num>5)//向下
            {
                ver = -1;
                hor = 0;
            }
            else if (num==0)//向上
            {
                ver = 1;
                hor = 0;
            }
            else if (num>0&&num<=2)//向左
            {
                hor = -1;
                ver = 0;
            }
            else if (num>2&&num<=4)//向右
            {
                hor = 1;
                ver = 0;
            }
            timeValChangeDirection = 0;
        }
        else
        {
            timeValChangeDirection += Time.fixedDeltaTime;
        }
        
        transform.Translate(Vector3.right * hor * move_speed * Time.deltaTime, Space.World);
        if (hor < 0)
        {
            sr.sprite = tankSprites[3];
            bullet_EulerAngles = new Vector3(0, 0, 90);
        }
        else if (hor > 0)
        {
            sr.sprite = tankSprites[1];
            bullet_EulerAngles = new Vector3(0, 0, -90);
        }
        if (hor != 0)
            return;
        
        transform.Translate(Vector3.up * ver * move_speed * Time.deltaTime, Space.World);
        if (ver < 0)
        {
            sr.sprite = tankSprites[2];
            bullet_EulerAngles = new Vector3(0, 0, -180);
        }
        else if (ver > 0)
        {
            sr.sprite = tankSprites[0];
            bullet_EulerAngles = new Vector3(0, 0, 0);
        }
    }
    //tank attack
    private void Attack()
    {
        if (bullet_prefab == null)
            return;
        Instantiate(bullet_prefab, transform.position, Quaternion.Euler(transform.eulerAngles + bullet_EulerAngles));
        time_Val = 0f;
        
    }

    //tank die
    private void Die()
    {
        PlayerManager.Instance.Score += 1;
        //死亡
        Destroy(gameObject);
        //产生爆炸特效
        Destroy(Instantiate(explosion_prefab, transform.position, transform.rotation), 0.17f);

    }

    //敌人与敌人碰撞自动转向
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag=="Enemy")
        {
            timeValChangeDirection = 4;
        }
    }
}
