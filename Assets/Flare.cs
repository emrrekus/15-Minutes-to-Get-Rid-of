using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flare : MonoBehaviour
{
    public Animator pl;
    public Animator pl2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            pl.SetTrigger("door");
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            pl2.SetTrigger("sit");
        }
    }

    void ShootFlare()
    {

    }
}
