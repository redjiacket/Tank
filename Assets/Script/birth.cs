using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class birth : MonoBehaviour
{
    public GameObject player_Prefab;
    public GameObject[] enemyPrefabList;
    public bool creatPlayer;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("born_tank", 0.8f);
        Destroy(gameObject, 0.8f);
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void born_tank()
    {
        if (creatPlayer)
        {
            Instantiate(player_Prefab, transform.position, Quaternion.identity);
        }
        else
        {
            int num = Random.Range(0, 2);
            Instantiate(enemyPrefabList[num], transform.position, Quaternion.identity);

        }
    }
}
