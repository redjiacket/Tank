using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    //属性值
    public int lifeValue = 3;
    public int Score = 0;
    public bool Is_dead;
    public bool Is_defeat;
    //引用
    public GameObject Born;
    public Text Player_Score_text;
    public Text Player_LifeValue_Text;
    public GameObject Is_gameover;

    //单例
    private static PlayerManager instance;

    public static PlayerManager Instance { get => instance; set => instance = value; }

    private void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Is_defeat)
        {
            Is_gameover.SetActive(true);
            Invoke("ReturnToMainMenu", 3);
            return;
        }
        if (Is_dead)
        {
            Recover();
        }
        Player_Score_text.text = Score.ToString();
        Player_LifeValue_Text.text = lifeValue.ToString();
    }

    private void Recover()
    {
        if (lifeValue<0)
        {
            //游戏失败，返回主界面
            Is_defeat = true;
            Invoke("ReturnToMainMenu", 3);
        }
        else
        {
            lifeValue--;
            GameObject temp = Instantiate(Born, new Vector3(-2, -8, 0), Quaternion.identity);
            temp.GetComponent<birth>().creatPlayer = true;
            Is_dead = false;
        }
    }

    private void ReturnToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
