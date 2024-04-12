using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour //MonoBehaviour is inbuilt parent class here
{
    // Start is called before the first frame update
    Vector2 look; //This a data type
    [SerializeField] Transform cameraTransform; //Transform is a data type. 
    [SerializeField] float movement = 5f;
    [SerializeField] float mouseSensitivity = 3f;
    [SerializeField] float mass = 1f;
    [SerializeField] float jumSpeed = 5f;
    CharacterController characterController;
    Vector3 velocity;

    //Serialized fields variable goes to the unity editor

    void Awake()
    {
        characterController = GetComponent<CharacterController>(); //get component means we need to add from the add component in inspector.
    }
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; //removes the cursor from the screen
    }

    // Update is called once per frame
    void Update() //acts like main method
    {
        UpdateGravity();
        UpdateMovement();
        UpdateLook();



    }

    void UpdateGravity()
    {
        var gravity = Physics.gravity * mass * Time.deltaTime;

        velocity.y = characterController.isGrounded ? -1f : velocity.y + gravity.y;
    }

    void UpdateMovement()
    {
        var x = Input.GetAxis("Horizontal");
        var y = Input.GetAxis("Vertical");
        var input = new Vector3();
        input += transform.forward * y; //Tansform is builtin class. moving forward movemenent
        input += transform.right * x; //controls horizontal movement

        input = Vector3.ClampMagnitude(input, 1f); //clamp restricts the movement. 

        if (Input.GetButtonDown("Jump") && characterController.isGrounded)
        {
            velocity.y = jumSpeed;
        }
        //transform.Translate(input * movement * Time.deltaTime, Space.World);
        characterController.Move((input * movement + velocity) * Time.deltaTime);
    }

    void UpdateLook()
    {
        look.x += Input.GetAxis("Mouse X") * mouseSensitivity; //Horizontal look and mouse sensititvity
        look.y += Input.GetAxis("Mouse Y") * mouseSensitivity; //vertical look
        look.y = Mathf.Clamp(look.y, -89f, 89f); //doesn't let you move 360 and limits upto the given number
        cameraTransform.localRotation = Quaternion.Euler(-look.y, 0, 0); //rotates your camera anticlock wise only camera not the character. vertical
        transform.localRotation = Quaternion.Euler(0, look.x, 0); //rotates in x axis in horizontal

    }
}

//x and y axis is the property of the vector inbulit class and look.x is accessing the property of the vector class.
