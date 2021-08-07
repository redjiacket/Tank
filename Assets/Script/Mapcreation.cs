using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mapcreation : MonoBehaviour
{
    //�����ĳ��Ϳ�
    private int wide = 13;
    private int height = 8;
    //����������ʼ����ͼ����Ԥ���������
    //0.home,1.wall,2.barrier,3.born,4.river,5.grass,6,airbarrier
    public GameObject[] items;

    //�����Ѿ�ʹ�ù���λ��
    private List<Vector3> itemPositionList = new List<Vector3>();
    
    private void Awake()
    {
        InitMap();
    }

    private void InitMap()
    {
        //��ʼ����ҵ�λ��
        itemPositionList.Add(new Vector3(-2, -8, 0));

        //ʵ����home
        Creat_items(items[0], new Vector3(0, -height, 0), Quaternion.identity);
        //��wallΧסhome
        Creat_items(items[1], new Vector3(-1, -height, 0), Quaternion.identity);
        Creat_items(items[1], new Vector3(1, -height, 0), Quaternion.identity);
        for (int i = -1; i < 2; i++)
        {
            Creat_items(items[1], new Vector3(i, -height + 1, 0), Quaternion.identity);
        }
        //��Χ����ǽ
        for (int i = -wide; i < wide + 1; i++)
        {
            Creat_items(items[6], new Vector3(i, height + 1, 0), Quaternion.identity);
        }
        for (int i = -wide; i < wide + 1; i++)
        {
            Creat_items(items[6], new Vector3(i, -height - 1, 0), Quaternion.identity);
        }
        for (int i = -height; i <= height; i++)
        {
            Creat_items(items[6], new Vector3(wide + 1, i, 0), Quaternion.identity);
        }
        for (int i = -height; i <= height; i++)
        {
            Creat_items(items[6], new Vector3(-wide - 1, i, 0), Quaternion.identity);
        }
        //ʵ����ʣ������
        for (int i = 0; i < 20; i++)//wall
        {
            Creat_items(items[1], CreatRandomPosition(), Quaternion.identity);
        }
        for (int i = 0; i < 20; i++)//barrier
        {
            Creat_items(items[2], CreatRandomPosition(), Quaternion.identity);
        }
        for (int i = 0; i < 10; i++)//river
        {
            Creat_items(items[4], CreatRandomPosition(), Quaternion.identity);
        }
        for (int i = 0; i < 20; i++)//grass
        {
            Creat_items(items[5], CreatRandomPosition(), Quaternion.identity);
        }
        //��ʼ�����
        GameObject temp = Instantiate(items[3], new Vector3(-2, -8, 0), Quaternion.identity);
        temp.GetComponent<birth>().creatPlayer = true;

        //��ʼ������
        Creat_items(items[3], new Vector3(0, 8, 0), Quaternion.identity);
        Creat_items(items[3], new Vector3(13, 8, 0), Quaternion.identity);
        Creat_items(items[3], new Vector3(-13, 8, 0), Quaternion.identity);
        InvokeRepeating("Creat_enemy", 4, 5);
    }
    private void Creat_items(GameObject creat_gameobiect, Vector3 creat_position, Quaternion creat_rotation)
    {
        GameObject temp = Instantiate(creat_gameobiect, creat_position, creat_rotation);
        temp.transform.SetParent(gameObject.transform);
        itemPositionList.Add(creat_position);

    }

    private Vector3 CreatRandomPosition()
    {
        //������x=-13��13��y=8��-8������
        while (true)
        {
            Vector3 creatPosition = new Vector3(Random.Range(-wide+1, wide), Random.Range(-height+1, height), 0);
            if (!If_positon_exist(creatPosition))
            {
                return creatPosition;
            }
            
        }
    }

    private bool If_positon_exist(Vector3 creatposition)
    {
        for (int i = 0; i < itemPositionList.Count; i++)
        {
            if (creatposition==itemPositionList[i])
            {
                return true;
            }
        }
        return false;
    }

    //�������˷���
    private  void Creat_enemy()
    {
        int num = Random.Range(0, 3);
        Vector3 EnemyPos = new Vector3();
        switch (num)
        {
            case 0:EnemyPos = new Vector3(-13, 8, 0);
                break;
            case 1:
                EnemyPos = new Vector3(0, 8, 0);
                break;
            case 2:
                EnemyPos = new Vector3(13, 8, 0);
                break;
            default:
                break;
        }
        Creat_items(items[3], EnemyPos, Quaternion.identity);
    }
}
