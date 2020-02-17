using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    Animation anim;

    private Camera mainCamera;

    private Rigidbody myBody;

    public float forwardVelocity = 100f;
    public float minmumSpeed = -1500f;

    public float maxHorizontalSpeed = 250f;
    private float current_HorizontalSpeed = 0f;

    public float maxVerticalSpeed = 250f;
    private float current_VerticalSpeed = 0f;

    private float currentRotation = 0f;
    private Vector3 currentAngle;

    public float left_BorderLimitx = 130f;
    public float right_BorderLimitx = 370f;
    public float vertical_UpperLimit = 160f;
    public float vertical_LowerLimit = 40f;

    public float bonus_HorizontalSpeed = 0f;
    public float boost_HorizontalSpeed = 0f;

    public float startSpeed = 40f;

    [HideInInspector]

    public bool moving = false;

    private Vector3 storedVelocity;

    private bool speed_Boosted = false;
    private int speed_BoostValue = 100;
    private float speed_BoostTimeout = 5f;
    private float speed_BoostTimer = 0f;




    void Awake()
    {

        anim = GetComponent<Animation>();

        myBody = GetComponent<Rigidbody>();

        myBody.isKinematic = true;

        currentAngle = myBody.transform.eulerAngles;

        mainCamera = Camera.main;

        storedVelocity = new Vector3(0f, 0f, startSpeed);



    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {

        if (moving)
        {
            myBody.velocity = new Vector3(current_HorizontalSpeed, current_VerticalSpeed, myBody.velocity.z);
            myBody.transform.eulerAngles = currentAngle;
        }

    }


    public void StartTakeOff()
    {
        anim.Play("TakeOff");
    }

    public void Resume()
    {

        myBody.velocity = storedVelocity;
        myBody.isKinematic = false;

        moving = true;

        BoxCollider[] boxColls = GetComponents<BoxCollider>();

        foreach(var b in boxColls)
        {
            b.enabled = true;
        }

    }



} //class



