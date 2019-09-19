using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float walkSpeed;
    public float jumpForce;
    public AudioSource coinSound;
    public float cameraDistZ = 6;
    Rigidbody rb;
    Collider col;
    bool pressedJump = false;
    Vector3 playerSize;
    // represents that you fell
    float minY = -1.5f;

    void Start () {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();

        playerSize = col.bounds.size;

        CameraFollowPlayer();
    }
	
	void FixedUpdate () {
        WalkHandler();
        JumpHandler();
        CameraFollowPlayer();
        FallHandler();
    }

    // check if the player fell
    void FallHandler()
    {
        if(transform.position.y <= minY)
        {
            GameManager.instance.GameOver();
        }
    }

    // walking logic
    void WalkHandler()
    {
        float hAxis = Input.GetAxis("Horizontal");
        float vAxis = Input.GetAxis("Vertical");

        // check that player is moving
        if(hAxis != 0 || vAxis != 0)
        {
            Vector3 movement = new Vector3(hAxis * walkSpeed * Time.deltaTime, 0, vAxis * walkSpeed * Time.deltaTime);
            Vector3 newPos = transform.position + movement;
            rb.MovePosition(newPos);
            Vector3 direction = new Vector3(hAxis, 0, vAxis);
            rb.rotation = Quaternion.LookRotation(direction);
        }
    }

    // jumping logic
    void JumpHandler()
    {
        float jAxis = Input.GetAxis("Jump");
        if(jAxis > 0)
        {
            bool isGrounded = CheckGrounded();

            if(!pressedJump && isGrounded)
            {
                pressedJump = true;
                Vector3 jumpVector = new Vector3(0, jAxis * jumpForce, 0);
                rb.AddForce(jumpVector, ForceMode.VelocityChange);
            }            
        }
        else
        {
            pressedJump = false;
        }
    }

    // check if the player is touching the ground
    bool CheckGrounded()
    {
        // location of corners
        Vector3 corner1 = transform.position + new Vector3(playerSize.x / 2, -playerSize.y / 2 + 0.01f, playerSize.z / 2);
        Vector3 corner2 = transform.position + new Vector3(-playerSize.x / 2, -playerSize.y / 2 + 0.01f, playerSize.z / 2);
        Vector3 corner3 = transform.position + new Vector3(playerSize.x / 2, -playerSize.y / 2 + 0.01f, -playerSize.z / 2);
        Vector3 corner4 = transform.position + new Vector3(-playerSize.x / 2, -playerSize.y / 2 + 0.01f, -playerSize.z / 2);

        // check if the player is grounded
        bool grounded1 = Physics.Raycast(corner1, -Vector3.up, 0.01f);
        bool grounded2 = Physics.Raycast(corner2, -Vector3.up, 0.01f);
        bool grounded3 = Physics.Raycast(corner3, -Vector3.up, 0.01f);
        bool grounded4 = Physics.Raycast(corner4, -Vector3.up, 0.01f);

        return (grounded1 || grounded2 || grounded3 || grounded4);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Coin"))
        {
            GameManager.instance.IncreaseScore(1);
            coinSound.Play();
            Destroy(other.gameObject);
        }
        else if(other.CompareTag("Enemy"))
        {
            GameManager.instance.GameOver();
        }
        else if (other.CompareTag("Goal"))
        {
            GameManager.instance.IncreaseLevel();
        }
    }

    void CameraFollowPlayer()
    {
        Vector3 cameraPos = Camera.main.transform.position;
        cameraPos.z = transform.position.z - cameraDistZ;
        Camera.main.transform.position = cameraPos;
    }
}
