using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool jumping;
    private GameObject camera;

    public float playerSpeed;
    public float jumpHeight;
    public float gravityValue;

    private Vector3 move;

    // Start is called before the first frame update
    void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(controller.isGrounded)
        {
            // Get the player's movement input, but only if they're on the ground
            move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            // Shouldn't have negative velocity while grounded, so enforce that
            if(playerVelocity.y < 0) { playerVelocity.y = 0; }
        }
        if(Input.GetButtonDown("Jump")) 
        { 
            jumping = true;
        }
    }

    // FixedUpdate is called once per physics update
    void FixedUpdate() 
    {
        // 2D planar movement
        controller.Move(move * Time.deltaTime * playerSpeed);

        // 3D planar movement
        playerVelocity.y += gravityValue * Time.deltaTime;
        if(jumping)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
            jumping = false;
        }
        controller.Move(playerVelocity * Time.deltaTime);
    }
}
