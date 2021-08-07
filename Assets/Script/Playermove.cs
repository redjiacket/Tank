using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playermove : MonoBehaviour
{
    //属性值
    public float move_speed = 3f;
    private Vector3 bullet_EulerAngles;
    private float time_Val=1f;
    private float defend_timeval=1f;
    private bool is_Defended=true;
    //引用
    private SpriteRenderer sr;
    public Sprite[] tankSprites;//w d s a
    public GameObject bullet_prefab;
    public GameObject explosion_prefab;
    public GameObject defend_prefab;
    public AudioSource moveAudio;
    public AudioClip[] tankAudio;


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
        //初始保护
        if(is_Defended)
        {
            defend_prefab.SetActive(true);
            defend_timeval -= Time.deltaTime;
            if(defend_timeval<=0)
            {
                is_Defended = false;
                defend_prefab.SetActive(false);
            }
        }
        else
        {
            is_Defended = false;
            defend_prefab.SetActive(false);
        }

        
    }
    private void FixedUpdate()
    {
        if (PlayerManager.Instance.Is_defeat)
        {
            return;
        }
        Move();
        //攻击cd
        if (time_Val >= 0.4f)
        {
            Attack();
        }
        else
        {
            time_Val += Time.fixedDeltaTime;
        }
    }

    //tankmove
    private void Move()
    {
        float hor = Input.GetAxisRaw("Horizontal");
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
        if (Mathf.Abs(hor)>0.05f)
        {
            moveAudio.clip = tankAudio[1];
            
            if (!moveAudio.isPlaying)
            {
                moveAudio.Play();
            }
        }

        if (hor != 0)
            return;
        float ver = Input.GetAxisRaw("Vertical");
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

        if (Mathf.Abs(ver) > 0.05f)
        {
            moveAudio.clip = tankAudio[1];

            if (!moveAudio.isPlaying)
            {
                moveAudio.Play();
            }
        }
        else
        {
            moveAudio.clip = tankAudio[0];

            if (!moveAudio.isPlaying)
            {
                moveAudio.Play();
            }
        }
    }
    //tank attack
    private void Attack()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (bullet_prefab == null)
                return;
            Instantiate(bullet_prefab, transform.position, Quaternion.Euler(transform.eulerAngles + bullet_EulerAngles));
            time_Val = 0f;
        }
    }

    //tank die
    private void Die()
    {
        if(is_Defended)
        {
            return;
        }

        PlayerManager.Instance.Is_dead = true;
        //死亡
        Destroy(gameObject);
        //产生爆炸特效
        Destroy( Instantiate(explosion_prefab, transform.position, transform.rotation),0.17f);
        
    }
}
