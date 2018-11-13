using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.Networking;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody))]
public class PlayerController : NetworkBehaviour {

    private static float WALK_FORWARD = 3.1f;
    private static float WALK_SIDEWARD = 2.1f;
    private static float RUN_FORWARD = 5.1f;
    private static float RUN_SIDEWARD = 3.1f;
    private static float SPRINT_FORWARD = 8.1f;
    private static float SPRINT_SIDEWARD = 5.1f;

    private static String animForward = "forwardSpeed";
    private static String animSide = "rightSpeed";
    private static String animDuck = "duck";
    private static String animCrawl = "crawl";
    private static String animTriggered = "triggered";

    [Header("Movement Variables")]
    [SerializeField]
    private float walkingForwardSpeed = 3.0f;
    [SerializeField]
    private float walkingSideSpeed = 2.0f;
    [SerializeField]
    private float runningForwardSpeed = 5.0f;
    [SerializeField]
    private float runningSideSpeed = 3.0f;
    [SerializeField]
    private float sprintingForwardSpeed = 8.0f;
    [SerializeField]
    private float sprintingSideSpeed = 5.0f;
    [SerializeField]
    private float sensitivityX = 3f;
    [Header("Camera Position Variables")]
    [SerializeField]
    private float cameraDistance = 8f;
    [SerializeField]
    private float cameraHeight = 4f;

    private Rigidbody localRigidBody;
    private Transform mainCamera;
    private Vector3 cameraOffset;
    private Animator animator;
    private float rotationX;

    private bool looking = false;

    private static KeyCode RUN_KEY = KeyCode.LeftShift;
    private static KeyCode SPRINT_KEY = KeyCode.Q;
    private static KeyCode DUCK_KEY = KeyCode.LeftControl;

    private bool running = false;
    private bool sprinting = false;
    private bool ducking = false;
    private bool triggered = false;
    private bool crawling = false;


    private float sideFactor;
    private float forwardFactor;

    [Header("Camera Smooth Variables")]
    [SerializeField]
    private float smoothTime = 0.3F;
    private Vector3 velocity = Vector3.zero;

    void Start () {
        if (!isLocalPlayer)
        {
            Destroy(this);
            return;
        }

        localRigidBody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        cameraOffset = new Vector3(0f, cameraHeight, -cameraDistance);
        mainCamera = Camera.main.transform;
        MoveCamera();
	}

    // Update is called once per frame
    void Update () {

        CheckSpeedValues();
    }

    private void CheckSpeedValues()
    {
        running = Input.GetKey(RUN_KEY);
        sprinting = Input.GetKey(SPRINT_KEY);
        ducking = Input.GetKey(DUCK_KEY);
      
        if (!ducking && sprinting)
        {
            forwardFactor = sprintingForwardSpeed;
            sideFactor = sprintingSideSpeed;
        }
        else
        if (!ducking && running)
        {
            forwardFactor = runningForwardSpeed;
            sideFactor = runningSideSpeed;
        }
        else
        {
            forwardFactor = walkingForwardSpeed;
            sideFactor = walkingSideSpeed;
        }

    }

    private void FixedUpdate()
    {
        
        float horizontalAmount = CrossPlatformInputManager.GetAxis("Horizontal");
        float verticalAmount = CrossPlatformInputManager.GetAxis("Vertical");

        float forwardSpeed = forwardFactor * verticalAmount;
        float sideSpeed = sideFactor * horizontalAmount;

        Vector3 deltaTranslation = transform.position + 
            transform.forward * forwardSpeed * Time.deltaTime + 
            transform.right * sideSpeed * Time.deltaTime;
        localRigidBody.MovePosition(deltaTranslation);

        UpdateAnimator(forwardSpeed, sideSpeed);

        if (Input.GetMouseButton(1))
        {
            Look();
        }
        MoveCamera();
    }

    private void UpdateAnimator(float forwardSpeed, float sideSpeed)
    {
        float buffer = 0.3f;

        animator.SetBool(animDuck, ducking);
        animator.SetBool(animTriggered, triggered);
        animator.SetBool(animCrawl, crawling);

        #region updateForwardAnimator
        if (forwardSpeed >= walkingForwardSpeed - buffer)
        {
            if (forwardSpeed >= runningForwardSpeed - buffer)
            {
                if (forwardSpeed >= sprintingForwardSpeed - buffer)
                {
                    //Sprint Forward
                    animator.SetFloat(animForward, SPRINT_FORWARD);
                }
                else
                {
                    //Run Forward
                    animator.SetFloat(animForward, RUN_FORWARD);
                }
            }
            else
            {
                //Walk Forward
                animator.SetFloat(animForward, WALK_FORWARD);
            }
        }else if(forwardSpeed <= -walkingForwardSpeed + buffer)
        {
            if (forwardSpeed <= -runningForwardSpeed + buffer)
            {
                if (forwardSpeed <= -sprintingForwardSpeed + buffer)
                {
                    //Sprint Backward
                    animator.SetFloat(animForward, -SPRINT_FORWARD);
                }
                else
                {
                    //Run Backward
                    animator.SetFloat(animForward, -RUN_FORWARD);
                }
            }
            else
            {
                //Walk Backward
                animator.SetFloat(animForward, -WALK_FORWARD);
            }
        }else
        {
            animator.SetFloat(animForward, 0);
        }
        #endregion

        #region updateSideAnimator
        if (sideSpeed >= walkingSideSpeed - buffer)
        {
            if (sideSpeed >= runningSideSpeed - buffer)
            {
                if (sideSpeed >= sprintingSideSpeed - buffer)
                {
                    //Sprint Right
                    animator.SetFloat(animSide, SPRINT_SIDEWARD);
                }
                else
                {
                    //Run Right
                    animator.SetFloat(animSide, RUN_SIDEWARD);
                }
            }
            else
            {
                //Walk Right
                animator.SetFloat(animSide, WALK_SIDEWARD);
            }
        }
        else if (sideSpeed <= -walkingSideSpeed + buffer)
        {
            if (sideSpeed <= -runningSideSpeed + buffer)
            {
                if (sideSpeed <= -sprintingSideSpeed + buffer)
                {
                    //Sprint Left
                    animator.SetFloat(animSide, -SPRINT_SIDEWARD);
                }
                else
                {
                    //Run Left
                    animator.SetFloat(animSide, -RUN_SIDEWARD);
                }
            }
            else
            {
                //Walk Left
                animator.SetFloat(animSide, -WALK_SIDEWARD);
            }
        }else
        {
            animator.SetFloat(animSide, 0);
        }
        #endregion
    }

    private void Look()
    {
        rotationX += Input.GetAxis("Mouse X") * sensitivityX;
        localRigidBody.MoveRotation( Quaternion.Euler(localRigidBody.rotation.eulerAngles.x, rotationX, localRigidBody.rotation.eulerAngles.z));
    }

    private void MoveCamera()
    {
        mainCamera.position = transform.position;
        mainCamera.rotation = transform.rotation;
        mainCamera.Translate(cameraOffset);
        mainCamera.LookAt(transform);
    }
}
