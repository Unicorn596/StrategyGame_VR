using UnityEngine;
using System.Collections;

public class wall : MonoBehaviour
{
   
    public float m_life = 5;
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
            if (bullet != null)
            {
                m_life -= bullet.zidang_power;
                if (m_life <= 0)
                {
                  
                    Destroy(this.gameObject);
                }
            }
        }
        Debug.Log(m_life);
    }
}
