using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Propellor : MonoBehaviour
{

    private bool rotating = true;



    // Update is called once per frame
    void Update()
    {

        if (rotating)
        {
            transform.Rotate(Vector3.right * 40);
        }
    }

    public void PauseRotation()
    {
        rotating = false;
    }

    public void ResumeRotation()
    {
        rotating = true;
    }
}
