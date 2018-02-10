using UnityEngine;
using System.Collections;
using System;
public class move : MonoBehaviour
{
    public Rigidbody rb;
    public GameObject base1,base2;
    public float m_speed = 50;




    public int MoveAllow = 1;
    int MoveAllow_tem1=1;//move 是否射击
    int MoveAllow_tem2=1;//中立塔给与的




    public int ShootAllow = 0;
    public string EnemyTeam1;
    public string EnemyTeam2; 
    Vector3 Keep_Rotation;
    public GameObject Enemy;
    public float m_life = 20;
    //子弹相关变量
    GameObject zidangs;
    public GameObject m_zidang;
    float m_rocketRate = 0;
    public Vector3 pos;//士兵位置
    public Vector3 aim;//当前目标
    Vector3 posAdd;
    int bijiao1 = new int(), bijiao2 = new int();
    int times_b=50;
    int times_bomb = 20;
    int DeadConfirmNumber = 20;
   public int shootcontrol = 0;
    Vector3 shootpoint;
   int time_1 = 0;
   Vector3 add;
    void Start()
    {
        StartTotalSet();
        
       

    }//初始化   
    void Update()
    {
        MoveTotalControl();              
    }

    void StartTotalSet()
    {
        rb = gameObject.GetComponent<Rigidbody>();

        //设定敌我
        if (this.tag == "team1" || this.tag == "base1")
        { EnemyTeam1 = "team2"; EnemyTeam2 = "base2"; }
        if (this.tag == "team2" || this.tag == "base2")
        { EnemyTeam1 = "team1"; EnemyTeam2 = "base1"; }

        SetEnemy(base2);

    }
    void MoveTotalControl()
    {
        KeepRotation();
        if (MoveAllow_tem1 == 0)
            MoveAllow =0;
        else
            MoveAllow = MoveAllow_tem2;
        if (MoveAllow == 1)
            MoveTo();
        if (ShootAllow == 1)
            Shoot(shootcontrol);
        times_b--;
        times_bomb--;
        DeadConfirm();
        if (Enemy == null)
            SetEnemy(base2);
    }
    void Remove(int i)
    {
        if (i==1)//当前攻击对象已死亡 
        {
            MoveAllow_tem1 = 1;
                ShootAllow = 0;
                shootcontrol = 0;
                SetEnemy(base2);         
         }
    }//重新行动
    void MoveTo()
    {
        pos = Vector3.MoveTowards(this.transform.position, base2.transform.position, m_speed * Time.deltaTime);
        this.transform.position = pos;
    }//朝向目标前进
    //void MoveAdd()
    //{
    //    posAdd = Enemy.transform.position - this.transform.position;
    //    posAdd /= 70;

    //    pos = this.transform.position + posAdd;
    //    this.transform.position = pos;
    //}
    void KeepRotation()
    {
        Keep_Rotation = Quaternion.LookRotation(base2.transform.position - this.transform.position).eulerAngles;
        Keep_Rotation.x = 0;
        Keep_Rotation.z = 0;
        transform.localRotation = Quaternion.Euler(Keep_Rotation);
    } //保证只会水平旋转
    void Shoot(int shootcontrol)
    {
        if (shootcontrol == 0)
        {
            m_rocketRate -= Time.deltaTime;
            if (m_rocketRate <= 0)
            {
                m_rocketRate = 1f;
                shootpoint = new Vector3(this.transform.position.x,  this.transform.position.y + 0.5f,  this.transform.position.z);
                Instantiate(m_zidang,shootpoint, this.transform.rotation);
                
            }
        }      
    }//受shootcontrol控制的射击

    void OnTriggerExit(Collider other)
    {
        if (other.tag.CompareTo(EnemyTeam1) == 0)
        {
            Remove(1);
        }
    }

    void OnTriggerEnter(Collider other)
    {

        if (other.tag.CompareTo("NeutralTower") == 0)
        {

            NeutralTower tower = other.GetComponent<NeutralTower>();
            if (tower != null)
            {
                float x = tower.transform.position.x - this.transform.position.x;
                float z = tower.transform.position.z - this.transform.position.z;
                float sr = (float)Math.Sqrt(x * x + z * z);
                add = new Vector3(3 * x / sr, 0, 3 * z / sr);
                this.transform.position += add;



                pos = Vector3.MoveTowards(this.transform.position, tower.transform.position, m_speed * Time.deltaTime);
                this.transform.position = pos;
            }
            //进入的一瞬间不出去


        }







        if (other.tag.CompareTo("bullet") == 0)
        {
            zidang bullet = other.GetComponent<zidang>();
            if (bullet.Enemy == this.gameObject)//bullet.EnemyTeam1 == this.tag || bullet.EnemyTeam2 == this.tag||
                if (bullet != null)
                    m_life -= bullet.zidang_power;
        }

           
        
      

        
        



    }
    void OnTriggerStay(Collider other)
    {
        if (other.tag.CompareTo("NeutralTower") == 0)
        {
           
            NeutralTower tower = other.GetComponent<NeutralTower>();
            if(tower!=null)
            MoveAllow_tem2 = tower.CanMoveOrNot;//由中立塔给
           

        }


        





        if (other.tag.CompareTo(EnemyTeam1) == 0 || other.tag.CompareTo(EnemyTeam2) == 0)
        {

            if (Enemy == base2)
            {
                MoveAllow_tem1 = 0;   ////////////////////////////////MoveAllow_tem1 是由发现敌人得到的////////////////////////////////////////////////
                ShootAllow = 1;
                aim = other.transform.position;//锁定目标
                Enemy = other.gameObject;
            }
          
        }
        if (other.tag.CompareTo("DEAD_PEOPLE") == 0)//攻击对象死亡，停止射击
        {
             shootcontrol = 1;           
            if(time_1==0){
                time_1 = 1;
                StartCoroutine(WaitForTime(() => { Remove(1); time_1 = 0; }, 1.01f, false));               
        }
    }

        if (times_bomb <= 0)
        {
            if (other.tag.CompareTo("aoe") == 0)
            {

                aoe bullet = other.GetComponent<aoe>();

                //   Debug.Log("bullet.EnemyTag=" + iii);
                //Debug.Log("this.tag=" + this.tag);
                if (bullet.EnemyTag == this.tag)//bullet.EnemyTeam1 == this.tag || bullet.EnemyTeam2 == this.tag||
                    if (bullet != null)
                        m_life -= bullet.zidang_power;
                times_bomb = 10;
            }
        }
        

       
        if (m_life < 0)
        {
            this.tag = "DEAD_PEOPLE";

        }

    }  







    void SetEnemy(GameObject Enemy_1)
    {
        Enemy = Enemy_1;
    }//设置当前敌人    
    void DeadConfirm()
    {
        if (this.tag.CompareTo("DEAD_PEOPLE") ==0 ) 
          DeadConfirmNumber--;
        if(DeadConfirmNumber==0)       
             Destroy(this.gameObject);                     
    }//控制延时死亡
    void HowTimeDoOnce(Action function, float time, bool IsCycle)    {     
        StartCoroutine(WaitForTime(function, time, IsCycle));    }//方法，多久一次，是否循环
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
