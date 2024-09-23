using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float walkingSpeed = 7.5f;
    public float runningSpeed = 11.5f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    public Camera playerCamera;
    public float lookSpeed = 2.0f;
    public float lookXLimit = 45.0f;
    public GameManager gameManager;
    public ScreenFader bloodEffect;
    public AudioSource deathSound;
    public Transform flashlight;
    public footstepSounds footstepSoundsScript;
    public bool isDead = false;

    CharacterController characterController;
    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;

    [HideInInspector]
    public bool canMove = true;

    void Start()
    {
        characterController = GetComponent<CharacterController>();

        // Lock cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (footstepSoundsScript == null)
        {
            footstepSoundsScript = GetComponent<footstepSounds>();
        }
    }
    void Update()
    {
        if (gameManager != null && gameManager.IsGameOver())
        {
            // If the game is over, do not perform movement
            return;
        }

        if (!canMove)
        {
            return; // Stop execution if the player is dead
        }

        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        // Press Left Shift to run
        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float curSpeedX = canMove ? (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Horizontal") : 0;
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        if (Input.GetButton("Jump") && canMove && characterController.isGrounded)
        {
            moveDirection.y = jumpSpeed;
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }

        // Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
        // when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
        // as an acceleration (ms^-2)
        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        // Move the controller
        characterController.Move(moveDirection * Time.deltaTime);

        // Player and Camera rotation
        if (canMove)
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }
    }

    public void HandleDeath()
    {
        canMove = false;
        isDead = true; 

        // Stop footstep sounds
        if (footstepSoundsScript != null)
        {
            footstepSoundsScript.StopFootstepSounds();
        }

        // Make the player invisible
        GetComponent<Renderer>().enabled = false;

        // Show the death panel
        if (gameManager != null)
        {
            gameManager.PlayerDied();
        }

        // Additional effects
        if (bloodEffect != null)
        {
            bloodEffect.ShowBloodEffect();
        }
        if (deathSound != null)
        {
            deathSound.Play();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            // Stop footstep sounds
            if (footstepSoundsScript != null)
            {
                footstepSoundsScript.StopFootstepSounds();
            }

            // Logic for player death
            if (gameManager != null)
            {
                gameManager.PlayerDied(); 
            }

            if (bloodEffect != null)
            {
                bloodEffect.ShowBloodEffect(); 
            }

            if (deathSound != null)
            {
                deathSound.Play(); 
            }

            isDead = true; 
        }
    }
}