using UnityEngine;
using System.Collections;

public class sbAI : MonoBehaviour
{
    public Transform solider1;
    public Transform solider2;
    float m_soliderRate = 0;
    public float m_life = 10;
    //士兵能量点
    public int SoliderPoint, sp;
    //创建士兵标识符
    public Transform SoliderClass;
    // Use this for initialization
    void Start()
    {
        SoliderPoint = 20;
        SoliderClass = solider1;
        sp = 5;
        InvokeRepeating("AddSoliderPoint", 2, 1.5F);
    }

    // Update is called once per frame
    //
    //
    //
    //
    //
    //
    //
    //
    //Update修改了自动生成士兵的范围
    void Update()
    {
        if (SoliderPoint >= 5)
        {
            int n = Random.Range(0, 1);
            if (n == 1)
            {
                Vector3 RanPos;
                RanPos.x = Random.Range(60, 240);
                RanPos.y = 66;
                RanPos.z = Random.Range(40, 190);
                CreateSolider(RanPos, solider1, 5);
            }
            if (n == 0 && SoliderPoint >= 10)
            {
                Vector3 RanPos;
                RanPos.x = Random.Range(10, 260);
                RanPos.y = 46;
                RanPos.z = Random.Range(20, 220);
                CreateSolider(RanPos, solider2, 10);
            }
        }
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
            Instantiate(soliderClass, targetPos, this.transform.rotation);

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
        if (other.tag.CompareTo("aoe") == 0)
        {
            Debug.Log("着弹确认");
            aoe bomb = other.GetComponent<aoe>();
            if (bomb != null)
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
}
