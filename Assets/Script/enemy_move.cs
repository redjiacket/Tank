using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_move : MonoBehaviour
{
    //����ֵ
    public float move_speed = 3f;
    private Vector3 bullet_EulerAngles;
    
    //����
    private SpriteRenderer sr;
    public Sprite[] tankSprites;//w d s a
    public GameObject bullet_prefab;
    public GameObject explosion_prefab;

    //��ʱ��
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
        
        //�������
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
            if (num>5)//����
            {
                ver = -1;
                hor = 0;
            }
            else if (num==0)//����
            {
                ver = 1;
                hor = 0;
            }
            else if (num>0&&num<=2)//����
            {
                hor = -1;
                ver = 0;
            }
            else if (num>2&&num<=4)//����
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
        //����
        Destroy(gameObject);
        //������ը��Ч
        Destroy(Instantiate(explosion_prefab, transform.position, transform.rotation), 0.17f);

    }

    //�����������ײ�Զ�ת��
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag=="Enemy")
        {
            timeValChangeDirection = 4;
        }
    }
}
