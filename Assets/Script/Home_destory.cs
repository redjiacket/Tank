using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Home_destory : MonoBehaviour
{
    private SpriteRenderer sr;
    public GameObject explosionPrefab;
    public Sprite Bronken_home;
    public AudioClip DieAudio;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    public void Die()
    {
        sr.sprite = Bronken_home;
        Destroy(Instantiate(explosionPrefab,transform.position, transform.rotation),0.17f);
        PlayerManager.Instance.Is_defeat = true;
        AudioSource.PlayClipAtPoint(DieAudio, transform.position);
    }
}
