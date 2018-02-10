using UnityEngine;
using System.Collections;

public class CameraFlow : MonoBehaviour
{
    public Transform target;
    private Vector3 offset;
    // Use this for initialization
    void Start()
    {
       

    }

    // Update is called once per frame
    void Update()
    {
        offset = target.position - this.transform.position;
        this.transform.position = target.position - offset;
    }
}