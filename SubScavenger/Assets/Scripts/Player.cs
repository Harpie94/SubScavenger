using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{

    [SerializeField] float mouseSensitivity;
    [SerializeField] float movementSpeedSwim;
    [SerializeField] float movementSpeedDive;
    [SerializeField] float jumpSpeed;
    [SerializeField] float mass = 1f;
    [SerializeField] float diveDepth;
    [SerializeField] Transform cameraTransform;

    [SerializeField] Rigidbody rb;
    CharacterController controller;
    Vector3 velocity;
    Vector2 look;
    GameObject player;


    bool isUnderwater;

    PlayerInput playerInput;
    InputAction moveAction;
    InputAction lookAction;
    //InputAction jumpAction;
    InputAction diveAction;

    private Floater floatingControl;



    public enum State
    {
        Swiming,
        Diving,
        Walking
    }

    public State state;

    void Awake()
    {

        rb = gameObject.GetComponent<Rigidbody>();
        controller = gameObject.GetComponent<CharacterController>();
        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions["move"];
        lookAction = playerInput.actions["look"];
        //jumpAction = playerInput.actions["jump"];
        diveAction = playerInput.actions["dive"];
        floatingControl = gameObject.GetComponent<Floater>();
        
    }
    void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateMovement();
        UpdateLook();
    }

    void UpdateMovement()
    {
        if (gameObject.transform.position.y < -diveDepth)
        {
            state = State.Diving;
            isUnderwater = true;
            controller.enabled = true;
            floatingControl.enabled = false;
        }
        else
        {
            state = State.Swiming;
            isUnderwater = false;
            controller.enabled = false;
            floatingControl.enabled = true;
        }
        switch (state)
        {
            case State.Swiming:
                var moveInputSwim = moveAction.ReadValue<Vector2>();
                var yInputSwim = diveAction.ReadValue<float>();
                var inputSwim = new Vector3();
                inputSwim += transform.forward * moveInputSwim.y;
                inputSwim += transform.right * moveInputSwim.x;
                if (yInputSwim == -1f)
                {
                    floatingControl.enabled = false;
                    rb.AddForce(0,-diveDepth * Time.deltaTime,0);
                }
                else
                {
                    floatingControl.enabled = true;
                }
                inputSwim = Vector3.ClampMagnitude(inputSwim, 1f);
                rb.AddForce((inputSwim * movementSpeedSwim + velocity) * Time.deltaTime);

                break;
            case State.Diving:
                var moveInputDive = moveAction.ReadValue<Vector2>();
                var yInputDive = diveAction.ReadValue<float>();
                var inputDive = new Vector3();
                inputDive += transform.forward * moveInputDive.y;
                inputDive += transform.right * moveInputDive.x;
                inputDive += transform.up * yInputDive;
                inputDive = Vector3.ClampMagnitude(inputDive, 1f);

                //var jumpInput = jumpAction.ReadValue<float>();
                //Jump Logic oustide of water



                controller.Move((inputDive * movementSpeedDive + velocity) * Time.deltaTime);      
                break;
        }

    }


    void UpdateLook()
    {
        var lookInput = lookAction.ReadValue<Vector2>();
        look.x += lookInput.x * mouseSensitivity;
        look.y += lookInput.y * mouseSensitivity;
        look.y = Mathf.Clamp(look.y, -90f, 90f);

        cameraTransform.localRotation = Quaternion.Euler(-look.y, 0, 0);
        transform.localRotation = Quaternion.Euler(0, look.x, 0);
    }
}
