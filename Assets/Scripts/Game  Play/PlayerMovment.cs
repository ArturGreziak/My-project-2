 using UnityEngine;

public class PlayerMovment : MonoBehaviour
{
    [Header("Refernces")]
    [SerializeField] private Rigidbody2D playerRidygbody;
    [SerializeField] private GroundChecker groundChecker;
    [SerializeField] private Transform legsTransform;
    [Header("Move")]
    [SerializeField] private float moveSpeed = 8f;
    [SerializeField] private GameObject movementParticlePrefab;
    [SerializeField] private float movementParticleLifetime = 0.3f;
    [SerializeField] private AudioClip moveSound;
    [SerializeField] private float moveSoundDelay = 1f;
    [Header("Jump")]
    [SerializeField] private float jumpPower = 8;
    [SerializeField] private float dublejumpPower = 8;
    [SerializeField] private AudioClip jumpSound;
    [Header("Land")]
    [SerializeField] private GameObject landingParticlePrafab;
    [SerializeField] private float landingParticleLifetime = 1f;


    private float  inputX;
    private bool hasDoubleJump = true;
    private bool jumpRequested = false;
    private float moveSoundTime = 0f;
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

        if (groundChecker.IsGrounded)
        {
            hasDoubleJump = true;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(groundChecker.IsGrounded)
            {
                jumpRequested = true;
                AudioSource.PlayClipAtPoint(jumpSound, transform.position);
            }
            else if( hasDoubleJump )
            {
                hasDoubleJump = false;
                jumpRequested = true;
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
        float speed = inputX * moveSpeed;
        playerRidygbody.velocity = new Vector2(speed, playerRidygbody.velocity.y);
        Debug.Log($"{playerRidygbody.velocity} -> i:{inputX} dt:{Time.fixedDeltaTime} spd:{moveSpeed}");
        if(jumpRequested)
        {
            float currentJumpPower = jumpPower;
            if(groundChecker.IsGrounded == false)
            {
                currentJumpPower = dublejumpPower;
            }

            playerRidygbody.velocity = new Vector2(playerRidygbody.velocity.x, 0);
            playerRidygbody.AddForce(new Vector2(0, currentJumpPower), ForceMode2D.Impulse);
            jumpRequested = false;
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
