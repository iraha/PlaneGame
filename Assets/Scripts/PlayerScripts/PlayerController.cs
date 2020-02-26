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
        CheckPlayerBorders();
        CheckControlInput();
        KeyboardInput();
    }

    void FixedUpdate()
    {

        if (moving)
        {
            myBody.velocity = new Vector3(current_HorizontalSpeed, current_VerticalSpeed, myBody.velocity.z);
            myBody.transform.eulerAngles = currentAngle;
        }

    }

    void MoveRight()
    {
        currentAngle = new Vector3(
            Mathf.LerpAngle(currentAngle.x, 0f, Time.deltaTime),
            Mathf.LerpAngle(currentAngle.y, 0f, Time.deltaTime),
            Mathf.LerpAngle(currentAngle.z, -70f, Time.deltaTime)
            );

        current_HorizontalSpeed = Mathf.Lerp(current_HorizontalSpeed,
            maxHorizontalSpeed + bonus_HorizontalSpeed + boost_HorizontalSpeed, Time.deltaTime);

    } // move right

    void MoveLeft()
    {
        currentAngle = new Vector3(
            Mathf.LerpAngle(currentAngle.x, 0f, Time.deltaTime),
            Mathf.LerpAngle(currentAngle.y, 0f, Time.deltaTime),
            Mathf.LerpAngle(currentAngle.z, 70f, Time.deltaTime)
            );

        current_HorizontalSpeed = Mathf.Lerp(current_HorizontalSpeed,
            -maxHorizontalSpeed + -bonus_HorizontalSpeed + -boost_HorizontalSpeed, Time.deltaTime);

    } // move Left

    void MoveUp()
    {
        currentAngle = new Vector3(
            Mathf.LerpAngle(currentAngle.x, -35f, Time.deltaTime),
            currentAngle.y,
            currentAngle.z
            );

        current_VerticalSpeed = Mathf.Lerp(current_VerticalSpeed, maxVerticalSpeed, Time.deltaTime / 2f);

    } // moveUp

    void MoveDown()
    {
        currentAngle = new Vector3(
            Mathf.LerpAngle(currentAngle.x, 35f, Time.deltaTime),
            currentAngle.y,
            currentAngle.z
            );

        current_VerticalSpeed = Mathf.Lerp(current_VerticalSpeed, -maxVerticalSpeed, Time.deltaTime / 2f);

    } // moveDown


    void KeyboardInput()
    {

        if(moving)
        {
            if(Input.GetKey(KeyCode.A))
            {
                MoveLeft();
            }
            if(Input.GetKey(KeyCode.D))
            {
                MoveRight();
            }
            if(Input.GetKey(KeyCode.W))
            {
                MoveUp();
            }
            if(Input.GetKey(KeyCode.S))
            {
                MoveDown();
            }
        }

    }


    void CheckControlInput()
    {

        if (moving)
        {
            if (!Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))
            {
                current_HorizontalSpeed = Mathf.Lerp(current_HorizontalSpeed, 0f, Time.deltaTime / 0.3f);

                currentAngle = new Vector3(
                    Mathf.LerpAngle(currentAngle.x, currentAngle.x, Time.deltaTime),
                    Mathf.LerpAngle(currentAngle.y, 0f, Time.deltaTime),
                    Mathf.LerpAngle(currentAngle.z, 0f, Time.deltaTime * 2f)
                    );
            }

            if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S))
            {
                current_VerticalSpeed = Mathf.Lerp(current_VerticalSpeed, 0f, Time.deltaTime / 0.3f);

                currentAngle = new Vector3(
                    Mathf.LerpAngle(currentAngle.x, 0f, Time.deltaTime * 2f),
                    Mathf.LerpAngle(currentAngle.y, 0f, Time.deltaTime),
                    Mathf.LerpAngle(currentAngle.z, currentAngle.z, Time.deltaTime)
                    );
            }


        }

    } // Control Input

    void CheckPlayerBorders()
    {

        if (moving)
        {
            if(transform.position.y > vertical_UpperLimit)
            {
                transform.position = new Vector3(transform.position.x, vertical_UpperLimit - 1, transform.position.z);

                current_VerticalSpeed = 0f;
                Input.ResetInputAxes();

            }

            if (transform.position.y < vertical_LowerLimit)
            {
                transform.position = new Vector3(transform.position.x, vertical_LowerLimit + 1, transform.position.z);

                current_VerticalSpeed = 0f;
                Input.ResetInputAxes();

            }

            if (transform.position.x < left_BorderLimitx)
            {
                transform.position = new Vector3(left_BorderLimitx + 1, transform.position.y, transform.position.z);

                current_HorizontalSpeed = 0f;
                currentAngle = new Vector3(
                    Mathf.LerpAngle(currentAngle.x, 0f, Time.deltaTime),
                    Mathf.LerpAngle(currentAngle.y, 0f, Time.deltaTime),
                    Mathf.LerpAngle(currentAngle.z, 0f, Time.deltaTime * 2f)
                    );
                Input.ResetInputAxes();

            }


            if (transform.position.x > right_BorderLimitx)
            {
                transform.position = new Vector3(right_BorderLimitx - 1, transform.position.y, transform.position.z);

                current_HorizontalSpeed = 0f;
                currentAngle = new Vector3(
                    Mathf.LerpAngle(currentAngle.x, 0f, Time.deltaTime),
                    Mathf.LerpAngle(currentAngle.y, 0f, Time.deltaTime),
                    Mathf.LerpAngle(currentAngle.z, 0f, Time.deltaTime * 2f)
                    );
                Input.ResetInputAxes();

            }
        }

    } // Player border




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



