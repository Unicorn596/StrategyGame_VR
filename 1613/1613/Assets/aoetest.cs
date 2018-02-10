using UnityEngine;
using System.Collections;

public class AOEtest : MonoBehaviour
{
    //生命值
    public float m_life = 2;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log(m_life + " TEST ");
        if (other.tag.CompareTo("bullet") == 0)
        {
            zidang bullet = other.GetComponent<zidang>();
            bomb boomb = other.GetComponent<bomb>();
            if (bullet != null)
            {
                m_life -= bullet.zidang_power;

            }
            if (boomb != null)
            {
             //   m_life -= boomb.zidang_power;
            }
        }
        if (m_life <= 0)
        {
            Debug.Log(m_life + " destory ");
            Destroy(this.gameObject);
        }
        Debug.Log(m_life);
        //Debug.Log(SoliderPoint + " SP ");
    }
}
