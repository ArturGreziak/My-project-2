using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovment : MonoBehaviour
{
    [Header("Refernces")]
    [SerializeField] private Rigidbody2D playerRidygbody;
    [SerializeField] private GroundChecker groundChecker;
    [SerializeField] private Transform legsTransform;
    [SerializeField] private GameObject landingParticlePrafab;
    [SerializeField] private float landingParticleLifetime = 1f;
    [SerializeField] private GameObject movementParticlePrefab;
    [SerializeField] private float movementParticleLifetime = 0.3f;
    [Space(5)]
    [Header("Setings")]
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float jumpPower = 50f;
    [SerializeField] private float dubeljumpPower = 50f;
    [Space(5)]
    [Header("Sounds")]
    [SerializeField] private AudioClip jumpSound;
    [SerializeField] private float moveSoundDelay = 1f;
     [SerializeField] private AudioClip moveSound;


    private float moveSoundTime = 0f;
    private float  inputX;
    private bool isDubleJump = false;
    private bool isJumpingInput = false;
    private Platform currentPlatform;

    private void Start()
    {
        groundChecker.OnLanding += HandleLanding;
    }

    private void HandleLanding()
    {
        var spawnedPrefab = Instantiate(landingParticlePrafab, legsTransform.position, Quaternion.identity);
        Destroy(spawnedPrefab, landingParticleLifetime );
    }

    private void Update()
    {
        HandleInput();
        HandleMovementEffeects();
    }

    private void HandleMovementEffeects()
    {
        if (isMoving() && groundChecker.IsGrounded)
        {
            moveSoundTime += Time.deltaTime;
            if (moveSoundTime >= moveSoundDelay)
            {
                moveSoundTime -= moveSoundDelay;
                AudioSource.PlayClipAtPoint(moveSound, transform.position);
                var spawnerPrefab = Instantiate(movementParticlePrefab, legsTransform.position, Quaternion.identity);
                Destroy(spawnerPrefab, movementParticleLifetime);
            }
        }
    }

    private void HandleInput()
    {
        inputX = Input.GetAxis("Horizontal");

        if (isMoving() && groundChecker)
        {
            isDubleJump = true;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(groundChecker.IsGrounded)
            {
                isDubleJump = true;
                isJumpingInput = true;
                AudioSource.PlayClipAtPoint(jumpSound, transform.position);
            } else if(isDubleJump) 
            {
                isJumpingInput = true;
                isDubleJump = false;
                AudioSource.PlayClipAtPoint(jumpSound, transform.position);
            }
            
         }

        if (Input.GetKeyDown(KeyCode.S) && currentPlatform != null && groundChecker.IsGrounded)
        {
            currentPlatform.SetCollidable(false);
        }
    }

    private void FixedUpdate()
    {
        float moveInput = inputX * Time.fixedTime * moveSpeed;
        playerRidygbody.velocity = new Vector2(moveInput, playerRidygbody.velocity.y);
        if(isJumpingInput )
        {
            float currentJumpPower = jumpPower;
            if(isDubleJump)
            {
                currentJumpPower = jumpPower;
            }

            playerRidygbody.velocity = new Vector2(playerRidygbody.velocity.x, 0);
            playerRidygbody.AddForce(new Vector2(0, jumpPower),ForceMode2D.Impulse);
            isJumpingInput = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.TryGetComponent( out Platform platform))
        {
            currentPlatform = platform;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.collider.TryGetComponent( out Platform platform))
        {
            currentPlatform = null;
        }
    }

    

    public bool isMoving()
    {
        return inputX != 0;
    }

    public float GetCurrentInputX()
    {
        return inputX;
    }
}
