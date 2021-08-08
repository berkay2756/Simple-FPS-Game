using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float hrznAxis;
    private float vrtcAXis;
    public float moveSpeed;
    public float jumpSpeed;
    public Transform mainCamera;

    public Transform playerTR;
    public Rigidbody playerRB;

    private float jumpConstant = 10000f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    bool isGrounded;

    void Update()
    {
        hrznAxis = Input.GetAxis("Horizontal");
        vrtcAXis = Input.GetAxis("Vertical");

        PlayerMoves();
        PlayerRotates();
    }

    void PlayerMoves()
    {
        playerTR.transform.Translate(Vector3.right * hrznAxis * Time.deltaTime * moveSpeed); // horizontal movement
        playerTR.transform.Translate(Vector3.forward * vrtcAXis * Time.deltaTime * moveSpeed); // vertical movement

        // sprint
        if (Input.GetKey("left shift"))
        {
            moveSpeed = 6.25f;
        }
        else
        {
            if (isGrounded == false)
            {
                moveSpeed += 0;
            }
            else
            {
                moveSpeed = 2.5f;
            }

        }

        // jump
        if (Input.GetKey("space") && isGrounded) 
        {
            playerRB.AddForce(0, jumpConstant * jumpSpeed * Time.deltaTime, 0);
        }

        // crouch
        if (Input.GetKey("left ctrl")) 
        {
            transform.localScale = new Vector3(1, 0.5f, 1);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }

    void PlayerRotates()
    {
        // rotate camera and player
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, Camera.main.transform.eulerAngles.y, transform.rotation.eulerAngles.z);

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
    }

}
