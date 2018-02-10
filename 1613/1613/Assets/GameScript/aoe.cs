using UnityEngine;
using System.Collections;

public class aoe : MonoBehaviour
{
    public string EnemyTag;
    float NumberTime = 10;
    public float zidang_power = 1;

    protected Transform m_transform;
    public float zidang_speed = 50;
    public float zidang_liveTime = 1;
    Vector3 pos;
    Vector3 GoTo = new Vector3();  
    GameObject Enemy = new GameObject();
    public string EnemyTeam1;//敌人的士兵
    public string EnemyTeam2;//敌人的防御塔

    void Start()
    {
        m_transform = this.transform;
        Destroy(this.gameObject, zidang_liveTime);
        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);

    }

    
    void Update()
    {
        if (NumberTime >= 0)
            NumberTime--;
        pos = Vector3.MoveTowards(this.transform.position, GoTo, zidang_speed * Time.deltaTime);

        this.transform.position = pos;
        if (this.tag == "aoe")
        {
            StartCoroutine(Func_die(0.3f));
        }
      
    }

    void OnTriggerEnter(Collider other)//意义何在
    {

        if (other.tag.CompareTo(EnemyTag) == 0)
        {


            StartCoroutine(Func_tag(0.35f));


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
            move owner = null;
            if (owner == null)
                owner = other.GetComponent<move>();
            if (owner != null)
            {
                Enemy = owner.Enemy;
                GoTo = Enemy.transform.position;
                EnemyTag = Enemy.tag;

            }
           
          
       
           
        }
        if (NumberTime == -1)
            if(Enemy!=null)
            GoTo = Enemy.transform.position;
    }


    IEnumerator Func_die(float time)
    {

        yield return new WaitForSeconds(time);
        Destroy(this.gameObject);

    }
    IEnumerator Func_tag(float time)
    {

        yield return new WaitForSeconds(time);
        this.tag = "aoe";


    }
}
