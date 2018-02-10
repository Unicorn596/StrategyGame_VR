using UnityEngine;
using System.Collections;



[AddComponentMenu("MyGame/Rocket")]
public class zidang : MonoBehaviour
{
    public GameObject target;
    protected Transform m_transform;
    public float zidang_speed = 10;
    public float zidang_liveTime = 0.3f;
    public float zidang_power = 1;
    public Vector3 pos;
    public Vector3 GoTo = new Vector3();


    public string EnemyTag;
    float NumberTime=10;
   public GameObject Enemy =new GameObject();
    public string EnemyTeam1;//敌人的士兵
    public string EnemyTeam2;//敌人的防御塔


    void Start()
    {
        m_transform = this.transform;
        Destroy(this.gameObject, zidang_liveTime);
        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
        
    }

    // Update is called once per frame
    void Update()
    {      
        if (NumberTime >= 0)
            NumberTime--;   
        pos = Vector3.MoveTowards(this.transform.position, GoTo, zidang_speed * Time.deltaTime);
        this.transform.position = pos;
    }

    void OnTriggerEnter(Collider other)//意义何在
    {
        
        if (other.tag.CompareTo(EnemyTag) == 0)
        {
           // Debug.Log("子弹碰撞塔成功");
            return;
        }

       
    }

    void OnTriggerStay(Collider other)
    {


        if (NumberTime >= 0&&NumberTime<8)//子弹生成出来后0.04秒到0.20秒之间，通过与自己相碰的物体的tag来设置敌人的标签
        {
            if (other.tag == "team1" || other.tag == "base1")
            { EnemyTeam1 = "team2"; EnemyTeam2 = "base2"; }
            if (other.tag == "team2" || other.tag == "base2")
            { EnemyTeam1 = "team1"; EnemyTeam2 = "base1"; }
            //设置敌人
            move owner = null;
            if (owner == null)
            {
                owner = other.GetComponent<move>();
               
            }
               
            if (owner != null)
            {
               //改了//////////////////////////////////////////////
                //Enemy = owner.Enemy;
                //GoTo = Enemy.transform.position;
                //EnemyTag = Enemy.tag;
                if (owner.Enemy != null)
                {
                    Enemy = owner.Enemy;
                    GoTo = Enemy.transform.position;
                    EnemyTag = Enemy.tag;
                }
                ////////////////////////////////////////////////
                
            }
            
            
            
           
           
        }
        if(other.tag.CompareTo("DEAD_PEOPLE")==0)
            Destroy(this.gameObject);
    }
    
    

}


