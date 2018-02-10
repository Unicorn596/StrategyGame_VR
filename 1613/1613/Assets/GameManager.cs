using UnityEngine;
using System.Collections;

[AddComponentMenu("MyGame/GameManager")]
public class GameManager : MonoBehaviour {

    public static GameManager Instance;

    //base12
    protected base1 base1;
    protected sbAI base2;

    //背景音乐
    public AudioClip m_musicClip;
    //声音源
    protected AudioSource m_Audio;

    void Awake()
    {
        Instance = this;
    }
    // Use this for initialization
	void Start () {
        //m_Audio = this.audio;
        //获取两个基地
        //GameObject obj_1 = GameObject.FindGameObjectWithTag("team1");
        //if(obj_1 !=null)
        //{
        //    base1 = obj_1.GetComponent<GameObject>();
           
        //}
        GameObject obj_2 = GameObject.FindGameObjectWithTag("base2");
        GameObject obj_1 = GameObject.FindGameObjectWithTag("base1");
        if(obj_2 !=null&&obj_1!=null)
        {
            base2 = obj_2.GetComponent<sbAI>();
            base1 = obj_1.GetComponent<base1>();

        }

	}
	
	// Update is called once per frame
	void Update () {
        //退出键暂停游戏
        if (Time.timeScale > 0 && Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0;
        }
	}

    void OnGUI()
    {
        //游戏暂停
        if(Time.timeScale == 0)
        {
            //继续游戏
            if(GUI.Button(new Rect(Screen.width * 0.5f-50 ,Screen.height*0.4F,100,30),"继续游戏"))
            {
                Time.timeScale =1;
            }
            //退出游戏
             if(GUI.Button(new Rect(Screen.width * 0.5f-50 ,Screen.height*0.6F,100,30),"退出游戏"))
             {
                 Application.Quit();
             }
        }

        int life2,SoliderPoint2,life1,SoliderPoint1;
        if(base2 !=null&&base1!=null)
        {
            life2 = (int)base2.m_life;
            SoliderPoint2 = (int)base2.SoliderPoint;
            life1 = (int)base1.m_life;
            SoliderPoint1 = (int)base1.SoliderPoint;
            GUI.skin.label.fontSize = 30;
            GUI.skin.label.alignment = TextAnchor.UpperLeft;
            GUI.Label(new Rect(Screen.width*0.75f, Screen.height * 0.1f, Screen.width, 60), "Self.life=" + life1);
            GUI.Label(new Rect(Screen.width * 0.75f, Screen.height * 0.2f, Screen.width, 60), "Self.SoliderPoint=" + SoliderPoint1);
            GUI.Label(new Rect(20, Screen.height * 0.1f, Screen.width, 60), "Enemy.life=" + life2);
            GUI.Label(new Rect(20, Screen.height * 0.2f, Screen.width, 60), "Enemy.SoliderPoint=" + SoliderPoint2);
        }
        else //GG
        {
            //放大字体
            GUI.skin.label.fontSize = 50;

            //显示游戏失败
            GUI.skin.label.alignment = TextAnchor.LowerCenter;
            if (base1 == null)
            {
                GUI.Label(new Rect(0, Screen.height * 0.2f, Screen.width, 60), "Defeat");
            }
            if (base2== null)
            {
                GUI.Label(new Rect(0, Screen.height * 0.2f, Screen.width, 60), "Win");
            }
            GUI.skin.label.fontSize = 20;

            //显示按钮
            if (GUI.Button(new Rect(Screen.width * 0.5f - 50, Screen.height * 0.5f, 100, 30), "重新开始"))
            {
               Application.LoadLevel("map");
            }
        }
    }

}
