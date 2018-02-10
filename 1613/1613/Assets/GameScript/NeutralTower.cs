using UnityEngine;
using System.Collections;
using System;
public class NeutralTower : MonoBehaviour
{


    ////士兵变量
    public Transform solider_fireEnemy;
    public Transform solider_fireMe;
    Vector3 shuabingdian;
    float shuabingshijian ;
    //public transform solider3;
    //float m_soliderrate = 0;

    ////士兵能量点
    //public int soliderpoint, sp;
    ////创建士兵标识符
    //public transform soliderclass;
    //string soliderclass;


    //士兵是否行走
    public int CanMoveOrNot = 0;

    //敌人
    public GameObject Enemy;
    //子弹
    public GameObject m_zidan;
    public float m_ProductSpeed = 10; //这里
    float m_rocketRate = 0;
    Vector3 shootpoint;
    //生命值
    public float m_life = 0;


    //所属方标记
    public int Statue;//0为中立，12为各自所属方


    //0为初态，
    //1为属于一队的非正在占领状态，2为属于二队的非正在占领状态，
    //3为一队正在占领状态，4为二队正在占领状态
    public int TransientStatue;


    //属于谁的虚血{现在的占领值，现在塔所属，刚刚谁攻击}
    float[] HP = new float[3] { 0, 0, 0 };

    int time_1 = 0;
    int times_bomb = 20;
    public int shootcontrol = 0;
    public string EnemyTeam;
    int[] HaveSoliderMath = new int[6] { 0, 0, 0, 0, 0, 0 };
    int[] HaveSolider = new int[2] { 0, 0 };

    int test1 = 0, test2 = 0;
    float timetime; float timetime1;
    void Start()
    {
        Statue = 0;
        shuabingshijian = m_ProductSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        timetime1 += Time.fixedDeltaTime;
        SetEnemy();
        solidermove();
        Occupation();
        shoot();

        m_life = HP[0];


        shuabing();

    }
    void FixedUpdate()
    {
        HaveSoliderMath[3] = HaveSoliderMath[0] - HaveSoliderMath[1];


        //   Debug.Log((HaveSoliderMath[0] - HaveSoliderMath[4]) + "        " + (HaveSoliderMath[1] - HaveSoliderMath[5]) + "    " + (HaveSoliderMath[2] - HaveSoliderMath[3]));



        calculate();




        // Debug.Log("11111111111111111");

        HaveSoliderMath[4] = HaveSoliderMath[0];
        HaveSoliderMath[5] = HaveSoliderMath[1];
        HaveSoliderMath[2] = HaveSoliderMath[0] - HaveSoliderMath[1];
    }

    void shuabing()
    {
        if (Statue == 1)
        {
            shuabingdian = this.transform.position - new Vector3(0, 0, 20);
        }
        if (Statue == 2)
        {
            shuabingdian = this.transform.position + new Vector3(0, 0, 20);
        }
        shuabingshijian -= Time.deltaTime;
        if (shuabingshijian <= 0)
        {
            if (Statue == 1)
                Instantiate(solider_fireEnemy, shuabingdian, new Quaternion());
            if (Statue == 2)
                Instantiate(solider_fireMe, shuabingdian, new Quaternion());
            shuabingshijian = m_ProductSpeed;//这里
        }
    }


    void calculate()
    {
        if (HaveSoliderMath[2] - HaveSoliderMath[3] > 0)//二队多于一队    //>
        {
            if (HaveSoliderMath[4] == HaveSoliderMath[0])//只有二队
            {
                HaveSolider[0] = 0;
                HaveSolider[1] = 1;
                if ((Statue == 1) || (Statue == 0))
                {
                    TransientStatue = 4;
                    shootcontrol = 1;
                    /////////////////////////////////////////////////////////////////////
                }
                if (Statue == 2)
                {
                    TransientStatue = 2;
                    shootcontrol = 1;
                }

            }
            if (HaveSoliderMath[4] != HaveSoliderMath[0])//两队进来
            {
                HaveSolider[0] = 1;
                HaveSolider[1] = 1;
                TransientStatue = Statue;
                if (Statue != 0)
                    shootcontrol = 0;
            }

        }





        if (HaveSoliderMath[2] - HaveSoliderMath[3] < 0)//一队多于二队   //<
        {
            if (HaveSoliderMath[5] == HaveSoliderMath[1])//只有一队
            {
                HaveSolider[0] = 1;
                HaveSolider[1] = 0;
                if ((Statue == 2) || (Statue == 0))
                {
                    TransientStatue = 3;

                    shootcontrol = 1;
                    /////////////////////////////////////////////////////////////////////
                }
                if (Statue == 1)
                {
                    TransientStatue = 1;
                    shootcontrol = 1;
                }
            }
            if (HaveSoliderMath[5] != HaveSoliderMath[1])//两队进来
            {
                HaveSolider[0] = 1;
                HaveSolider[1] = 1;
                TransientStatue = Statue;
                if (Statue != 0)
                    shootcontrol = 0;
            }
        }
        if (HaveSoliderMath[2] == HaveSoliderMath[3])//双方人数相等
        {
            if (HaveSoliderMath[4] == HaveSoliderMath[0])//没人
            {
                HaveSolider[0] = 0;
                HaveSolider[1] = 0;
                TransientStatue = Statue;
                shootcontrol = 1;
            }
            if (HaveSoliderMath[4] != HaveSoliderMath[0])//两队进来
            {
                HaveSolider[0] = 1;
                HaveSolider[1] = 1;
                TransientStatue = Statue;
                if (Statue != 0)
                    shootcontrol = 0;
            }

            //////////////////////////////////////////////////////////////////////////////
        }
    }

    void solidermove()
    {
        if (TransientStatue == 1 || TransientStatue == 2 || TransientStatue == 0)
        {
            CanMoveOrNot = 1;
        }
        if (TransientStatue == 3 || TransientStatue == 4)
        {
            CanMoveOrNot = 0;
        }
    }
    void SetEnemy()
    {
        if (Statue == 1)
        { EnemyTeam = "team2"; }
        if (Statue == 2)
        { EnemyTeam = "team1"; }
    }
    void HaveSoliderInCollider(Collider other)
    {


        // Debug.Log("aaaaaaaaaaaaaa");
        //  HaveSoliderMath[4] = HaveSoliderMath[0];
        // HaveSoliderMath[5] = HaveSoliderMath[1];

        if (other.tag.CompareTo("team1") == 0)
        {

            HaveSoliderMath[0]++;
        }
        if (other.tag.CompareTo("team2") == 0)
        {

            HaveSoliderMath[1]++;
        }

    }


    void Occupation()//占领，扣血
    {
        //TransientStatue                                                            Statue
        //0为初态                                                                    0为中立，12为各自所属方
        //1为属于一队的非正在占领状态，2为属于二队的非正在占领状态  
        //3为一队正在占领状态，4为二队正在占领状态

        //属于谁的虚血{现在的占领值，现在塔所属，刚刚谁攻击}
        // float[] HP = new float[3] { 0, 0, 0 };



        if (HP[2] == 0)
        {
            //  Debug.Log("dd22d");
            if (TransientStatue == 3)
                HP[2] = 1;
            if (TransientStatue == 4)
                HP[2] = 2;
        }

        if (HP[2] == 1)
        {
            //Debug.Log("dd33d");
            if (TransientStatue == 3 && HP[0] < 300)
            {
                HP[0]++;//占领读条ing
            }
            if (TransientStatue == 4)
            {
                HP[0]--;//移除敌人占领的读条ing
            }
        }
        if (HP[2] == 2)
        {
            //   Debug.Log("dd44d");
            if (TransientStatue == 4 && HP[0] < 300)
            {
                HP[0]++;//占领读条ing
            }
            if (TransientStatue == 3)
            {
                HP[0]--;//移除敌人占领的读条ing
            }
        }


        if (TransientStatue != 3 && TransientStatue != 4 && HP[0] < 300 && Statue != 0)//没人攻击自己回血
        {
            //     Debug.Log("dd55d");
            HP[0]++;
        }
        if (TransientStatue != 3 && TransientStatue != 4 && HP[0] > 0 && Statue == 0)//没人攻击自己回血
        {
            //      Debug.Log("dd66d");
            HP[0]--;
        }

        if (HP[0] == 0)
        {
            HP[2] = 0;
            Statue = 0;
        }
        if (HP[0] == 300)
        {
            if (Statue == 0)
            {
                if (TransientStatue == 3)
                {
                    HP[2] = 1;
                    Statue = 1;

                }
                if (TransientStatue == 4)
                {
                    HP[2] = 2;
                    Statue = 2;
                }
            }

        }


    }
    void shoot()
    {
        if (shootcontrol == 0)//允许开火
        {
            m_rocketRate -= Time.deltaTime;
            if (m_rocketRate <= 0)
            {
                m_rocketRate = 1f;
                shootpoint = this.transform.position;
                Instantiate(m_zidan, shootpoint, this.transform.rotation);
            }
        }
        else
        {
            Enemy = null;
        }
    }
    void OnTriggerEnter(Collider other)
    {

    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag.CompareTo("team1") == 0 || other.tag.CompareTo("team2") == 0)
            HaveSoliderInCollider(other);



        if (other.tag.CompareTo(EnemyTeam) == 0)//设置攻击对象
        {
            if (shootcontrol == 0)
            {
                if (Enemy == null)
                    Enemy = other.gameObject;
                //  Debug.Log("position" + other.gameObject.transform.position);
            }

        }

        //if (other.tag.CompareTo("team2") == 0)
        //{
        //    test2 = test2 + 1;

        //} 
        //test1 = test1+1;


    }








    void HowTimeDoOnce(Action function, float time, bool IsCycle)
    {
        StartCoroutine(WaitForTime(function, time, IsCycle));
    }//方法，多久一次，是否循环
    IEnumerator WaitForTime(Action action, float waitTime, bool IsCycle)
    {
        if (IsCycle)
        {
            while (true)
            {
                yield return new WaitForSeconds(waitTime); action();

            }
        }
        else
        {
            yield return new WaitForSeconds(waitTime); action();
        }
    }//不用看

}


//塔的逻辑
//1. 塔不能被毁灭只能被占领，故无生命值
//2. 占领采取读条的方式，士兵越多，读条速度越快
//3. 士兵占领塔的时候就在塔下不移动，如有敌方过来则相互攻打直至剩下其中一方为止
//4. 塔被占领后会攻击敌方，但如果敌方尝试夺取的时候会停止攻击
//5. 如果敌方先占领，那么己方占领的时候需要先消除对方的读条
//6. 塔被占领后会生成小兵助力进攻

