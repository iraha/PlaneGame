using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{

    private GameObject player;

    [HideInInspector]
    public bool gameStarted;

    private bool cameraLockToPlayer;

    private Vector3 currentAngle;
    private int cameraType = 0;

    public Vector3 cameraMenuPosition = new Vector3(174f, 33f, -2314f);
    public float cameraTilt = 10f;

    private bool cameraEndScreen = false;

    // add player controller
    private PlayerController playerController;


    // Start is called before the first frame update
    void Start()
    {

        currentAngle = transform.eulerAngles;
        transform.position = cameraMenuPosition;

        player = GameObject.FindWithTag("Player");

        // get player controller
        playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        
    }

    // Update is called once per frame
    void Update()
    {
        SwitchCameraType();
    }

    void LateUpdate()
    {
        LerpCameraToGameStart();

        CameraFollowPlayer();
    }


    void LerpCameraToGameStart()
    {

        if (gameStarted)
        {
            transform.position = new Vector3(
            Mathf.Lerp(transform.position.x, player.transform.position.x, Time.deltaTime / 1.5f),
            Mathf.Lerp(transform.position.y, player.transform.position.y + 20f, Time.deltaTime / 1.5f),
            Mathf.Lerp(transform.position.z, player.transform.position.z - 80, Time.deltaTime / 1.5f)
            );

            currentAngle = new Vector3(
                Mathf.LerpAngle(currentAngle.x, cameraTilt, Time.deltaTime / 1.5f),
                Mathf.LerpAngle(currentAngle.y, 0f, Time.deltaTime / 1.5f),
                Mathf.LerpAngle(currentAngle.z, 0f, Time.deltaTime)
                );


            transform.eulerAngles = currentAngle;

            if (transform.position.x > player.transform.position.x - 0.5f)
            {
                gameStarted = false;
                cameraLockToPlayer = true;

                print("Stop Lerping");

                // call player take off animation
                playerController.StartTakeOff();
            }

        } // if gamestarted

    } // lerp the camera

    void CameraFollowPlayer()
    {

        if(cameraLockToPlayer && !cameraEndScreen)
        {

            if(cameraType == 0)
            {

                transform.position = new Vector3(
                    Mathf.Lerp(transform.position.x, player.transform.position.x, Time.deltaTime * 5f),
                    Mathf.Lerp(transform.position.y, player.transform.position.y + 20f, Time.deltaTime + 5f),
                    player.transform.position.z - 80f
                    );

                transform.eulerAngles = new Vector3(cameraTilt, 0f, 0f);

            } else
            {
                transform.position = new Vector3(250f, 90f, player.transform.position.z - 120f);

                transform.eulerAngles = new Vector3(0f, 0f, 0f);
            }

        }

    } // followr player

    void SwitchCameraType()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {

            if (cameraType == 0)
            {
                cameraType = 1;
            } else
            {
                cameraType = 0;
            }

        }
    }


} // class



















