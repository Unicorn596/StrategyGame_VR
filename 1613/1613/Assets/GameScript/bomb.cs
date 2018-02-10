using UnityEngine;
using System.Collections;



[AddComponentMenu("MyGame/Rocket")]
public class bomb : MonoBehaviour
{
    //public GameObject target;
    protected Transform m_transform;
    public GameObject aoe;
    public float zidang_speed = 50;
    public float zidang_liveTime = 1;
    public Vector3 pos;
    public Vector3 GoTo = new Vector3();


   
    
 

    public string EnemyTag;
    float NumberTime=10;
    GameObject Enemy =new GameObject();
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
      //  Debug.Log("bomb.EnemyTag=" + EnemyTag);
    }
    
    void OnTriggerEnter(Collider other)//意义何在
    {
        
        if (other.tag.CompareTo(EnemyTag) == 0)
        {
            Vector3 pos = this.transform.position;
           
            Instantiate(aoe, pos, this.transform.rotation);
           // Debug.Log("炸弹销毁");
            StartCoroutine(Func_time(0.06f));

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

            move owner=null;
            if(owner ==null)
            owner = other.GetComponent<move>();
            if (owner != null)
            {
                Enemy = owner.Enemy;
                GoTo = Enemy.transform.position;
                EnemyTag = Enemy.tag;
              //  Debug.Log("tag" + EnemyTag);
            }
           
           
        }
        
    }
   
    IEnumerator Func_time(float time)
    {

        yield return new WaitForSeconds(time);
        Destroy(this.gameObject);

    }

}


