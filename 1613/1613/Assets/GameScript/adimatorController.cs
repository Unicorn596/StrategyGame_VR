using UnityEngine;
using System.Collections;

public class adimatorController : MonoBehaviour
{

    private Animator solider;
    public move a;

    // Use this for initialization
    void Start()
    {
        a = this.gameObject.GetComponent<move>();
        solider = this.gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (a.MoveAllow == 1)
            solider.SetBool("move", true);
        if (a.MoveAllow != 1)
            solider.SetBool("move", false);
    }
}