using UnityEngine;
using System.Collections;

public class player : MonoBehaviour {

    public Transform m_zidang;





	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0))
        {
            Instantiate(m_zidang, transform.position, transform.rotation);
        }
        
	}
}
