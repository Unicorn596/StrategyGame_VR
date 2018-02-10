using UnityEngine;
using System.Collections;

public class base1 : MonoBehaviour
{
    //士兵变量
    public Transform solider1;
    public Transform solider2;
    public Transform solider3;
    public Transform solider1_1;
    public Transform solider1_2;
    public Transform solider2_1;
    public Transform solider2_2;
    float m_soliderRate = 0;
    //鼠标点击变量
    protected Vector3 m_targetPos;
    public LayerMask m_inputMask;
    //生命值
    public float m_life = 220;
    //士兵能量点
    public int SoliderPoint,sp;
    //创建士兵标识符
    public Transform SoliderClass;
    string soliderclass;
    
    
    void Start()
    {
        //SoliderPoint = 100;
        SoliderClass = solider1;
        sp = 5;
        InvokeRepeating("AddSoliderPoint", 2, 1.5F);
        if (this.tag == "base1")
            soliderclass = "team1";
        else
            soliderclass = "team2";
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 ms = Input.mousePosition;
            Ray ray = Camera.main.ScreenPointToRay(ms);
            RaycastHit hitinfo;
            bool iscast = Physics.Raycast(ray, out hitinfo, 1000, m_inputMask);
            if (iscast)
            {
                m_targetPos = hitinfo.point;
                CreateSolider(m_targetPos, SoliderClass, sp);
            }
           
        }
        //solider point add

    }

    void CreateSolider(Vector3 targetPos, Transform soliderClass, int sp)
    {
        
        if (SoliderPoint >= sp)
        {
            SoliderPoint -= sp;
            m_soliderRate -= Time.deltaTime;
            if (m_soliderRate <= 0)
            {
                m_soliderRate = 0.1f;
            }
            targetPos.y -= 10; 
            //GameObject GO=(GameObject)
                Instantiate(soliderClass, targetPos, this.transform.rotation);
          //GO.transform.tag = soliderclass;
            //  GO.tag = soliderclass;
            //Debug.Log("tag=====" + soliderclass);
           // GO.gameObject.tag = soliderclass;
            
        }
    }

    void OnTriggerEnter(Collider other)
    {
        //.Log(m_life + " TEST ");
        if (other.tag.CompareTo("bullet") == 0)
        {
            zidang bullet = other.GetComponent<zidang>();
            aoe boomb = other.GetComponent<aoe>();
            if (bullet != null)
            {
                m_life -= bullet.zidang_power;

            }
            if (boomb != null)
            {
                m_life -= boomb.zidang_power;
            }
        }
        if (m_life <= 0)
        {
            Debug.Log(m_life + " destory ");
            Destroy(this.gameObject);
        }
        //Debug.Log(m_life);
        // Debug.Log(SoliderPoint + " SP ");
    }


    void OnTriggerStay(Collider other)
    {
        if(other.tag.CompareTo("aoe")==0)
        {
            Debug.Log("着弹确认");
            aoe bomb = other.GetComponent<aoe>();
            if(bomb != null)
            {
                m_life -= bomb.zidang_power;
            }
            if (m_life <= 0)
            {
                Debug.Log(m_life + " destory ");
                Destroy(this.gameObject);
            }
        }
    }





    void AddSoliderPoint()
    {
        SoliderPoint += 1;
    }






    void OnGUI()//xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
    {
        GUI.Label(new Rect(Screen.width * 0.5f + 250, Screen.height * 0.95f, 600, 500), " sp =5");
        GUI.Label(new Rect(Screen.width * 0.5f + 100, Screen.height * 0.95f, 600, 500), " sp =10");
        if (GUI.Button(new Rect(Screen.width * 0.5f + 250, Screen.height * 0.9f, 100, 50), "Solider"))
        {
            SoliderClass = solider1;
            sp = 5;
        }

        if (GUI.Button(new Rect(Screen.width * 0.5f + 100, Screen.height * 0.9f, 100, 50), "Lancer"))
        {
            SoliderClass = solider2;
            sp = 10;
        }
       //if(GUI.Button(new Rect(Screen.width * 0.5f + 150, Screen.height * 0.8f, 50, 30), "左中立塔"))
       //{

       //} 
       // if (GUI.Button(new Rect(Screen.width * 0.5f + 100, Screen.height * 0.8f, 50, 30), "右中立塔"))
       //{

       //}
    }
}
