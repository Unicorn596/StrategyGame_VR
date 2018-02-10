using UnityEngine;
using System.Collections;

public class shangxiazuoyou : MonoBehaviour {
    //public Rigidbody rb;
    //public float m_speed = 1;
    //float TranslateSpeed = 10;
    //float zuoyou = 1;
    //float m_rocketRate = 0;
    //// Use this for initialization
    //public Transform m_zidang;
    //public Transform n_zidang;
    
    //protected Transform m_transform;
    
    //Vector3 position = new Vector3();
    //Quaternion ShootDire;
    //public Transform target;
    ////目标位置
    //protected Vector3 m_targetPos;
    ////鼠标射线碰撞层
    //public LayerMask m_inputMask;
    




   // Vector3 rotation = new Vector3();
    //Quaternion Rotation = new Quaternion();
    
//    void Start () {
//        rb = gameObject.GetComponent<Rigidbody>();
//        m_transform = this.transform;
//        m_targetPos = this.m_transform.position;
//       // rotation = rb.transform.localEulerAngles;
//      //  rotation.x = -rotation.x;
//      //  rotation.y = -rotation.y;
//      //  rotation.z = -rotation.z;
//      //  Rotation =              Quaternion.Euler(rotation);
       


//    }
	
//    // Update is called once per frame
//    void FixedUpdate () {
        
//        if (Input.GetKey(KeyCode.UpArrow))
//        {
//            Debug.Log("您按下了↓键");
//            transform.Translate(Vector3.down * m_speed * Time.deltaTime * (-TranslateSpeed)); 
//        }
//        if (Input.GetKey(KeyCode.DownArrow))
//        {
//            Debug.Log("您按下了↑键");
//            transform.Translate(Vector3.down *m_speed* Time.deltaTime * TranslateSpeed);
//        }
//        if (Input.GetKey(KeyCode.LeftArrow))
//        {
//            Debug.Log("您按下了←键");
//            transform.Translate(Vector3.right * m_speed * Time.deltaTime * (TranslateSpeed));
//            zuoyou = 0;
//        }
//        if (Input.GetKey(KeyCode.RightArrow))
//        {
//            Debug.Log("您按下了→键");
//            transform.Translate(Vector3.right *m_speed* Time.deltaTime * (-TranslateSpeed));
//            zuoyou = 1;
//        }
      

//    }

//    void Update()
//    {
//        m_rocketRate -= Time.deltaTime;
//        if (m_rocketRate <= 0)
//        {
//            m_rocketRate = 0.1f;
//            if (Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0))
//            {
//                //计算两者角度
//                ShootDire = this.transform.rotation;
//                ShootDire = new Quaternion(ShootDire.x, ShootDire.y, ShootDire.z, ShootDire.w);
//                ShootDire.SetLookRotation( target.position,this.position);
//                ShootDire = new Quaternion(ShootDire.x, ShootDire.w, ShootDire.z, ShootDire.y);
//                //获取位置参数
//                position = this.transform.position;
//                position = new Vector3(position.x+5, position.y, position.z);
//                //生成子弹
//                Instantiate(m_zidang, position, ShootDire);
//                Debug.Log("tr="+transform.rotation);
//                Debug.Log("sd=" + ShootDire);

//            }
//        }
//    }

//    void shoot()
//    {
//        if (Input.GetMouseButton(0))
//        {
//            Vector3 ms = Input.mousePosition;
//            Ray ray = Camera.main.ScreenPointToRay(ms);
//            RaycastHit hitinfo;
//            bool iscast = Physics.Raycast(ray, out hitinfo, 1000, m_inputMask);
//            if (iscast)
//            {
//                m_targetPos = hitinfo.point;
//            }
//        }
//    }

}
